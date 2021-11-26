using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class FootstepsController : MonoBehaviour
{
    GameManager gameManager;
    AudioSource objectAudio;

    float delay = 1f; //the amount of time between each step
    float currentTime = 0f;
    bool play = false;
    GroundType currentGround;

    float volumeMultiplier = 1f;

    AudioClip[] desertSteps;
    AudioClip[] concreteSteps;

    // Start is called before the first frame update
    void Start()
    {
        SetupAudioSource();
        LoadAudioClips();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (play && !gameManager.isPaused())
        {
            CheckGroundType();
            if(currentTime >= delay)
            {
                currentTime = 0;
                switch (currentGround)
                {
                    case GroundType.Desert:
                        int i = Random.Range(0, desertSteps.Length);
                        objectAudio.clip = desertSteps[i];
                        objectAudio.volume = 0.008f*volumeMultiplier;
                        objectAudio.Play();
                        break;
                    case GroundType.Concrete:
                        int j = Random.Range(0, concreteSteps.Length);
                        objectAudio.clip = concreteSteps[j];
                        objectAudio.volume = 0.03f*volumeMultiplier;
                        objectAudio.Play();
                        break;

                }
            }
            currentTime += Time.deltaTime;
        }
    }
    void CheckGroundType()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.tag == "Terrain")
            {
                currentGround = GroundType.Desert;
            }
            else
            {
                currentGround = GroundType.Concrete;
            }
        }
    }
    public void SetVolumeMultiplier(float multi)
    {
        volumeMultiplier = multi;
    }
    public void playSound(float d)
    {
        delay = d;
        currentTime = d * 0.5f;
        play = true;
    }
    public void stopSound()
    {
        play = false;
    }
    public bool isPlaying()
    {
        return play;
    }
    public GroundType getCurrentGroundType()
    {
        return currentGround;
    }
    public float getCurrentDelay()
    {
        return delay;
    }
    void LoadAudioClips()
    {
        desertSteps = Resources.LoadAll<AudioClip>("Game Sounds/footsteps/desert");
        concreteSteps = Resources.LoadAll<AudioClip>("Game Sounds/footsteps/concrete");
    }
    void SetupAudioSource()
    {
        objectAudio = gameObject.AddComponent<AudioSource>();
        objectAudio.playOnAwake = false;
        AudioMixer a = Resources.Load<AudioMixer>("GameMixer");
        objectAudio.outputAudioMixerGroup = a.FindMatchingGroups("Game Sound")[0];
        objectAudio.spatialBlend = 1;
        objectAudio.volume = 0.01f;
    }
}

public enum GroundType
{
    Desert,
    Concrete
}
