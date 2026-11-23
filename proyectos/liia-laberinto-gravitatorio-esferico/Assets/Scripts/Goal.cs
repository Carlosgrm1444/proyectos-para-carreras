using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class Goal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SurfaceBallController>() != null) GameFlow.Instance?.ReachGoal();
        }
    }
}
