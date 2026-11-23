using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class BallFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform surfaceCenter;
        [SerializeField] private float gameplayHeight = 6.4f;
        [SerializeField] private float overviewHeight = 7.2f;
        [SerializeField] private float normalSmoothSpeed = 9f;
        [SerializeField] private float catchUpNormalSmoothSpeed = 18f;
        [SerializeField] private float maximumAngularLag = 4f;
        [SerializeField] private float rotationSpeed = 14f;
        [SerializeField] private float distanceSmoothSpeed = 9f;
        [SerializeField] private float positionSmoothSpeed = 14f;

        private Vector3 cameraNormal;
        private Vector3 referenceUp;
        private float currentHeight;
        private MazeBallController ball;

        public void Configure(Transform followTarget, Transform center)
        {
            target = followTarget;
            surfaceCenter = center;
            cameraNormal = (target.position - surfaceCenter.position).normalized;
            referenceUp = FindStableUp(cameraNormal);
            currentHeight = overviewHeight;
            ball = target.GetComponent<MazeBallController>();
        }

        private void LateUpdate()
        {
            if (target == null || surfaceCenter == null) return;

            if (Input.GetKeyDown(KeyCode.C)) RecenterImmediately();

            var state = GameFlow.Instance == null ? GameState.Attract : GameFlow.Instance.State;
            var surfaceNormal = (target.position - surfaceCenter.position).normalized;
            if (state == GameState.Attract)
            {
                // La portada usa un ángulo tres cuartos: enseña varias franjas
                // del circuito en vez de mirar únicamente el punto de inicio.
                surfaceNormal = (Quaternion.AngleAxis(105f, Vector3.up) * surfaceNormal + Vector3.up * .12f).normalized;
            }
            var normalSpeed = Vector3.Angle(cameraNormal, surfaceNormal) > maximumAngularLag
                ? catchUpNormalSmoothSpeed
                : normalSmoothSpeed;
            var normalBlend = 1f - Mathf.Exp(-normalSpeed * Time.deltaTime);
            var previousNormal = cameraNormal;
            cameraNormal = Vector3.Slerp(cameraNormal, surfaceNormal, normalBlend).normalized;

            // El eje visual se deriva de la misma cuadrícula que usa el
            // controlador. Así izquierda/derecha y arriba/abajo conservan su
            // significado en pantalla aunque la bola cambie de hemisferio.
            referenceUp = GridAlignedUp(cameraNormal);

            var desiredHeight = state == GameState.Attract || state == GameState.Won
                ? overviewHeight
                : gameplayHeight + (ball == null ? 0f : Mathf.Clamp01(ball.Speed / 4.6f) * .45f);
            currentHeight = Mathf.Lerp(currentHeight, desiredHeight, 1f - Mathf.Exp(-distanceSmoothSpeed * Time.unscaledDeltaTime));

            var cameraDistance = Vector3.Distance(target.position, surfaceCenter.position) + currentHeight;
            var desiredPosition = surfaceCenter.position + cameraNormal * cameraDistance;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 1f - Mathf.Exp(-positionSmoothSpeed * Time.unscaledDeltaTime));

            var desiredRotation = Quaternion.LookRotation(-cameraNormal, referenceUp);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                desiredRotation,
                1f - Mathf.Exp(-rotationSpeed * Time.unscaledDeltaTime));
        }

        private void RecenterImmediately()
        {
            if (target == null || surfaceCenter == null) return;
            cameraNormal = (target.position - surfaceCenter.position).normalized;
            referenceUp = GridAlignedUp(cameraNormal);
            var state = GameFlow.Instance == null ? GameState.Attract : GameFlow.Instance.State;
            currentHeight = state == GameState.Attract || state == GameState.Won ? overviewHeight : gameplayHeight;
            transform.position = surfaceCenter.position + cameraNormal * (Vector3.Distance(target.position, surfaceCenter.position) + currentHeight);
            transform.rotation = Quaternion.LookRotation(-cameraNormal, referenceUp);
        }

        private Vector3 GridAlignedUp(Vector3 normal)
        {
            if (ball != null && ball.Layout != null)
            {
                var gridUp = Vector3.ProjectOnPlane(ball.Layout.SurfaceTangentY(ball.GridPosition.x, ball.GridPosition.y), normal);
                if (gridUp.sqrMagnitude > .001f) return gridUp.normalized;
            }
            return FindStableUp(normal);
        }

        private static Vector3 FindStableUp(Vector3 normal)
        {
            var up = Vector3.ProjectOnPlane(Vector3.up, normal);
            if (up.sqrMagnitude < .01f) up = Vector3.ProjectOnPlane(Vector3.forward, normal);
            return up.normalized;
        }
    }
}
