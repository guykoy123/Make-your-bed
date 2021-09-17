using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform CameraTransform;
    public Timer gameTimer;

    public float speed = 10f;
    public float gravity = -10f;
    public float mouseSensitivity = 300f;
    public float jumpForce = 7f;

    float upSpeed = 0;
    bool isGrounded=true;
    float xRotation = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        controller.Move(new Vector3(0,0,0));
        isGrounded = controller.isGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer.gotTime())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            CameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = (transform.right * horizontal + transform.forward * vertical) * speed;

            if(isGrounded && Input.GetButtonDown("Jump"))
            {
                upSpeed += jumpForce;
            }
            else if (!isGrounded)
            {
                upSpeed += gravity * Time.deltaTime;
            }

            Vector3 movement = direction * Time.deltaTime;
            movement.y += upSpeed * Time.deltaTime;

            controller.Move(movement);
            isGrounded = controller.isGrounded; //because controller.isGrounded is updated after using the move method
        }  
        
    }
}
