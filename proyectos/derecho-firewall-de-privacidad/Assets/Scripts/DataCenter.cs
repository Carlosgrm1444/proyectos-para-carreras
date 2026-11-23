using UnityEngine;

public sealed class DataCenter : MonoBehaviour
{
    private int maxHealth = 10;
    private int health;
    public int Health => health;
    public int MaxHealth => maxHealth;

    private void Awake() => health = maxHealth;
    public void SetMaxHealth(int value) { maxHealth = Mathf.Max(1, value); health = maxHealth; }
    public void TakeDamage(int amount) { health = Mathf.Max(0, health - Mathf.Max(0, amount)); }
    public void Heal(int amount) { health = Mathf.Min(maxHealth, health + Mathf.Max(0, amount)); }
    public void ResetHealth() => health = maxHealth;
}

