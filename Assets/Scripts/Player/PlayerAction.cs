using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Transform playerCam;
    public Rigidbody holdPosition;
    public Timer gameTimer;
    public GameManager gameManager;

    CharacterJoint joint;
    GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if player press escape key (independent of pausing)
        if (Input.GetButtonDown("Cancel") && gameTimer.gotTime())
        {
            if (gameManager.isPaused())
            {
                gameManager.Unpause();
            }
            else
            {
                gameManager.pauseGame();
            }
           
        }
        //check mouse click while game is active
        if (Input.GetMouseButtonDown(0) && gameTimer.gotTime() && !gameManager.isPaused())
        {
            //send ray and act based on the object hit
            RaycastHit hit;
            Physics.Raycast(playerCam.position, playerCam.TransformVector(Vector3.forward), out hit, 5f);
            if(hit.collider != null)
            {
                if (hit.collider.tag == "Object")
                {
                    HoldObject(hit);
                }
                else if (hit.collider.tag == "Drawer")
                {
                    DrawerController drawer = hit.collider.GetComponent<DrawerController>();
                    drawer.ToggleDrawer();
                }
                else if(hit.collider.tag == "Door")
                {
                    hit.collider.GetComponent<DoorController>().toggleDoor();
                }
                else
                {
                    Debug.Log("hit: " + hit.collider.name);
                }
            }
            
        }
        //clear the joint created for holding the object on mouse button release
        else if (Input.GetMouseButtonUp(0) && gameTimer.gotTime() && !gameManager.isPaused())
        {
            if (joint != null)
            {
                Destroy(joint);
            }
            
        }
        //if button is not pressed and joint is not null, clear it
        else if(joint != null && !Input.GetMouseButton(0) && gameTimer.gotTime() && !gameManager.isPaused())
        {
            Destroy(joint);
        }
        
    }

    void HoldObject(RaycastHit hit)
    {
        //get game object and add charachter joint to it
        item = hit.collider.gameObject;
        item.AddComponent<CharacterJoint>();
        joint = item.GetComponent<CharacterJoint>();

        //get anchor position from position the player clicked on the object
        Vector3 anchorPosition = item.transform.InverseTransformPoint(hit.point);

        Debug.Log("hold position:" + hit.point);
        //maintains the distance of the object from the player
        holdPosition.transform.position = hit.point;

        //update anchor position
        joint.connectedBody = holdPosition;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = Vector3.zero;
        joint.anchor = anchorPosition;


        //set twist limits, allows object to rotate completely around
        SoftJointLimit jointLimit = joint.lowTwistLimit;
        jointLimit.limit = -177;
        joint.lowTwistLimit = jointLimit;
        jointLimit.limit = 177;
        joint.highTwistLimit = jointLimit;
    }
}
