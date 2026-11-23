using System.Collections;
using UnityEngine;

namespace LIIA.SphereMaze
{
    public sealed class LIIAVisualFeedback : MonoBehaviour
    {
        public static LIIAVisualFeedback Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void PlayCollect(Vector3 position, Vector3 normal)
        {
            StartCoroutine(PulseRing(position, normal, new Color(1f, .64f, .06f), .18f, 1.4f, .5f));
        }

        public void PlayCheckpoint(Vector3 position, Vector3 normal)
        {
            StartCoroutine(PulseRing(position, normal, new Color(.1f, .68f, 1f), .22f, 1.65f, .65f));
        }

        public void PlayImpact(Vector3 position, Vector3 normal)
        {
            StartCoroutine(ImpactFlash(position, normal));
        }

        public void PlayGoal(Vector3 position)
        {
            var normal = position.sqrMagnitude > .001f ? position.normalized : Vector3.up;
            StartCoroutine(PulseRing(position, normal, new Color(1f, .52f, .08f), .25f, 2.2f, .85f));
            StartCoroutine(DelayedPulse(position, normal, .18f));
        }

        private IEnumerator DelayedPulse(Vector3 position, Vector3 normal, float delay)
        {
            yield return new WaitForSeconds(delay);
            yield return PulseRing(position, normal, new Color(1f, .86f, .35f), .18f, 2.7f, .8f);
        }

        private static IEnumerator PulseRing(Vector3 position, Vector3 normal, Color color, float startScale, float endScale, float duration)
        {
            var root = new GameObject("FX_GlowPulse");
            root.transform.position = position + normal * .08f;
            root.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);
            var line = LIIAGeometryFactory.AddGlowRing(root, color, .32f, .035f);
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var progress = Mathf.Clamp01(elapsed / duration);
                root.transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, Mathf.SmoothStep(0f, 1f, progress));
                line.widthMultiplier = Mathf.Lerp(1f, .08f, progress);
                yield return null;
            }
            Destroy(root);
        }

        private static IEnumerator ImpactFlash(Vector3 position, Vector3 normal)
        {
            var flash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            flash.name = "FX_Impact";
            flash.transform.position = position + normal * .05f;
            flash.GetComponent<Renderer>().sharedMaterial = LIIAMaterialFactory.Create(new Color(1f, .72f, .25f), 2.8f, .2f, .8f);
            Destroy(flash.GetComponent<Collider>());
            var elapsed = 0f;
            const float duration = .22f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var progress = Mathf.Clamp01(elapsed / duration);
                flash.transform.localScale = Vector3.one * Mathf.Lerp(.08f, .38f, progress) * (1f - progress * .72f);
                yield return null;
            }
            Destroy(flash);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}
