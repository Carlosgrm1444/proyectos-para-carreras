using UnityEngine;
using UnityEngine.Rendering;

public sealed class GuardianWeapons : MonoBehaviour
{
    [SerializeField] private float swordRadius = 2.25f;
    [SerializeField] private int swordDamage = 1;
    [SerializeField] private float swordCooldown = .25f;
    [SerializeField] private float bowCooldown = 0.45f;
    [SerializeField] private float shieldDuration = 1.5f;
    [SerializeField] private float shieldCooldown = 2f;
    private GuardianController guardian;
    private GameSession session;
    private float bowTimer;
    private float swordTimer;
    private bool counterReady;
    private float damageBoostTimer;
    private float shieldTimer;
    private float shieldCooldownTimer;
    private float actionLockTimer;
    private float counterTimer;
    private GameObject shieldVisual;
    public bool CounterReady { get; private set; }
    public float DamageBoostRemaining => damageBoostTimer;
    public float ShieldCooldownRemaining => shieldCooldownTimer;
    public float ShieldRemaining => Mathf.Max(0f, shieldTimer);

    public void Configure(GameSession owner)
    {
        session = owner;
        guardian = GetComponent<GuardianController>();
        shieldVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        shieldVisual.name = "ConsentShieldVisual";
        shieldVisual.transform.SetParent(transform, false);
        shieldVisual.transform.localPosition = Vector3.up;
        shieldVisual.transform.localScale = new Vector3(2.6f, 2.1f, 2.6f);
        var shieldMaterial = PrimitiveMaterial.Apply(shieldVisual.GetComponent<Renderer>(), new Color(.1f, .65f, 1f, .35f));
        ConfigureTransparent(shieldMaterial);
        shieldVisual.GetComponent<Collider>().enabled = false;
        shieldVisual.SetActive(false);
    }

    private void Update()
    {
        if (session == null || session.IsGameOver) return;
        if (TutorialDirector.IsBlockingInput) return;
        bowTimer -= Time.deltaTime;
        swordTimer -= Time.deltaTime;
        damageBoostTimer = Mathf.Max(0f, damageBoostTimer - Time.deltaTime);
        shieldCooldownTimer = Mathf.Max(0f, shieldCooldownTimer - Time.deltaTime);
        actionLockTimer = Mathf.Max(0f, actionLockTimer - Time.deltaTime);
        counterTimer = Mathf.Max(0f, counterTimer - Time.deltaTime);
        if (counterTimer <= 0f) { counterReady = false; CounterReady = false; }
        if (guardian.ConsumeBlockedHit())
        {
            counterReady = true;
            CounterReady = true;
            counterTimer = 1.25f;
            session.PlayCounterSound();
            GameHaptics.Pulse(.18f, .62f, .22f);
        }
        var swordPressed = Input.GetKeyDown(KeyCode.JoystickButton5);
        var bowPressed = Input.GetAxisRaw("RightTrigger") > .35f;
        var shieldHeld = Input.GetKey(KeyCode.JoystickButton4) || Input.GetAxisRaw("LeftTrigger") < -.35f;

        if (guardian.IsShielding)
        {
            shieldTimer -= Time.deltaTime;
            UpdateShieldVisual();
            if (shieldTimer <= 0f || !shieldHeld) BreakShield();
            return;
        }

        shieldVisual.SetActive(false);
        if (actionLockTimer > 0f) return;
        // Una sola acción por actualización: espada > arco > escudo.
        if (swordPressed && swordTimer <= 0f) SwordAttack();
        else if (bowPressed && bowTimer <= 0f) BowAttack();
        else if (shieldHeld && shieldCooldownTimer <= 0f) StartShield();
    }

    private void SwordAttack()
    {
        swordTimer = swordCooldown;
        actionLockTimer = .12f;
        var wasCounter = counterReady;
        var damage = Mathf.RoundToInt(swordDamage * (damageBoostTimer > 0f ? 1.5f : 1f)) + (wasCounter ? 1 : 0);
        counterReady = false;
        CounterReady = false;
        session.PlaySwordSound();
        session.JuiceSword(transform.position + guardian.AimDirection * 1.25f);
        foreach (var threat in FindObjectsOfType<Threat>())
        {
            if (Vector3.Distance(transform.position, threat.transform.position) <= swordRadius)
                threat.TakeDamage(damage);
        }
        var slash = GameObject.CreatePrimitive(PrimitiveType.Cube);
        slash.name = "FirewallSwordSlash";
        slash.transform.position = transform.position + guardian.AimDirection * 1.25f + Vector3.up * .65f;
        slash.transform.rotation = Quaternion.LookRotation(guardian.AimDirection);
        slash.transform.localScale = new Vector3(.35f, .25f, 1.5f);
        PrimitiveMaterial.Apply(slash.GetComponent<Renderer>(), wasCounter ? Color.white : new Color(1f, .85f, .2f));
        slash.GetComponent<Collider>().enabled = false;
        Destroy(slash, .12f);
    }

    public void ApplyDamageBoost(float duration) => damageBoostTimer = Mathf.Max(damageBoostTimer, duration);

    private void BowAttack()
    {
        bowTimer = bowCooldown;
        actionLockTimer = .12f;
        session.PlayBowSound();
        session.JuiceBow(transform.position + guardian.AimDirection * 1.1f);
        var projectileObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        projectileObject.name = "ARCOProjectile";
        projectileObject.transform.position = transform.position + guardian.AimDirection * 1.1f + Vector3.up * .2f;
        projectileObject.transform.localScale = new Vector3(.28f, .8f, .28f);
        projectileObject.transform.rotation = Quaternion.LookRotation(guardian.AimDirection);
        PrimitiveMaterial.Apply(projectileObject.GetComponent<Renderer>(), Color.magenta);
        projectileObject.AddComponent<Projectile>().Configure(guardian.AimDirection, session);
    }

    private void StartShield()
    {
        shieldTimer = shieldDuration;
        guardian.SetShielding(true);
        shieldVisual.SetActive(true);
        UpdateShieldVisual();
    }

    private void UpdateShieldVisual()
    {
        var ratio = Mathf.Clamp01(shieldTimer / shieldDuration);
        var color = new Color(.1f, .65f, 1f, Mathf.Lerp(.08f, .48f, ratio));
        shieldVisual.GetComponent<Renderer>().material.color = color;
        shieldVisual.transform.localScale = Vector3.one * Mathf.Lerp(2.1f, 2.6f, ratio);
    }

    private void BreakShield()
    {
        guardian.SetShielding(false);
        shieldCooldownTimer = shieldCooldown;
        shieldVisual.SetActive(false);
        session.PlayShieldBreakSound();
        session.JuiceShieldBreak(transform.position);
        var burst = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        burst.name = "ShieldBreakEffect";
        burst.transform.position = transform.position + Vector3.up;
        burst.transform.localScale = Vector3.one * 2.8f;
        var burstMaterial = PrimitiveMaterial.Apply(burst.GetComponent<Renderer>(), new Color(.25f, .8f, 1f, .8f));
        ConfigureTransparent(burstMaterial);
        burst.GetComponent<Collider>().enabled = false;
        Destroy(burst, .16f);
    }

    private static void ConfigureTransparent(Material material)
    {
        material.SetFloat("_Surface", 1f);
        material.SetFloat("_Mode", 3f);
        material.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.renderQueue = 3000;
    }
}

