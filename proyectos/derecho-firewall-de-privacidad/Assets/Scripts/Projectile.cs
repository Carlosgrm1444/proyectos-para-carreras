using UnityEngine;

public sealed class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 1.5f;
    private Vector3 direction;
    private GameSession session;

    public void Configure(Vector3 travelDirection, GameSession owner)
    {
        direction = travelDirection.sqrMagnitude > .01f ? travelDirection.normalized : Vector3.forward;
        session = owner;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        var delta = direction * speed * Time.deltaTime;
        if (Physics.SphereCast(transform.position, .12f, direction, out var hit, delta.magnitude) && WorldCollision.IsObstacle(hit))
        {
            Destroy(gameObject);
            return;
        }
        transform.position += delta;
        foreach (var threat in FindObjectsOfType<Threat>())
        {
            if (Vector3.Distance(transform.position, threat.transform.position) < 1f)
            {
                threat.TakeDamage(1);
                Destroy(gameObject);
                return;
            }
        }
    }
}
