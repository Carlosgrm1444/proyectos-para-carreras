using UnityEngine;

public static class WorldCollision
{
    public static Vector3 Move(Vector3 position, Vector3 delta, float radius)
    {
        if (delta.sqrMagnitude < .00001f) return position;
        var distance = delta.magnitude;
        var direction = delta / distance;
        var origin = position + Vector3.up * .45f;
        if (!Physics.SphereCast(origin, radius, direction, out var hit, distance + .04f) || hit.collider.GetComponentInParent<ObstacleMarker>() == null)
            return position + delta;

        var allowed = Mathf.Max(0f, hit.distance - .045f);
        var moved = position + direction * allowed;
        var remainder = delta - direction * allowed;
        var slide = Vector3.ProjectOnPlane(remainder, hit.normal);
        if (slide.sqrMagnitude < .00001f) return moved;
        var slideDistance = slide.magnitude;
        var slideDirection = slide / slideDistance;
        if (!Physics.SphereCast(moved + Vector3.up * .45f, radius, slideDirection, out var slideHit, slideDistance + .04f) || slideHit.collider.GetComponentInParent<ObstacleMarker>() == null)
            return moved + slide;
        return moved + slideDirection * Mathf.Max(0f, slideHit.distance - .045f);
    }

    public static bool IsObstacle(RaycastHit hit) => hit.collider != null && hit.collider.GetComponentInParent<ObstacleMarker>() != null;
}

