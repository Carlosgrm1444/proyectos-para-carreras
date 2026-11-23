using UnityEngine;

public sealed class MusicController : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = true;
        source.volume = .10f;
        source.priority = 200;
        source.clip = Resources.Load<AudioClip>("RetroLevelMusic");
    }

    private void Start()
    {
        if (source.clip != null) source.Play();
        else Debug.LogWarning("No se encontró Resources/RetroLevelMusic.wav");
    }

    public void SetMuted(bool value) => source.mute = value;
}
