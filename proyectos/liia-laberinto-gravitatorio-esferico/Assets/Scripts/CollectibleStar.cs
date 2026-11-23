using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class CollectibleStar : MonoBehaviour
    {
        public bool Collected { get; private set; }
        public void MarkCollected() { Collected = true; gameObject.SetActive(false); }
        public void ResetStar() { Collected = false; gameObject.SetActive(true); }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SurfaceBallController>() != null) GameFlow.Instance?.CollectStar(this);
        }
    }
}
