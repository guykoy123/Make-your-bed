using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public enum Side
    {
        Right,
        Left
    }
    public bool ClosetDoor = false;
    public Side DoorSide;
    
    Vector3 axis = new Vector3(0, 0, 1);
    bool open = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleDoor()
    {
        if (ClosetDoor)
        {
            toggleClosetDoor();

            
        }
        else
        {
            if (open)
            {
                //close door
                transform.Rotate(axis, 90f);
                open = false;
            }
            else
            {
                //open door
                open = true;
                transform.Rotate(axis, -90f);
            }
        }
    }
    private void toggleClosetDoor()
    {
        if (DoorSide == Side.Right)
        {
            if (open)
            {
                //close door
                transform.Rotate(new Vector3(0, 0, 1), 90f);
                transform.localPosition += new Vector3(0, 0.003f, 0);
                open = false;
            }
            else
            {
                //open door
                open = true;
                transform.Rotate(new Vector3(0, 0, 1), -90f);
                transform.localPosition += new Vector3(0, -0.003f, 0);
            }
        }
        else
        {
            if (open)
            {
                //close door
                transform.Rotate(new Vector3(0, 0, 1), -90f);
                transform.localPosition += new Vector3(-0.002f, 0.005f, 0);
                open = false;
            }
            else
            {
                //open door
                open = true;
                transform.Rotate(new Vector3(0, 0, 1), 90f);
                transform.localPosition += new Vector3(0.002f, -0.005f, 0);
            }
        }
    }

}
