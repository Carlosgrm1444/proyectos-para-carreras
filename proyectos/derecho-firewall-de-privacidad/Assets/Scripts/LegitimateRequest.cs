using UnityEngine;

public sealed class LegitimateRequest : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    private Transform target;
    private GameSession session;
    public Transform Target => target;

    public void Configure(Transform destination, GameSession owner) { target = destination; session = owner; }

    private void Update()
    {
        if (target == null) return;
        var direction = NpcNavigation.GetDirection(transform, target.position, .35f);
        transform.position = WorldCollision.Move(transform.position, direction * speed * Time.deltaTime, .34f);
        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            session.RequestSaved(this);
            Destroy(gameObject);
        }
    }
}

