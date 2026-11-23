using System;
using System.Collections;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class MazeBallController : MonoBehaviour
    {
        [SerializeField] private float gravityAccelerationCellsPerSecond = 11.5f;
        [SerializeField] private float maxSpeedCellsPerSecond = 4.6f;
        [SerializeField] private float rollingResistance = 1.9f;
        [SerializeField] private float maxTiltDegrees = 12f;
        [SerializeField] private float tiltResponseDegreesPerSecond = 36f;
        [SerializeField] private float radiusInCells = .27f;
        [SerializeField] private float ballSurfaceOffset = .38f;
        [SerializeField] private float visualRadius = .22f;

        private SphericalMazeLayout layout;
        private ISphereTiltInput input;
        private Transform ballVisual;
        private Vector2 position;
        private Vector2 startPosition;
        private Vector2 velocity;
        private Vector2 tiltDegrees;
        private Vector3 visualBaseScale;
        private bool isRespawning;
        private float lastImpactAt;

        public Vector2 GridPosition => position;
        public Vector2 Velocity => velocity;
        public float Speed => velocity.magnitude;
        public Vector2 TiltDegrees => tiltDegrees;
        public bool IsRespawning => isRespawning;
        public Vector2Int StartCell => Vector2Int.RoundToInt(startPosition);
        public SphericalMazeLayout Layout => layout;

        public void Configure(SphericalMazeLayout maze, MonoBehaviour inputSource, Transform visual)
        {
            layout = maze;
            input = inputSource as ISphereTiltInput;
            ballVisual = visual;
            visualBaseScale = ballVisual == null ? Vector3.one : ballVisual.localScale;
            startPosition = layout.StartCell;
            position = startPosition;
            SnapToSurface(position);
        }

        private void Update()
        {
            if (layout == null || isRespawning || GameFlow.Instance == null || GameFlow.Instance.State != GameState.Playing) return;
            var direction = input == null ? Vector2.zero : input.ReadTilt();
            // El PNG usa Y creciente desde su borde inferior; invertimos la
            // entrada vertical para que la flecha arriba avance hacia arriba
            // en la lectura visual del laberinto.
            direction.y = -direction.y;
            var targetTilt = direction * maxTiltDegrees;
            tiltDegrees = Vector2.MoveTowards(tiltDegrees, targetTilt, tiltResponseDegreesPerSecond * Time.deltaTime);
            layout.transform.localRotation = Quaternion.Euler(tiltDegrees.y, 0f, -tiltDegrees.x);

            // La entrada controla la inclinación virtual. La aceleración de la
            // bola se deriva de esa inclinación; nunca se asigna su posición o
            // velocidad directamente desde WASD.
            velocity += tiltDegrees / maxTiltDegrees * gravityAccelerationCellsPerSecond * Time.deltaTime;
            if (direction.sqrMagnitude < .001f)
                velocity = Vector2.MoveTowards(velocity, Vector2.zero, rollingResistance * Time.deltaTime);
            velocity = Vector2.ClampMagnitude(velocity, maxSpeedCellsPerSecond);

            var previousPosition = position;
            MoveWithCollision(velocity * Time.deltaTime);
            SnapToSurface(previousPosition);

            var cell = layout.NormalizeCell(new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)));
            if (Vector2.Distance(position, cell) < .32f)
                ProcessCell(cell);
        }

        public void RespawnStart()
        {
            RespawnAt(StartCell);
        }

        public void RespawnAt(Vector2Int cell)
        {
            position = cell;
            position = layout.NormalizeGridPosition(position);
            velocity = Vector2.zero;
            tiltDegrees = Vector2.zero;
            layout.transform.localRotation = Quaternion.identity;
            if (ballVisual != null)
            {
                ballVisual.localRotation = Quaternion.identity;
                ballVisual.localScale = visualBaseScale;
            }
            SnapToSurface(position);
        }

        public void PlayFallAndRespawn(Vector2Int targetCell, Action completed = null)
        {
            if (isRespawning) return;
            if (!Application.isPlaying)
            {
                RespawnAt(targetCell);
                completed?.Invoke();
                return;
            }
            StartCoroutine(FallRoutine(targetCell, completed));
        }

        private void ProcessCell(Vector2Int cell)
        {
            switch (layout.TileAt(cell.x, cell.y))
            {
                case MazeTile.Goal:
                    GameFlow.Instance.ReachGoal();
                    break;
                case MazeTile.Star:
                    GameFlow.Instance.CollectStar(cell);
                    break;
                case MazeTile.Checkpoint:
                    GameFlow.Instance.SetCheckpoint(cell);
                    break;
                case MazeTile.Trap:
                    GameFlow.Instance.PlayerHitTrap();
                    break;
            }
        }

        private void MoveWithCollision(Vector2 amount)
        {
            if (amount.sqrMagnitude < .000001f) return;
            var impactSpeed = velocity.magnitude;
            var collided = false;
            var steps = Mathf.Max(1, Mathf.CeilToInt(amount.magnitude / .08f));
            var step = amount / steps;
            for (var i = 0; i < steps; i++)
            {
                var horizontal = position + new Vector2(step.x, 0f);
                if (layout.CanOccupy(horizontal, radiusInCells)) position = layout.NormalizeGridPosition(horizontal);
                else
                {
                    velocity.x *= -.18f;
                    collided = true;
                }

                var vertical = position + new Vector2(0f, step.y);
                if (layout.CanOccupy(vertical, radiusInCells)) position = layout.NormalizeGridPosition(vertical);
                else
                {
                    velocity.y *= -.18f;
                    collided = true;
                }
            }
            if (collided && impactSpeed > .65f && Time.unscaledTime - lastImpactAt > .12f)
            {
                lastImpactAt = Time.unscaledTime;
                LIIAAudioFeedback.Instance?.PlayImpact(impactSpeed / maxSpeedCellsPerSecond);
                LIIAVisualFeedback.Instance?.PlayImpact(transform.position, layout.SurfaceNormal(position.x, position.y));
            }
        }

        private void SnapToSurface(Vector2 previousPosition)
        {
            var previousWorldPosition = layout.SurfacePoint(previousPosition.x, previousPosition.y, ballSurfaceOffset);
            transform.position = layout.SurfacePoint(position.x, position.y, ballSurfaceOffset);
            var normal = layout.SurfaceNormal(position.x, position.y);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);

            if (ballVisual == null) return;
            var travelled = Vector3.ProjectOnPlane(transform.position - previousWorldPosition, normal);
            if (travelled.sqrMagnitude < .000001f) return;
            var rollingAxis = Vector3.Cross(travelled.normalized, normal);
            var degrees = travelled.magnitude / visualRadius * Mathf.Rad2Deg;
            ballVisual.Rotate(rollingAxis, degrees, Space.World);
        }

        private IEnumerator FallRoutine(Vector2Int targetCell, Action completed)
        {
            isRespawning = true;
            velocity = Vector2.zero;
            tiltDegrees = Vector2.zero;
            layout.transform.localRotation = Quaternion.identity;
            var startWorld = transform.position;
            var inward = (layout.transform.position - startWorld).normalized;
            const float duration = .52f;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var progress = Mathf.Clamp01(elapsed / duration);
                transform.position = startWorld + inward * Mathf.SmoothStep(0f, 1.05f, progress);
                transform.Rotate(Vector3.right, 520f * Time.deltaTime, Space.Self);
                if (ballVisual != null)
                    ballVisual.localScale = visualBaseScale * Mathf.Lerp(1f, .08f, progress);
                yield return null;
            }
            isRespawning = false;
            RespawnAt(targetCell);
            completed?.Invoke();
        }
    }
}
