using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SargentController : MonoBehaviour
{
    public CharacterController controller;
    public PathController path;
    public Animator animator;
    public Timer gameTimer;
    public FootstepsController footSteps;
    public GameManager gameManager;

    AudioSource audioSource;
    AudioClip[] screamingClips;

    public float speed = 5f;
    public float minimumDistanceToWaypoint = 1f;
    public float gravity = -10f;
    float downVelocity = 0f;
    bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        path.setMinDistance(minimumDistanceToWaypoint);
        controller.Move(Vector3.zero);
        animator.SetBool("Walking", true);
        footSteps.playSound(0.5f);
        footSteps.SetVolumeMultiplier(3);

        SetupAudioSource();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer.getRemainingTime() < 32)
        {
            if (!path.isFinished() && !gameManager.isPaused())
            {
                //check if a door is ahead and open it
                CheckForDoor();
                //apply gravity
                if (!controller.isGrounded)
                {
                    downVelocity += gravity * Time.deltaTime;
                }
                else
                {
                    downVelocity = 0;
                }
                //get the movement direction
                Vector3 direction = path.GetDirection(transform.position);
                //rotate to face the movement direction
                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720 * Time.deltaTime);
                transform.rotation = targetRotation;
                //add vertical velocity
                Vector3 movement = direction * speed * Time.deltaTime;
                movement.y = downVelocity;
                controller.Move(movement);
            }
            else if (!finished && !gameManager.isPaused())
            {
                finished = true;
                animator.SetBool("Walking", false);
                footSteps.stopSound();
                animator.SetTrigger("Judge");
                Debug.Log("reached the end");
                Debug.Log("rotation: " + transform.rotation);

            }
        }
        if(!gameTimer.gotTime() && !finished)
        {
            transform.position = path.getEndPosition()+new Vector3(0,1f,0);
            transform.rotation = new Quaternion(0, 0.4f, 0, 0.9f);
            path.markFinished();
        }
        if (finished && !audioSource.isPlaying && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Armature|yell")
        {
            int i = Random.Range(0, screamingClips.Length);
            audioSource.clip = screamingClips[i];
            audioSource.Play();

        }


    }
    void CheckForDoor()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit,2f);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Door")
            {
                DoorController d = hit.collider.GetComponent<DoorController>();
                if (!d.isOpen())
                {
                    Debug.Log(name + " open door(" + hit.collider.name + ")");
                    animator.SetTrigger("Door");
                    d.toggleDoor(true);
                }
            }
        }

    }
    void SetupAudioSource()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;
        audioSource.volume = 0.5f;
        AudioMixer a = Resources.Load<AudioMixer>("GameMixer");
        audioSource.outputAudioMixerGroup = a.FindMatchingGroups("Game Sound")[0];

        screamingClips = Resources.LoadAll<AudioClip>("Game Sounds/sargeant");
    }
}
