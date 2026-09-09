using UnityEngine;

public class HitEffect : MonoBehaviour {
    public AudioClip sound;

    [Tooltip("Tempo até se autodestruir (pra ser de acordo com o tamanho do som)")]
    public float lifetime = 1f;

    void Start(){
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && sound != null) {
            audioSource.PlayOneShot(sound);
        } 
        
        Destroy(gameObject, lifetime);
    }
}