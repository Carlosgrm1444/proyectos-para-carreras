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
        private AudioClip start;
        private AudioClip star;
        private AudioClip checkpoint;
        private AudioClip trap;
        private AudioClip goal;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            source.volume = .32f;
            Muted = PlayerPrefs.GetInt("LIIA_AUDIO_MUTED", 0) == 1;
            source.mute = Muted;
            start = CreateTone("Start", 440f, .13f, .35f);
            star = CreateTone("Star", 880f, .18f, .34f);
            checkpoint = CreateTone("Checkpoint", 620f, .2f, .3f);
            trap = CreateTone("Trap", 165f, .26f, .36f);
            goal = CreateTone("Goal", 740f, .42f, .38f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)) ToggleMute();
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

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}
