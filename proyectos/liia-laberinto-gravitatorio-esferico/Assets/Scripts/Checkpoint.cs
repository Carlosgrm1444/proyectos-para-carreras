using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class Checkpoint : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SurfaceBallController>() != null) GameFlow.Instance?.SetCheckpoint(transform);
        }
    }
}
