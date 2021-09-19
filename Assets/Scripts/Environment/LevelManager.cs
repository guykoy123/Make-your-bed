using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Transform spawnPoint;

    private void Start()
    {
        spawnPoint = GameObject.Find("Spawn Point").GetComponent<Transform>();
    }
    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag != "Player")
        {
            Debug.Log(other.name + " is out of bounds");
            other.transform.position = spawnPoint.position;
        }
        
    }
}
