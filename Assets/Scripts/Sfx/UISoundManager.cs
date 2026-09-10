using UnityEngine;

public class UISoundManager : MonoBehaviour {
    public static UISoundManager Instance;

    [Header("Sons")] 
    public AudioClip hoverSound;
    public AudioClip CancelSound;
    public AudioClip AcceptSound;

    private AudioSource audioSource;

    void Awake(){
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHover(){
        if (hoverSound != null) audioSource.PlayOneShot(hoverSound);
    }

    public void PlayAccept(){
        if (AcceptSound != null) audioSource.PlayOneShot(AcceptSound);
    }
    
    public void PlayCancel(){
        if (CancelSound != null) audioSource.PlayOneShot(CancelSound);
    }
}