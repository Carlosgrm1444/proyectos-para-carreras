using UnityEngine;

public sealed class ThreatProjectile : MonoBehaviour
{
    private Transform guardian;
    private Transform center;
    private GameSession session;
    private int damage;
    private Vector3 direction;
    private float life = 3f;

    public void Configure(Transform targetGuardian, Transform targetCenter, GameSession owner, int amount)
    {
        guardian = targetGuardian;
        center = targetCenter;
        session = owner;
        damage = amount;
        direction = guardian == null ? Vector3.forward : (guardian.position - transform.position).normalized;
    }

    private void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0f) { Destroy(gameObject); return; }
        var delta = direction * 8f * Time.deltaTime;
        if (Physics.SphereCast(transform.position, .12f, direction, out var hit, delta.magnitude) && WorldCollision.IsObstacle(hit))
        {
            Destroy(gameObject);
            return;
        }
        transform.position += delta;
        if (guardian != null && Vector3.Distance(transform.position, guardian.position) < .7f)
        {
            session.ThreatHitGuardian(damage);
            Destroy(gameObject);
        }
    }
}
