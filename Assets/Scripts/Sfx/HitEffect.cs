using UnityEngine;

public class HitEffect : MonoBehaviour {
    [Header("Som")]
    public AudioClip sound;

    [Header("Efeito visual")]
    public ParticleSystem particles;

    [Tooltip("Usado só se não houver som nem partícula com duração pra calcular sozinho")]
    public float fallbackLifetime = 1f;

    void Start(){
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && sound != null) {
            audioSource.PlayOneShot(sound);
        }

        if (particles != null) {
            particles.Play();
        }

        Destroy(gameObject, CalculateLifetime());
    }

    private float CalculateLifetime(){
        float duration = fallbackLifetime;

        if (sound != null) duration = Mathf.Max(duration, sound.length);
        if (particles != null) {
            var main = particles.main;
            duration = Mathf.Max(duration, main.duration + main.startLifetime.constantMax);
        }

        return duration;
    }
}