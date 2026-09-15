using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [Header("Som")]
    public AudioClip sound;

    [Header("Duração")]
    public float lifetime = 1f;

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }

        Destroy(gameObject, lifetime);
    }
}