using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class MazeGameplayMarker : MonoBehaviour
    {
        private MazeTile type;
        private Vector2Int cell;
        private Renderer markerRenderer;

        public void Configure(MazeTile markerType, Vector2Int markerCell)
        {
            type = markerType;
            cell = markerCell;
            markerRenderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (type != MazeTile.Star || markerRenderer == null || GameFlow.Instance == null) return;
            markerRenderer.enabled = !GameFlow.Instance.IsStarCollected(cell);
        }
    }
}
