using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SargentController : MonoBehaviour
{
    public CharacterController controller;
    public PathController path;
    public Animator animator;
    public Timer gameTimer;
    public FootstepsController footSteps;
    public GameManager gameManager;

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
            }
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
}
