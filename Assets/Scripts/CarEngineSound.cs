using UnityEngine;

public class CarEngineSound : MonoBehaviour
{
    public AudioSource engineAudioSource; 
    public float minPitch = 0.8f;         
    public float maxPitch = 2.0f;         
    public float maxSpeed = 100f;         

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.loop = true; 
            engineAudioSource.Play();
        }
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;

        float pitch = Mathf.Lerp(minPitch, maxPitch, speed / maxSpeed);

        engineAudioSource.pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }
}
