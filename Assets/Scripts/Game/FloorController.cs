using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    List<Collider> items=new List<Collider>();
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
        //remove duplicates from collider list
        string OnFloor = "On the floor: ";
        List<string> itemNames = new List<string>();
        for(int i = 0; i < items.Count; i++)
        {
            if (!itemNames.Contains(items[i].name))
            {
                itemNames.Add(items[i].name);
                OnFloor += items[i].name + ", ";
            }
        }
        Debug.Log(OnFloor);
        return itemNames.Count;
    }
    public void RemoveItemFromFloor(Collider item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("removed item from floor: " + item.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            if (!items.Contains(other))
            {
                items.Add(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag== "Object")
        {
            if (items.Contains(other))
            {
                items.Remove(other);
            }
        }
    }
}
