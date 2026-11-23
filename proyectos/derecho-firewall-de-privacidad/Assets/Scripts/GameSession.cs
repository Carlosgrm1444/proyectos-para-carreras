using UnityEngine;

public sealed class GameSession : MonoBehaviour
{
    private GuardianController guardian;
    private DataCenter center;
    private EntitySpawner spawner;
    private int score;
    private bool gameOver;
    private CombatAudio combatAudio;
    private bool gameOverSoundPlayed;
    private int streak;
    private int bestScore;
    private bool paused;
    private bool muted;
    private string powerUpMessage;
    private float powerUpMessageTimer;
    private GameJuice juice;
    private MusicController music;
    private float sessionTimer;
    private bool timeExpired;

    public int Score => score;
    public int Streak => streak;
    public int BestScore => bestScore;
    public int Wave => spawner == null ? 1 : spawner.Wave;
    public bool IsGameOver => gameOver;
    public bool IsPaused => paused;
    public bool IsMuted => muted;
    public bool IsTimeExpired => timeExpired;
    public float TimeRemaining => 0f;
    public float TimeElapsed => sessionTimer;
    public string PowerUpMessage => powerUpMessage;
    public bool IsPowerUpMessageVisible => powerUpMessageTimer > 0f;
    public GuardianController Guardian => guardian;
    public DataCenter Center => center;

    public void Configure(GuardianController player, DataCenter dataCenter, EntitySpawner entitySpawner, GameJuice gameJuice = null)
    {
        guardian = player; center = dataCenter; spawner = entitySpawner; juice = gameJuice; spawner.BindSession(this); combatAudio = GetComponent<CombatAudio>(); music = GetComponent<MusicController>();
        bestScore = PlayerPrefs.GetInt("DerechoBestScore", 0);
    }

    private void Update()
    {
        if (TutorialDirector.IsBlockingInput) return;
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            TogglePause();
            return;
        }
        powerUpMessageTimer = Mathf.Max(0f, powerUpMessageTimer - Time.deltaTime);
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7)) Restart();
            return;
        }
        if (paused) return;
        sessionTimer += Time.deltaTime;
        if ((guardian.Health <= 0 || center.Health <= 0) && !gameOver)
        {
            gameOver = true;
            if (!gameOverSoundPlayed) { SaveBestScore(); combatAudio?.GameOver(); gameOverSoundPlayed = true; }
        }
    }

    public void RequestSaved(LegitimateRequest request) { if (!gameOver) { score += 10; streak++; combatAudio?.RequestSaved(); juice?.Popup("+10 SOLICITUD PROTEGIDA", Color.green); juice?.Burst(center.transform.position + Vector3.up, Color.green, .8f); GameHaptics.Pulse(.15f, .2f, .08f); } }
    public void RequestLost(LegitimateRequest request) { if (!gameOver) { score -= 5; streak = 0; combatAudio?.RequestLost(); juice?.Popup("-5 SOLICITUD PERDIDA", Color.red); juice?.Flash(Color.red, .18f); GameHaptics.Pulse(.35f, .1f, .12f); } }
    public void ThreatHitGuardian(int amount)
    {
        if (gameOver) return;
        if (guardian.IsShielding)
        {
            guardian.RegisterBlockedHit();
            combatAudio?.ShieldBlock();
            return;
        }
        if (guardian.TryTakeDamage(amount)) { combatAudio?.EnemyHit(); juice?.Shake(.12f, .12f); juice?.Flash(Color.red, .12f); GameHaptics.Pulse(.3f, .15f, .1f); }
    }
    public void ThreatHitCenter(int amount) { if (!gameOver) { center.TakeDamage(amount); combatAudio?.EnemyHit(); juice?.Shake(.16f, .16f); juice?.Flash(new Color(1f, .2f, .05f), .16f); GameHaptics.Pulse(.4f, .25f, .14f); } }
    public void ThreatDefeated(Vector3 position)
    {
        if (!gameOver) { score += 2; streak++; spawner.TrySpawnPowerUp(position, streak); combatAudio?.EnemyDefeated(); juice?.Popup("+2 AMENAZA NEUTRALIZADA", Color.white); juice?.Burst(position + Vector3.up, Color.yellow, .7f); GameHaptics.Pulse(.12f, .35f, .1f); }
    }
    public void ThreatDamaged(Vector3 position)
    {
        if (gameOver) return;
        combatAudio?.EnemyHit();
        juice?.Burst(position + Vector3.up * .5f, Color.white, .28f);
        juice?.Shake(.025f, .05f);
        GameHaptics.Pulse(.06f, .1f, .04f);
    }
    public void ApplyPowerUp(PowerUpType type)
    {
        if (type == PowerUpType.Heal) { guardian.Heal(2); center.Heal(2); powerUpMessage = "CURACIÓN CONSUMIDA: +2 GUARDIÁN / +2 CENTRO"; }
        if (type == PowerUpType.Speed) { guardian.ApplySpeedBoost(8f); powerUpMessage = "VELOCIDAD ACTIVA: 8 s"; }
        if (type == PowerUpType.Damage) { FindObjectOfType<GuardianWeapons>()?.ApplyDamageBoost(8f); powerUpMessage = "DAÑO ACTIVO: 8 s"; }
        powerUpMessageTimer = 2.5f;
        juice?.Popup(powerUpMessage, type == PowerUpType.Heal ? Color.green : type == PowerUpType.Speed ? Color.yellow : Color.magenta); juice?.Flash(Color.white, .08f); GameHaptics.Pulse(.3f, .5f, .18f);
    }
    public void PlaySwordSound() => combatAudio?.Sword();
    public void PlayBowSound() => combatAudio?.Bow();
    public void PlayCounterSound() { combatAudio?.CounterReady(); juice?.ActionFeedback("CONTRAATAQUE LISTO · RB", Color.white); }
    public void PlayShieldBreakSound() => combatAudio?.ShieldBreak();
    public void PlayPowerUpSound() => combatAudio?.PowerUp();
    public void PlayWaveSound() => combatAudio?.WaveStarted();
    public void WaveStarted(int wave)
    {
        if (wave <= 1) return;
        combatAudio?.WaveStarted();
        juice?.Popup($"OLEADA {wave}", new Color(1f, .55f, .1f));
        juice?.Flash(new Color(1f, .35f, .05f), .08f);
        GameHaptics.Pulse(.12f, .28f, .12f);
    }
    public void JuiceSword(Vector3 position) { juice?.Shake(.04f, .08f); juice?.Burst(position + Vector3.up * .5f, Color.yellow, .55f); juice?.ActionFeedback("ESPADA · IMPACTO", Color.yellow); GameHaptics.Pulse(.08f, .15f, .06f); }
    public void JuiceBow(Vector3 position) { juice?.Burst(position + Vector3.up * .3f, Color.magenta, .35f); juice?.ActionFeedback("ARCO · SOLICITUD A.R.C.O.", Color.magenta); }
    public void JuiceShieldBreak(Vector3 position) { juice?.Shake(.15f, .16f); juice?.Flash(new Color(.2f, .7f, 1f), .14f); juice?.Burst(position + Vector3.up, Color.cyan, 1.2f); juice?.ActionFeedback("ESCUDO · SE ROMPIÓ", Color.cyan); GameHaptics.Pulse(.5f, .2f, .16f); }

    public void TogglePause()
    {
        if (gameOver) return;
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
    }

    public void ToggleMute()
    {
        muted = !muted;
        combatAudio?.SetMuted(muted);
        music?.SetMuted(muted);
    }

    public void QuitGame()
    {
        if (!paused) return;
        SaveBestScore();
        Application.Quit();
    }

    public void Restart()
    {
        foreach (var request in FindObjectsOfType<LegitimateRequest>()) Destroy(request.gameObject);
        foreach (var threat in FindObjectsOfType<Threat>()) Destroy(threat.gameObject);
        Time.timeScale = 1f; paused = false; guardian.ResetHealth(); center.ResetHealth(); score = 0; streak = 0; sessionTimer = 0f; timeExpired = false; gameOver = false; gameOverSoundPlayed = false; powerUpMessage = null; powerUpMessageTimer = 0f; spawner.ResetTimers();
    }

    private void SaveBestScore()
    {
        if (score > bestScore) { bestScore = score; PlayerPrefs.SetInt("DerechoBestScore", bestScore); PlayerPrefs.Save(); }
    }
}
