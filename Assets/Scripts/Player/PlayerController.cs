using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject SpawnPosition;
    public bool WakeUp = false;
    public AudioClip[] wakeUpSounds;
    public AudioSource playerAudio;
    public AudioSource musicSource;
    public GameObject ObjectiveText;
    public CharacterController cc;
    public Animator eyesAnimator;
    public Animator playerAnimator;

    float objectiveTimer = 7;
    bool startedMusic = false;
    private void Awake()
    {
        if (WakeUp)
        {
            cc.enabled = false;
            transform.position = SpawnPosition.transform.position;
            cc.enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (WakeUp)
        {
            //transform.position = SpawnPosition.transform.position;
            int randomClip = Random.Range(0, wakeUpSounds.Length);
            playerAudio.clip = wakeUpSounds[randomClip];
            playerAudio.Play();
            ObjectiveText.SetActive(true);
            eyesAnimator.gameObject.SetActive(true);
            eyesAnimator.SetTrigger("open eyes");
            playerAnimator.SetTrigger("WakeUp");
        }
        else
        {
            eyesAnimator.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (WakeUp)
        {
            if (objectiveTimer > 0)
            {
                objectiveTimer -= Time.deltaTime;
            }
            else
            {
                ObjectiveText.SetActive(false);
                if (!startedMusic)
                {
                    musicSource.Play();
                    startedMusic = true;
                }
            }
        }
    }
}
