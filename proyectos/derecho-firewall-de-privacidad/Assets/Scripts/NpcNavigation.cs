using System.Collections.Generic;
using UnityEngine;

public static class NpcNavigation
{
    private const int MinCell = -15;
    private const int MaxCell = 15;
    private const float RepathInterval = .45f;

    private sealed class AgentPath
    {
        public readonly List<Vector3> points = new List<Vector3>();
        public int index;
        public float nextRepath;
        public Vector2Int targetCell;
    }

    private static readonly Dictionary<Transform, AgentPath> paths = new Dictionary<Transform, AgentPath>();
    private static readonly Vector2Int[] Neighbors =
    {
        new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1),
        new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1)
    };
    private static float nextCleanup;

    public static Vector3 GetDirection(Transform mover, Vector3 target, float clearance = .45f)
    {
        if (mover == null) return Vector3.zero;
        if (Time.time >= nextCleanup)
        {
            nextCleanup = Time.time + 5f;
            var stale = new List<Transform>();
            foreach (var pair in paths) if (pair.Key == null) stale.Add(pair.Key);
            foreach (var key in stale) paths.Remove(key);
        }
        if (!paths.TryGetValue(mover, out var path)) { path = new AgentPath(); paths[mover] = path; }
        var targetCell = ToCell(target);
        if (Time.time >= path.nextRepath || path.targetCell != targetCell || path.points.Count == 0)
        {
            BuildPath(mover.position, target, clearance, path);
            path.targetCell = targetCell;
            path.nextRepath = Time.time + RepathInterval;
        }
        while (path.index < path.points.Count - 1 && FlatDistance(mover.position, path.points[path.index]) < .42f) path.index++;
        var steeringTarget = path.points.Count > 0 && path.index < path.points.Count ? path.points[path.index] : target;
        return DirectDirection(mover.position, steeringTarget);
    }

    public static Vector3 GetDirection(Vector3 origin, Vector3 target, float clearance = .45f) => DirectDirection(origin, target);

    private static void BuildPath(Vector3 origin, Vector3 target, float clearance, AgentPath output)
    {
        output.points.Clear(); output.index = 0;
        var start = FindOpenCell(ToCell(origin), clearance);
        var goal = FindOpenCell(ToCell(target), clearance);
        var open = new List<Vector2Int> { start };
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float> { [start] = 0f };
        var fScore = new Dictionary<Vector2Int, float> { [start] = Heuristic(start, goal) };
        var found = false;
        for (var safety = 0; open.Count > 0 && safety < 1200; safety++)
        {
            var best = 0;
            for (var i = 1; i < open.Count; i++) if (fScore[open[i]] < fScore[open[best]]) best = i;
            var current = open[best];
            if (current == goal) { found = true; break; }
            open.RemoveAt(best);
            foreach (var offset in Neighbors)
            {
                var next = current + offset;
                if (!InBounds(next) || IsBlocked(next, clearance)) continue;
                if (offset.x != 0 && offset.y != 0 && (IsBlocked(current + new Vector2Int(offset.x, 0), clearance) || IsBlocked(current + new Vector2Int(0, offset.y), clearance))) continue;
                var tentative = gScore[current] + (offset.x != 0 && offset.y != 0 ? 1.414f : 1f);
                if (gScore.TryGetValue(next, out var known) && tentative >= known) continue;
                cameFrom[next] = current; gScore[next] = tentative; fScore[next] = tentative + Heuristic(next, goal);
                if (!open.Contains(next)) open.Add(next);
            }
        }
        if (!found) { output.points.Add(target); return; }
        var cells = new List<Vector2Int> { goal };
        var cursor = goal;
        while (cursor != start && cameFrom.TryGetValue(cursor, out var previous)) { cursor = previous; cells.Add(cursor); }
        cells.Reverse();
        for (var i = 1; i < cells.Count; i++) output.points.Add(new Vector3(cells[i].x, origin.y, cells[i].y));
        if (output.points.Count == 0) output.points.Add(target);
    }

    private static Vector3 DirectDirection(Vector3 origin, Vector3 target)
    {
        var desired = target - origin; desired.y = 0f;
        return desired.sqrMagnitude < .01f ? Vector3.zero : desired.normalized;
    }

    private static Vector2Int FindOpenCell(Vector2Int center, float clearance)
    {
        if (!IsBlocked(center, clearance)) return center;
        for (var radius = 1; radius <= 4; radius++)
        for (var x = -radius; x <= radius; x++)
        for (var y = -radius; y <= radius; y++)
        {
            var candidate = center + new Vector2Int(x, y);
            if (InBounds(candidate) && !IsBlocked(candidate, clearance)) return candidate;
        }
        return center;
    }

    private static bool IsBlocked(Vector2Int cell, float clearance)
    {
        foreach (var collider in Physics.OverlapSphere(new Vector3(cell.x, .45f, cell.y), clearance))
            if (collider.GetComponentInParent<ObstacleMarker>() != null) return true;
        return false;
    }

    private static Vector2Int ToCell(Vector3 p) => new Vector2Int(Mathf.Clamp(Mathf.RoundToInt(p.x), MinCell, MaxCell), Mathf.Clamp(Mathf.RoundToInt(p.z), MinCell, MaxCell));
    private static bool InBounds(Vector2Int cell) => cell.x >= MinCell && cell.x <= MaxCell && cell.y >= MinCell && cell.y <= MaxCell;
    private static float Heuristic(Vector2Int a, Vector2Int b) => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    private static float FlatDistance(Vector3 a, Vector3 b) { a.y = 0f; b.y = 0f; return Vector3.Distance(a, b); }
}
