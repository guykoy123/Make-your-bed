using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudioPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] clips;
    public bool playFromClips;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (playFromClips)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
        }
        audioSource.Play();
    }
}
    
