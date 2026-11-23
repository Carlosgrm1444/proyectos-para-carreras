using UnityEngine;

namespace LIIA.SphereMaze
{
    public enum GameAudioCue { Start, Star, Checkpoint, Trap, Goal }

    /// <summary>Feedback sonoro procedural: no requiere archivos ni licencias externas.</summary>
    [RequireComponent(typeof(AudioSource))]
    public sealed class LIIAAudioFeedback : MonoBehaviour
    {
        public static LIIAAudioFeedback Instance { get; private set; }
        public bool Muted { get; private set; }

        private AudioSource source;
        private AudioSource rollingSource;
        private MazeBallController ball;
        private AudioClip start;
        private AudioClip star;
        private AudioClip checkpoint;
        private AudioClip trap;
        private AudioClip goal;
        private AudioClip impact;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            source.volume = .32f;
            rollingSource = gameObject.AddComponent<AudioSource>();
            rollingSource.playOnAwake = false;
            rollingSource.loop = true;
            rollingSource.spatialBlend = 0f;
            Muted = PlayerPrefs.GetInt("LIIA_AUDIO_MUTED", 0) == 1;
            source.mute = Muted;
            start = CreateTone("Start", 440f, .13f, .35f);
            star = CreateTone("Star", 880f, .18f, .34f);
            checkpoint = CreateTone("Checkpoint", 620f, .2f, .3f);
            trap = CreateTone("Trap", 165f, .26f, .36f);
            goal = CreateTone("Goal", 740f, .42f, .38f);
            impact = CreateNoise("Impact", .12f, .24f);
            rollingSource.clip = CreateRollingLoop();
            rollingSource.volume = 0f;
            rollingSource.Play();
        }

        public void Configure(MazeBallController player) => ball = player;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)) ToggleMute();
            if (rollingSource == null) return;
            var speedRatio = ball == null || GameFlow.Instance == null || GameFlow.Instance.State != GameState.Playing
                ? 0f
                : Mathf.Clamp01(ball.Speed / 4.6f);
            rollingSource.volume = Muted ? 0f : speedRatio * .13f;
            rollingSource.pitch = Mathf.Lerp(.72f, 1.32f, speedRatio);
        }

        public void PlayImpact(float strength)
        {
            if (source == null || Muted) return;
            source.PlayOneShot(impact, Mathf.Lerp(.22f, .72f, Mathf.Clamp01(strength)));
        }

        public void Play(GameAudioCue cue)
        {
            if (source == null || Muted) return;
            source.PlayOneShot(cue == GameAudioCue.Start ? start :
                cue == GameAudioCue.Star ? star :
                cue == GameAudioCue.Checkpoint ? checkpoint :
                cue == GameAudioCue.Trap ? trap : goal);
        }

        public void ToggleMute()
        {
            Muted = !Muted;
            source.mute = Muted;
            if (rollingSource != null) rollingSource.mute = Muted;
            PlayerPrefs.SetInt("LIIA_AUDIO_MUTED", Muted ? 1 : 0);
            PlayerPrefs.Save();
        }

        private static AudioClip CreateTone(string clipName, float frequency, float duration, float gain)
        {
            const int sampleRate = 44100;
            var samples = Mathf.CeilToInt(sampleRate * duration);
            var data = new float[samples];
            for (var i = 0; i < samples; i++)
            {
                var progress = i / (float)(samples - 1);
                var envelope = Mathf.Sin(progress * Mathf.PI);
                data[i] = Mathf.Sin(2f * Mathf.PI * frequency * i / sampleRate) * envelope * gain;
            }
            var clip = AudioClip.Create(clipName, samples, 1, sampleRate, false);
            clip.SetData(data, 0);
            return clip;
        }

        private static AudioClip CreateNoise(string clipName, float duration, float gain)
        {
            const int sampleRate = 44100;
            var samples = Mathf.CeilToInt(sampleRate * duration);
            var data = new float[samples];
            var random = new System.Random(1444);
            for (var i = 0; i < samples; i++)
            {
                var progress = i / (float)(samples - 1);
                var envelope = Mathf.Pow(1f - progress, 2f);
                data[i] = ((float)random.NextDouble() * 2f - 1f) * envelope * gain;
            }
            var clip = AudioClip.Create(clipName, samples, 1, sampleRate, false);
            clip.SetData(data, 0);
            return clip;
        }

        private static AudioClip CreateRollingLoop()
        {
            const int sampleRate = 22050;
            var data = new float[sampleRate];
            var random = new System.Random(62);
            var filtered = 0f;
            for (var i = 0; i < data.Length; i++)
            {
                var noise = (float)random.NextDouble() * 2f - 1f;
                filtered = Mathf.Lerp(filtered, noise, .055f);
                data[i] = filtered * .32f + Mathf.Sin(i / (float)sampleRate * Mathf.PI * 2f * 74f) * .05f;
            }
            var clip = AudioClip.Create("Rolling", data.Length, 1, sampleRate, false);
            clip.SetData(data, 0);
            return clip;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}
