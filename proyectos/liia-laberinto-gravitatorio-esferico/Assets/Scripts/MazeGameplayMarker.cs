using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class MazeGameplayMarker : MonoBehaviour
    {
        private MazeTile type;
        private Vector2Int cell;
        private Renderer[] markerRenderers;
        private bool previousCollected;
        private bool previousCheckpointActive;
        private Vector3 baseScale;

        public void Configure(MazeTile markerType, Vector2Int markerCell)
        {
            type = markerType;
            cell = markerCell;
            markerRenderers = GetComponentsInChildren<Renderer>();
            baseScale = transform.localScale;
        }

        private void Update()
        {
            if (GameFlow.Instance == null) return;
            if (type == MazeTile.Star)
            {
                transform.Rotate(Vector3.up, 68f * Time.deltaTime, Space.Self);
                transform.localScale = baseScale * (1f + Mathf.Sin(Time.time * 4.5f + cell.x) * .08f);
                var collected = GameFlow.Instance.IsStarCollected(cell);
                if (collected && !previousCollected)
                    LIIAVisualFeedback.Instance?.PlayCollect(transform.position, transform.up);
                SetRenderersVisible(!collected);
                previousCollected = collected;
            }
            else if (type == MazeTile.Checkpoint)
            {
                var active = GameFlow.Instance.IsCheckpointActive(cell);
                if (active && !previousCheckpointActive)
                    LIIAVisualFeedback.Instance?.PlayCheckpoint(transform.position, transform.up);
                transform.localScale = baseScale * (active ? 1.12f + Mathf.Sin(Time.time * 5f) * .04f : 1f);
                previousCheckpointActive = active;
            }
            else if (type == MazeTile.Trap)
            {
                transform.localScale = baseScale * (1f + Mathf.Sin(Time.time * 6f + cell.y) * .06f);
            }
        }

        private void SetRenderersVisible(bool visible)
        {
            if (markerRenderers == null) return;
            foreach (var item in markerRenderers) item.enabled = visible;
        }
    }
}
