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
    public bool Locked = false;
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;

    AudioSource doorAudio;
    Vector3 axis = new Vector3(0, 0, 1);
    bool open = false;
    
    // Start is called before the first frame update
    void Start()
    {

        doorAudio = gameObject.GetComponent<AudioSource>();
        doorAudio.playOnAwake = false;
        doorAudio.spatialBlend = 1;
        doorAudio.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isOpen()
    {
        return open;
    }
    public void toggleDoor(bool force)
    {
        if (!Locked || force)
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
                    doorAudio.clip = doorCloseClip;
                    doorAudio.Play();
                }
                else
                {
                    //open door
                    open = true;
                    transform.Rotate(axis, -90f);
                    doorAudio.clip = doorOpenClip;
                    doorAudio.Play();
                }
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
                doorAudio.clip = doorCloseClip;
                doorAudio.Play();
            }
            else
            {
                //open door
                open = true;
                transform.Rotate(new Vector3(0, 0, 1), -90f);
                transform.localPosition += new Vector3(0, -0.003f, 0);
                doorAudio.clip = doorOpenClip;
                doorAudio.Play();
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
                doorAudio.clip = doorCloseClip;
                doorAudio.Play();
            }
            else
            {
                //open door
                open = true;
                transform.Rotate(new Vector3(0, 0, 1), 90f);
                transform.localPosition += new Vector3(0.002f, -0.005f, 0);
                doorAudio.clip = doorOpenClip;
                doorAudio.Play();
            }
        }
    }

}
