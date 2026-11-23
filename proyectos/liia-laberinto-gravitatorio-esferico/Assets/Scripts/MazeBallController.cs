using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class MazeBallController : MonoBehaviour
    {
        [SerializeField] private float speedCellsPerSecond = 4.2f;
        [SerializeField] private float radiusInCells = .27f;
        [SerializeField] private float ballSurfaceOffset = .28f;

        private SphericalMazeLayout layout;
        private ISphereTiltInput input;
        private Vector2 position;
        private Vector2 startPosition;

        public Vector2 GridPosition => position;

        public void Configure(SphericalMazeLayout maze, MonoBehaviour inputSource)
        {
            layout = maze;
            input = inputSource as ISphereTiltInput;
            startPosition = layout.StartCell;
            position = startPosition;
            SnapToSurface();
        }

        private void Update()
        {
            if (layout == null || GameFlow.Instance == null || GameFlow.Instance.State != GameState.Playing) return;
            var direction = input == null ? Vector2.zero : input.ReadTilt();
            var amount = direction * speedCellsPerSecond * Time.deltaTime;
            TryMove(new Vector2(amount.x, 0f));
            TryMove(new Vector2(0f, amount.y));
            SnapToSurface();

            var cell = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
            if (cell == layout.GoalCell && Vector2.Distance(position, layout.GoalCell) < .32f)
                GameFlow.Instance.ReachGoal();
        }

        public void RespawnStart()
        {
            position = startPosition;
            SnapToSurface();
        }

        private void TryMove(Vector2 amount)
        {
            if (amount.sqrMagnitude < .000001f) return;
            var candidate = position + amount;
            if (layout.CanOccupy(candidate, radiusInCells)) position = candidate;
        }

        private void SnapToSurface()
        {
            transform.position = layout.SurfacePoint(position.x, position.y, ballSurfaceOffset);
            transform.up = layout.SurfaceNormal(position.x, position.y);
        }
    }
}
