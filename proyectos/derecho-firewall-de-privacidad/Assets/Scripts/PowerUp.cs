using UnityEngine;

public enum PowerUpType { Heal, Speed, Damage }

public sealed class PowerUp : MonoBehaviour
{
    private PowerUpType type;
    private GameSession session;
    private Vector3 baseScale;
    private Renderer powerRenderer;

    public void Configure(PowerUpType powerUpType, GameSession owner)
    {
        type = powerUpType;
        session = owner;
        powerRenderer = GetComponent<Renderer>();
        baseScale = transform.localScale;
        if (powerRenderer != null)
            PrimitiveMaterial.Apply(powerRenderer, type == PowerUpType.Heal ? Color.green : type == PowerUpType.Speed ? Color.yellow : Color.magenta);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 90f * Time.deltaTime);
        var pulse = 1f + Mathf.Sin(Time.time * 5f) * .12f;
        transform.localScale = baseScale * pulse;
        if (session != null && session.Guardian != null && Vector3.Distance(transform.position, session.Guardian.transform.position) < 1.3f)
        {
            session.ApplyPowerUp(type);
            CreateCollectEffect();
            session.PlayPowerUpSound();
            Destroy(gameObject);
        }
    }

    private void CreateCollectEffect()
    {
        var effect = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        effect.name = "PowerUpCollectEffect";
        effect.transform.position = transform.position;
        effect.transform.localScale = Vector3.one * 1.4f;
        var renderer = effect.GetComponent<Renderer>();
        PrimitiveMaterial.Apply(renderer, type == PowerUpType.Heal ? Color.green : type == PowerUpType.Speed ? Color.yellow : Color.magenta);
        effect.GetComponent<Collider>().enabled = false;
        Destroy(effect, .2f);
    }
}

