using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class MazeBallController : MonoBehaviour
    {
        [SerializeField] private float accelerationCellsPerSecond = 32f;
        [SerializeField] private float maxSpeedCellsPerSecond = 4.6f;
        [SerializeField] private float rollingResistance = 8.5f;
        [SerializeField] private float radiusInCells = .27f;
        [SerializeField] private float ballSurfaceOffset = .28f;
        [SerializeField] private float visualRadius = .22f;

        private SphericalMazeLayout layout;
        private ISphereTiltInput input;
        private Transform ballVisual;
        private Vector2 position;
        private Vector2 startPosition;
        private Vector2 velocity;

        public Vector2 GridPosition => position;
        public Vector2 Velocity => velocity;
        public float Speed => velocity.magnitude;
        public Vector2Int StartCell => Vector2Int.RoundToInt(startPosition);

        public void Configure(SphericalMazeLayout maze, MonoBehaviour inputSource, Transform visual)
        {
            layout = maze;
            input = inputSource as ISphereTiltInput;
            ballVisual = visual;
            startPosition = layout.StartCell;
            position = startPosition;
            SnapToSurface(position);
        }

        private void Update()
        {
            if (layout == null || GameFlow.Instance == null || GameFlow.Instance.State != GameState.Playing) return;
            var direction = input == null ? Vector2.zero : input.ReadTilt();
            // El PNG usa Y creciente desde su borde inferior; invertimos la
            // entrada vertical para que la flecha arriba avance hacia arriba
            // en la lectura visual del laberinto.
            direction.y = -direction.y;
            if (direction.sqrMagnitude > .001f)
            {
                var targetVelocity = direction * maxSpeedCellsPerSecond;
                velocity = Vector2.MoveTowards(velocity, targetVelocity, accelerationCellsPerSecond * Time.deltaTime);
            }
            else
            {
                velocity = Vector2.MoveTowards(velocity, Vector2.zero, rollingResistance * Time.deltaTime);
            }

            var previousPosition = position;
            MoveWithCollision(velocity * Time.deltaTime);
            SnapToSurface(previousPosition);

            var cell = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
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
            velocity = Vector2.zero;
            if (ballVisual != null) ballVisual.localRotation = Quaternion.identity;
            SnapToSurface(position);
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
            var steps = Mathf.Max(1, Mathf.CeilToInt(amount.magnitude / .08f));
            var step = amount / steps;
            for (var i = 0; i < steps; i++)
            {
                var horizontal = position + new Vector2(step.x, 0f);
                if (layout.CanOccupy(horizontal, radiusInCells)) position = horizontal;
                else velocity.x = 0f;

                var vertical = position + new Vector2(0f, step.y);
                if (layout.CanOccupy(vertical, radiusInCells)) position = vertical;
                else velocity.y = 0f;
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
    }
}
