using UnityEngine;

public sealed class GuardianController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float arenaLimit = 15f;
    [SerializeField] private int maxHealth = 5;
    private int health;
    private Vector3 aimDirection = Vector3.forward;
    private float invulnerabilityTimer;
    private bool blockedHitPending;
    private float speedBoostTimer;
    private bool shieldActive;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public float SpeedBoostRemaining => speedBoostTimer;
    public Vector3 AimDirection => aimDirection;
    public bool IsShielding => shieldActive;
    public bool IsInvulnerable => invulnerabilityTimer > 0f;
    public bool ConsumeBlockedHit()
    {
        var wasBlocked = blockedHitPending;
        blockedHitPending = false;
        return wasBlocked;
    }

    private void Awake() => health = maxHealth;

    private void Update()
    {
        invulnerabilityTimer = Mathf.Max(0f, invulnerabilityTimer - Time.deltaTime);
        speedBoostTimer = Mathf.Max(0f, speedBoostTimer - Time.deltaTime);
        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.sqrMagnitude > 1f) input.Normalize();
        var rightStick = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
        if (rightStick.sqrMagnitude > 0.12f) aimDirection = new Vector3(rightStick.x, 0f, rightStick.y).normalized;
        else if (input.sqrMagnitude > 0.01f) aimDirection = new Vector3(input.x, 0f, input.y);
        if (aimDirection.sqrMagnitude > 0.01f) transform.rotation = Quaternion.LookRotation(aimDirection, Vector3.up);
        var speedMultiplier = speedBoostTimer > 0f ? 1.35f : 1f;
        transform.position = WorldCollision.Move(transform.position, new Vector3(input.x, 0f, input.y) * moveSpeed * speedMultiplier * Time.deltaTime, .42f);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -arenaLimit, arenaLimit), transform.position.y, Mathf.Clamp(transform.position.z, -arenaLimit, arenaLimit));
    }

    public void SetHealth(int value) { maxHealth = Mathf.Max(1, value); health = maxHealth; }
    public void Heal(int amount) { health = Mathf.Min(maxHealth, health + Mathf.Max(0, amount)); }
    public bool TryTakeDamage(int amount)
    {
        if (IsInvulnerable) return false;
        health = Mathf.Max(0, health - Mathf.Max(0, amount));
        invulnerabilityTimer = .35f;
        return true;
    }

    public void RegisterBlockedHit() => blockedHitPending = true;
    public void SetShielding(bool value) => shieldActive = value;
    public void ApplySpeedBoost(float duration) => speedBoostTimer = Mathf.Max(speedBoostTimer, duration);
    public void ResetHealth() { health = maxHealth; invulnerabilityTimer = 0f; blockedHitPending = false; speedBoostTimer = 0f; shieldActive = false; }
}

