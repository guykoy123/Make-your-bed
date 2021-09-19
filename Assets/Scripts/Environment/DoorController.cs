using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
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
        if (open)
        {
            transform.Rotate(axis, 90f);
            open = false;
        }
        else
        {
            open = true;
            transform.Rotate(axis, -90f);
        }
    }

}
