using UnityEngine;

public class PlayImpactSound : MonoBehaviour
{
    public float impactThreshold = 0.3f;
    public float maxImpact = 5f;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;
        if (impact >= impactThreshold)
        {
            float volume = Mathf.Clamp01(impact / maxImpact);
            audioSource.PlayOneShot(audioSource.clip, volume);
        }
    }
}
