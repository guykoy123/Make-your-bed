using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudioPlayer : MonoBehaviour
{
    AudioSource audioSource;
    float PreviousVelocity;
    float Velocity = 0;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.volume = OptionsData.masterVolume;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PreviousVelocity = Velocity;
        Velocity = rb.velocity.magnitude;
    }

    public void UpdateVolume()
    {
        //audioSource.volume = OptionsData.masterVolume;
    }
    public void OnCollisionEnter(Collision collision)
    {
        audioSource.volume = Mathf.InverseLerp(0, 1, PreviousVelocity);
        audioSource.Play();
    }
}
    
