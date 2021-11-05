using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAction : MonoBehaviour
{
    public Transform playerCam;
    public Rigidbody holdPosition;
    public Timer gameTimer;
    public GameManager gameManager;
    public TMP_Text interactMessage;

    bool disabledAction = false;
    bool skipFrame = false;
    CharacterJoint joint;
    GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabledAction)
        {
            if (!skipFrame)
            {
                //check if player press escape key (independent of pausing)
                CheckPause();

                //check mouse click while game is active
                CheckMouseClick();

                //check if interactable objectss
                CheckInteractable();
            }
            else
            {
                skipFrame = false;
            }
        }
    }

    public void DisableAction()
    {
        disabledAction = true;
    }
    public void EnableAction()
    {
        disabledAction = false;
    }
    public void SkipFrame()
    {
        skipFrame = true;
    }
    void CheckInteractable()
    {
        RaycastHit hit2;
        Physics.Raycast(playerCam.position, playerCam.TransformVector(Vector3.forward), out hit2, 3f);
        if (hit2.collider != null)
        {
            //check game object has interactable tag
            if (hit2.collider.tag == "Interact")
            {
                //show interact message (press e)
                interactMessage.text = "PRESS E TO TAKE OUT TRASH";      
                //check if e button is pressed
                if (Input.GetButtonDown("Interact"))
                {
                    //TODO: will be more orgenized when more interavtable stuff is added
                    hit2.collider.GetComponent<TrashcanController>().TakeoutTrash();
                }
            }

            //check for trash bag
            else if (hit2.collider.tag == "Object")
            {
                TrashbagController t;
                hit2.collider.TryGetComponent<TrashbagController>(out t);
                if (t)
                {
                    interactMessage.text = "PRESS E TO DESTROY TRASHBAG";
                    if (Input.GetButtonDown("Interact"))
                    {
                        hit2.collider.GetComponent<TrashbagController>().DestroyBag();
                        Destroy(hit2.collider.gameObject);
                        interactMessage.text = "";
                    }  
                }
            }

            else if(hit2.collider.tag == "Dust Spot")
            {
                DustSpotController spot = hit2.collider.GetComponent<DustSpotController>();
                if (!spot.isCleaning())
                {
                    interactMessage.text = "PRESS E TO CLEAN DUST";
                    if (Input.GetButtonDown("Interact"))
                    {
                        spot.EnterCleaningView();
                        interactMessage.text = "PRESS E TO EXIT BACK";
                    }
                }
            }

            else
            {
                interactMessage.text="";
            }
        }
        else
        {
            interactMessage.text="";
        }
    }
    void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && gameTimer.gotTime() && !gameManager.isPaused())
        {
            //send ray and act based on the object hit
            RaycastHit hit;
            Physics.Raycast(playerCam.position, playerCam.TransformVector(Vector3.forward), out hit, 5f);
            if (hit.collider != null)
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
                else if (hit.collider.tag == "Door")
                {
                    hit.collider.GetComponent<DoorController>().toggleDoor(false);
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
        else if (joint != null && !Input.GetMouseButton(0) && gameTimer.gotTime() && !gameManager.isPaused())
        {
            Destroy(joint);
        }
    }

    void CheckPause()
    {
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
    }
    void HoldObject(RaycastHit hit)
    {
        //get game object and add charachter joint to it
        item = hit.collider.gameObject;
        item.AddComponent<CharacterJoint>();
        joint = item.GetComponent<CharacterJoint>();

        //get anchor position from position the player clicked on the object
        Vector3 anchorPosition = item.transform.InverseTransformPoint(hit.point);

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
