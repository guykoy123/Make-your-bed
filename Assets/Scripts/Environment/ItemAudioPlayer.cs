using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudioPlayer : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.volume = OptionsData.masterVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateVolume()
    {
        audioSource.volume = OptionsData.masterVolume;
    }
    public void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}
    
