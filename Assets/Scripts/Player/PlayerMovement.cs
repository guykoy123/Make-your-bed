using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform CameraTransform;
    public Timer gameTimer;
    public GameManager gameManager;
    public Animator playerAnimator;

    //Movement speeds
    float WalkSpeed = 5f;
    float SprintSpeed = 9f;
    float CrouchSpeed = 3f;
    float ProneSpeed = 1.5f;

    public float gravity = -10f;
    public float mouseSensitivity = 300f;
    public float jumpForce = 7f;

    float upSpeed = 0;
    bool isGrounded=true;
    float xRotation = 0f;
    float currentSpeed;

    bool movementDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        controller.Move(new Vector3(0,0,0));
        isGrounded = controller.isGrounded;
        currentSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //while game is not paused
        if (gameTimer.gotTime() && !gameManager.isPaused() && !movementDisabled)
        {
            
            PlayerCameraMove();

            CheckMovementType();

            //get movement input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            //calculate movement
            Vector3 direction = (transform.right * horizontal + transform.forward * vertical) * currentSpeed;

            //play walking animation
            if (direction.sqrMagnitude > 0)
            {
                playerAnimator.SetBool("Walk", true);
            }
            else
            {
                playerAnimator.SetBool("Walk", false);
            }

            //checl jumping
            if(isGrounded && Input.GetButtonDown("Jump"))
            {
                upSpeed = jumpForce;
            }
            else if (!isGrounded)
            {
                upSpeed += gravity * Time.deltaTime;
            }

            Vector3 movement = direction * Time.deltaTime;
            movement.y += upSpeed * Time.deltaTime;
            //move player
            controller.Move(movement);
            isGrounded = controller.isGrounded; //because controller.isGrounded is updated after using the move method
        }  
        
    }

    public void DisableMovement()
    {
        movementDisabled = true;
    }
    public void EnableMovement()
    {
        movementDisabled = false;
    }
    void PlayerCameraMove()
    {
        //rotate the player camera up and down

        //get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //clamp the upwards rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate the camera up and down
        CameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //rotate player
        transform.Rotate(Vector3.up * mouseX);

    }
    void CheckMovementType()
    {
        /*
         * check if a movement type button is pressed down
         * if yes triggers the movement animation and changes the player speed
         * then checks if movement type button is released 
         * if yes triggers animation and reverts to walking speed
         */
        if (Input.GetButtonDown("Sprint"))
        {
            playerAnimator.SetBool("Sprint", true);
            currentSpeed = SprintSpeed;
        }
        else if (Input.GetButtonDown("Crouch"))
        {
            playerAnimator.SetTrigger("CrouchDown");
            currentSpeed = CrouchSpeed;
            playerAnimator.SetBool("Crouch", true);
            controller.height = 1.6f;
            controller.center = new Vector3(0, -0.1f, 0);
        }
        else if (Input.GetButtonDown("Prone"))
        {
            playerAnimator.SetTrigger("ProneDown");
            currentSpeed = ProneSpeed;
            playerAnimator.SetBool("Prone", true);
            controller.height = 0.5f;
            controller.center = new Vector3(0, -0.6f, 1.5f);
        }

        if (Input.GetButtonUp("Crouch"))
        {
            playerAnimator.SetTrigger("CrouchUp");
            currentSpeed = WalkSpeed;
            playerAnimator.SetBool("Crouch", false);
            controller.height = 1.8f;
            controller.center = new Vector3(0, 0, 0);
        }
        else if (Input.GetButtonUp("Prone"))
        {
            playerAnimator.SetTrigger("ProneUp");
            currentSpeed = WalkSpeed;
            playerAnimator.SetBool("Prone", false);
            controller.height = 1.8f;
            controller.center = new Vector3(0, 0, 0);
        }
       else if (Input.GetButtonUp("Sprint"))
        {
            currentSpeed = WalkSpeed;
            playerAnimator.SetBool("Sprint", false);
        }
    }
}
