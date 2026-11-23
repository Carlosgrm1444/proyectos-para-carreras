using UnityEngine;

public sealed class CombatAudio : MonoBehaviour
{
    private AudioSource source;
    private AudioClip swordClip;
    private AudioClip bowClip;
    private AudioClip shieldClip;
    private AudioClip counterClip;
    private AudioClip savedClip;
    private AudioClip lostClip;
    private AudioClip hitClip;
    private AudioClip defeatedClip;
    private AudioClip gameOverClip;
    private AudioClip shieldBreakClip;
    private AudioClip powerUpClip;
    private AudioClip waveClip;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.volume = .35f;
        source.priority = 32;
        swordClip = CreateTone("Sword", 520f, 160f, .12f);
        bowClip = CreateTone("Bow", 760f, 320f, .16f);
        shieldClip = CreateTone("Shield", 180f, 360f, .18f);
        counterClip = CreateTone("Counter", 900f, 450f, .22f);
        savedClip = CreateTone("RequestSaved", 640f, 980f, .16f);
        lostClip = CreateTone("RequestLost", 260f, 90f, .2f);
        hitClip = CreateTone("Impact", 110f, 70f, .1f);
        defeatedClip = CreateTone("Defeated", 480f, 120f, .18f);
        gameOverClip = CreateTone("GameOver", 180f, 45f, .45f);
        shieldBreakClip = CreateTone("ShieldBreak", 620f, 80f, .24f);
        powerUpClip = CreateTone("PowerUp", 420f, 980f, .24f);
        waveClip = CreateTone("Wave", 260f, 720f, .38f);
    }

    public void Sword() => source.PlayOneShot(swordClip);
    public void Bow() => source.PlayOneShot(bowClip);
    public void ShieldBlock() => source.PlayOneShot(shieldClip);
    public void CounterReady() => source.PlayOneShot(counterClip);
    public void RequestSaved() => source.PlayOneShot(savedClip);
    public void RequestLost() => source.PlayOneShot(lostClip);
    public void EnemyHit() => source.PlayOneShot(hitClip);
    public void EnemyDefeated() => source.PlayOneShot(defeatedClip);
    public void GameOver() => source.PlayOneShot(gameOverClip);
    public void ShieldBreak() => source.PlayOneShot(shieldBreakClip);
    public void PowerUp() => source.PlayOneShot(powerUpClip);
    public void WaveStarted() => source.PlayOneShot(waveClip);
    public void SetMuted(bool value) => source.mute = value;

    private static AudioClip CreateTone(string name, float startFrequency, float endFrequency, float duration)
    {
        const int sampleRate = 44100;
        var samples = Mathf.CeilToInt(sampleRate * duration);
        var data = new float[samples];
        for (var i = 0; i < samples; i++)
        {
            var t = i / (float)samples;
            var frequency = Mathf.Lerp(startFrequency, endFrequency, t);
            var envelope = Mathf.Sin(Mathf.PI * t);
            data[i] = Mathf.Sin(2f * Mathf.PI * frequency * i / sampleRate) * envelope * .45f;
        }
        var clip = AudioClip.Create(name, samples, 1, sampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }
}
