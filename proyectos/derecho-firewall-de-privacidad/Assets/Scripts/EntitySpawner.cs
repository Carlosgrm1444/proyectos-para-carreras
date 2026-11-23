using UnityEngine;

public sealed class EntitySpawner : MonoBehaviour
{
    [SerializeField] private float requestInterval = 5f;
    [SerializeField] private float threatInterval = 7f;
    private Transform center;
    private Transform guardian;
    private GameSession session;
    private float requestTimer;
    private float threatTimer;
    private float waveTimer;
    private int wave = 1;
    private int lastSpawnSide = -1;
    public int Wave => wave;

    public void Configure(Transform dataCenter, Transform targetGuardian) { center = dataCenter; guardian = targetGuardian; }
    public void BindSession(GameSession owner) { session = owner; }

    private void Update()
    {
        if (session == null || session.IsGameOver) return;
        requestTimer += Time.deltaTime;
        threatTimer += Time.deltaTime;
        waveTimer += Time.deltaTime;
        if (waveTimer >= 20f) { waveTimer = 0f; wave++; session.WaveStarted(wave); }
        var requestCadence = Mathf.Max(1.35f, requestInterval - (wave - 1) * .4f);
        var threatCadence = Mathf.Max(1.6f, threatInterval - (wave - 1) * .5f);
        if (requestTimer >= requestCadence) { requestTimer = 0f; SpawnRequest(); if (wave >= 6 && wave % 3 == 0) SpawnRequest(); }
        if (threatTimer >= threatCadence) { threatTimer = 0f; SpawnThreat(); if (wave >= 5 && wave % 4 == 1) SpawnThreat(); }
    }

    public void ResetTimers() { requestTimer = 0f; threatTimer = 0f; waveTimer = 0f; wave = 1; lastSpawnSide = -1; }

    public void TrySpawnPowerUp(Vector3 position, int streak)
    {
        var chance = Mathf.Clamp01(.08f + Mathf.Min(streak * .02f, .12f));
        if (Random.value > chance) return;
        var power = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        power.name = "PowerUp";
        power.transform.position = position + Vector3.up * .4f;
        var type = (PowerUpType)Random.Range(0, 3);
        PrimitiveMaterial.Apply(power.GetComponent<Renderer>(), type == PowerUpType.Heal ? Color.green : type == PowerUpType.Speed ? Color.yellow : Color.magenta);
        power.AddComponent<PowerUp>().Configure(type, session);
    }

    private void SpawnRequest()
    {
        var request = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        request.name = "LegitimateRequest";
        request.transform.position = GetSpawnPosition(.5f);
        PrimitiveMaterial.Apply(request.GetComponent<Renderer>(), Color.green);
        request.AddComponent<LegitimateRequest>().Configure(center, session);
    }

    private void SpawnThreat()
    {
        var threat = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var type = (ThreatType)Random.Range(0, 5);
        threat.name = $"Threat_{type}";
        threat.transform.position = GetSpawnPosition(.5f);
        var colors = new[] { Color.red, Color.magenta, new Color(.55f, .2f, .8f), new Color(1f, .45f, .05f), new Color(.8f, .1f, .1f) };
        PrimitiveMaterial.Apply(threat.GetComponent<Renderer>(), colors[(int)type]);
        CreateThreatMarker(threat.transform, type);
        threat.AddComponent<Threat>().Configure(guardian, center, session, type);
    }

    private static void CreateThreatMarker(Transform owner, ThreatType type)
    {
        var primitive = type == ThreatType.RemoteLeak ? PrimitiveType.Sphere :
                        type == ThreatType.ArmoredImpostor ? PrimitiveType.Cylinder :
                        type == ThreatType.Interceptor ? PrimitiveType.Capsule : PrimitiveType.Cube;
        var marker = GameObject.CreatePrimitive(primitive);
        marker.name = $"ThreatMarker_{type}";
        marker.transform.SetParent(owner, false);
        marker.transform.localPosition = new Vector3(0f, .82f, 0f);
        marker.transform.localScale = type == ThreatType.ArmoredImpostor ? new Vector3(.72f, .12f, .72f) :
                                      type == ThreatType.Interceptor ? new Vector3(.22f, .42f, .22f) :
                                      type == ThreatType.CenterSaboteur ? new Vector3(.62f, .18f, .62f) : Vector3.one * .34f;
        if (type == ThreatType.Intruder) marker.transform.localRotation = Quaternion.Euler(0f, 45f, 45f);
        PrimitiveMaterial.Apply(marker.GetComponent<Renderer>(), new Color(.92f, .96f, 1f));
        marker.GetComponent<Collider>().enabled = false;
    }

    private Vector3 GetSpawnPosition(float height)
    {
        // Alterna los cuatro lados para distribuir las apariciones por todo el mapa.
        var side = Random.Range(0, 4);
        if (side == lastSpawnSide) side = (side + 1) % 4;
        lastSpawnSide = side;

        const float edge = 13.5f;
        const float spread = 11f;
        var offset = Random.Range(-spread, spread);
        switch (side)
        {
            case 0: return new Vector3(offset, height, edge);   // norte
            case 1: return new Vector3(edge, height, offset);   // este
            case 2: return new Vector3(offset, height, -edge);  // sur
            default: return new Vector3(-edge, height, offset); // oeste
        }
    }
}
