using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    List<GameObject> items=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetItemsOnFloor()
    {
        //TODO: implement not counting items in proper position
        return items.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            items.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag== "Object")
        {
            items.Remove(other.gameObject);
        }
    }
}
