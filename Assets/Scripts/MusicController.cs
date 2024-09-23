using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip musicClip;    

    void Start()
    {
        audioSource.clip = musicClip;
        audioSource.loop = true;  
        audioSource.Play();     
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
}