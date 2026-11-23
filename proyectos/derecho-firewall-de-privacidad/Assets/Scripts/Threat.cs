using UnityEngine;

public enum ThreatType { Intruder, RemoteLeak, ArmoredImpostor, Interceptor, CenterSaboteur }

public sealed class Threat : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int health = 2;
    private int maxHealth = 2;
    [SerializeField] private float attackCooldown = 1f;
    private Transform target;
    private Transform center;
    private GameSession session;
    private float attackTimer;
    private GameObject healthBar;
    private GameObject healthFill;
    private const int MaxHealth = 2;
    private Renderer bodyRenderer;
    private Color bodyColor;
    private float hitFlashTimer;
    private float vulnerableTimer;
    private float aggroTimer;
    private Transform aggroTarget;
    private float remoteShotTimer;
    private float warningTimer;
    private ThreatType archetype;
    public ThreatType Type => archetype;

    private void Start()
    {
        bodyRenderer = GetComponent<Renderer>();
        bodyColor = bodyRenderer != null ? bodyRenderer.material.color : Color.red;
        CreateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        if (archetype == ThreatType.ArmoredImpostor && vulnerableTimer <= 0f) return;
        health -= Mathf.Max(0, amount);
        hitFlashTimer = .12f;
        if (bodyRenderer != null) bodyRenderer.material.color = Color.white;
        session.ThreatDamaged(transform.position);
        aggroTarget = target;
        aggroTimer = 3.5f;
        UpdateHealthBar();
        if (health <= 0)
        {
            session.ThreatDefeated(transform.position);
            Destroy(gameObject);
        }
    }

    private void CreateHealthBar()
    {
        healthBar = GameObject.CreatePrimitive(PrimitiveType.Cube);
        healthBar.name = "EnemyHealthBarBackground";
        healthBar.transform.SetParent(transform, false);
        healthBar.transform.localPosition = new Vector3(0f, 1.5f, 0f);
        healthBar.transform.localScale = new Vector3(1.55f, .18f, .08f);
        PrimitiveMaterial.Apply(healthBar.GetComponent<Renderer>(), new Color(.05f, .05f, .05f));
        healthBar.GetComponent<Collider>().enabled = false;

        healthFill = GameObject.CreatePrimitive(PrimitiveType.Cube);
        healthFill.name = "EnemyHealthBarFill";
        healthFill.transform.SetParent(transform, false);
        healthFill.transform.localPosition = new Vector3(-.775f, 1.5f, -.015f);
        healthFill.transform.localScale = new Vector3(1.55f, .18f, .08f);
        PrimitiveMaterial.Apply(healthFill.GetComponent<Renderer>(), new Color(.2f, .95f, .25f));
        healthFill.GetComponent<Collider>().enabled = false;
    }

    private void UpdateHealthBar()
    {
        if (healthFill == null) return;
        var ratio = Mathf.Clamp01((float)health / maxHealth);
        healthFill.transform.localScale = new Vector3(1.55f * ratio, .18f, .08f);
        healthFill.transform.localPosition = new Vector3(-.775f + (.775f * ratio), 1.5f, -.015f);
    }

    private void LateUpdate()
    {
        if (Camera.main == null) return;
        if (healthBar != null) healthBar.transform.rotation = Camera.main.transform.rotation;
        if (healthFill != null) healthFill.transform.rotation = Camera.main.transform.rotation;
    }

    public void Configure(Transform guardian, Transform dataCenter, GameSession owner) { target = guardian; guardianTransform = guardian; center = dataCenter; session = owner; }

    private void Update()
    {
        if (hitFlashTimer > 0f)
        {
            hitFlashTimer -= Time.deltaTime;
            if (hitFlashTimer <= 0f && bodyRenderer != null) bodyRenderer.material.color = bodyColor;
        }
        if (archetype == ThreatType.ArmoredImpostor && hitFlashTimer <= 0f && bodyRenderer != null)
            bodyRenderer.material.color = vulnerableTimer > 0f ? Color.yellow : bodyColor;
        else if (warningTimer <= 0f && hitFlashTimer <= 0f && bodyRenderer != null)
            bodyRenderer.material.color = bodyColor;
        if (target == null || center == null) return;
        attackTimer -= Time.deltaTime;
        vulnerableTimer = Mathf.Max(0f, vulnerableTimer - Time.deltaTime);
        aggroTimer = Mathf.Max(0f, aggroTimer - Time.deltaTime);
        if (aggroTimer <= 0f) aggroTarget = null;
        remoteShotTimer = Mathf.Max(0f, remoteShotTimer - Time.deltaTime);
        warningTimer = Mathf.Max(0f, warningTimer - Time.deltaTime);
        if (warningTimer > 0f && bodyRenderer != null && hitFlashTimer <= 0f) bodyRenderer.material.color = Color.yellow;
        if (archetype == ThreatType.RemoteLeak && remoteShotTimer <= 0f && Vector3.Distance(transform.position, target.position) < 11f)
        {
            FireRemoteShot();
            remoteShotTimer = Mathf.Max(.9f, attackCooldown + .35f);
        }
        var destination = SelectDestination();
        var direction = NpcNavigation.GetDirection(transform, destination, .45f);
        if (archetype == ThreatType.RemoteLeak && Vector3.Distance(transform.position, target.position) < 4f)
            direction = -direction;
        transform.position = WorldCollision.Move(transform.position, direction * speed * Time.deltaTime, .43f);
        if (Vector3.Distance(transform.position, destination) < 1f)
        {
            if (attackTimer > 0f && attackTimer <= .3f) warningTimer = .3f;
            if (attackTimer <= 0f)
            {
                var request = FindRequest(destination);
                if (request != null)
                {
                    session.RequestLost(request);
                    Destroy(request.gameObject);
                }
                else if (destination == center.position) session.ThreatHitCenter(damage);
                else
                {
                    var wasShielding = target == guardianTransform && target != null && target.GetComponent<GuardianController>() != null && target.GetComponent<GuardianController>().IsShielding;
                    session.ThreatHitGuardian(damage);
                    if (archetype == ThreatType.ArmoredImpostor && wasShielding) vulnerableTimer = 1f;
                }
                attackTimer = attackCooldown;
            }
            return;
        }
    }

    public void Configure(Transform guardian, Transform dataCenter, GameSession owner, ThreatType type)
    {
        target = guardian; guardianTransform = guardian; center = dataCenter; session = owner; archetype = type;
        var waveBonus = Mathf.FloorToInt(Mathf.Max(0, owner.Wave - 1) / 5f);
        maxHealth = (type == ThreatType.ArmoredImpostor ? 3 : 2) + waveBonus;
        health = maxHealth;
        speed = type == ThreatType.Interceptor ? 2.8f : type == ThreatType.RemoteLeak ? 1.4f : speed;
        speed *= 1f + Mathf.Min((owner.Wave - 1) * .035f, .75f);
        damage = 1 + Mathf.FloorToInt(Mathf.Max(0, owner.Wave - 1) / 8f);
    }

    private Vector3 SelectDestination()
    {
        if (aggroTarget != null) return aggroTarget.position;
        if (archetype == ThreatType.CenterSaboteur) return center.position;
        if (archetype == ThreatType.Interceptor)
        {
            var requests = FindObjectsOfType<LegitimateRequest>();
            if (requests.Length > 0)
            {
                var nearest = requests[0];
                var distance = Vector3.Distance(transform.position, nearest.transform.position);
                foreach (var request in requests)
                {
                    var candidateDistance = Vector3.Distance(transform.position, request.transform.position);
                    if (candidateDistance < distance) { nearest = request; distance = candidateDistance; }
                }
                return nearest.transform.position;
            }
        }
        var guardianDistance = Vector3.Distance(transform.position, target.position);
        var centerDistance = Vector3.Distance(transform.position, center.position);
        return centerDistance < guardianDistance ? center.position : target.position;
    }

    private Transform guardianTransform;

    private void FireRemoteShot()
    {
        var shotObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shotObject.name = "RemoteLeakProjectile";
        shotObject.transform.position = transform.position + Vector3.up * .35f;
        shotObject.transform.localScale = Vector3.one * .22f;
        PrimitiveMaterial.Apply(shotObject.GetComponent<Renderer>(), new Color(1f, .18f, .45f));
        shotObject.GetComponent<Collider>().enabled = false;
        shotObject.AddComponent<ThreatProjectile>().Configure(target, center, session, damage);
    }

    private LegitimateRequest FindRequest(Vector3 destination)
    {
        if (archetype != ThreatType.Interceptor) return null;
        foreach (var request in FindObjectsOfType<LegitimateRequest>())
            if (Vector3.Distance(request.transform.position, destination) < 1.2f) return request;
        return null;
    }
}

