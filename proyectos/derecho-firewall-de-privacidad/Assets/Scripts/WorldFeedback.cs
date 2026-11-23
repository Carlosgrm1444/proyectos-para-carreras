using UnityEngine;

public sealed class WorldFeedback : MonoBehaviour
{
    private Transform center;
    private Transform guardian;
    private Transform centerRing;
    private Transform guardianRing;
    private Transform centerPlatform;
    private Transform centerPlatformInner;

    public void Configure(Transform dataCenter, Transform player)
    {
        center = dataCenter;
        guardian = player;
        centerRing = CreateRing("CenterPulseRing", Color.cyan);
        guardianRing = CreateRing("GuardianPulseRing", Color.yellow);
        centerPlatform = CreatePlatform("CenterPlatformOuter", new Color(.08f, .28f, .38f), 3.6f, .12f);
        centerPlatformInner = CreatePlatform("CenterPlatformInner", new Color(.10f, .58f, .76f), 2.55f, .08f);
    }

    private Transform CreateRing(string name, Color color)
    {
        var ring = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        ring.name = name;
        PrimitiveMaterial.Apply(ring.GetComponent<Renderer>(), color);
        ring.GetComponent<Collider>().enabled = false;
        ring.transform.localScale = new Vector3(1.8f, .025f, 1.8f);
        return ring.transform;
    }

    private static Transform CreatePlatform(string name, Color color, float radius, float height)
    {
        var platform = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        platform.name = name;
        PrimitiveMaterial.Apply(platform.GetComponent<Renderer>(), color);
        platform.GetComponent<Collider>().enabled = false;
        platform.transform.localScale = new Vector3(radius, height, radius);
        return platform.transform;
    }

    private void Update()
    {
        var pulse = 1f + Mathf.Sin(Time.time * 3f) * .08f;
        if (center != null && centerRing != null)
        {
            centerRing.position = center.position + Vector3.up * .08f;
            centerRing.localScale = new Vector3(1.8f, .025f, 1.8f) * pulse;
            centerRing.Rotate(Vector3.up, 20f * Time.deltaTime);
            if (centerPlatform != null)
            {
                centerPlatform.position = center.position + Vector3.up * .015f;
                centerPlatform.Rotate(Vector3.up, 6f * Time.deltaTime);
            }
            if (centerPlatformInner != null)
            {
                centerPlatformInner.position = center.position + Vector3.up * .06f;
                centerPlatformInner.Rotate(Vector3.up, -9f * Time.deltaTime);
            }
        }
        if (guardian != null && guardianRing != null)
        {
            guardianRing.position = guardian.position + Vector3.up * .04f;
            guardianRing.localScale = new Vector3(1.25f, .02f, 1.25f) * (1f + Mathf.Sin(Time.time * 5f) * .12f);
            guardianRing.Rotate(Vector3.up, -45f * Time.deltaTime);
        }
    }
}
