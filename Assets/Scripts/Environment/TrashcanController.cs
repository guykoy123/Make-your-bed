using System.Collections.Generic;
using UnityEngine;

public class TrashcanController : MonoBehaviour
{
    public FloorController floorController;

    List<GameObject> Trash = new List<GameObject>();
    GameObject trashBagPrefab;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        //load trashbag prefab
        trashBagPrefab = Resources.Load<GameObject>("full trashbag");

        //set trashbag spawn position
        spawnPosition = transform.position;
        spawnPosition.y += 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        //add objects that are inside the trash can to trash list.
        if (other.tag == "Object")
        {
            if (!other.name.Contains("trashbag"))
            {
                Trash.Add(other.gameObject);
            }
            
        }
    }

    public void TakeoutTrash()
    {
        //spawn the gameobject
        //GameObject bag = Instantiate(trashBagPrefab);
        //bag.transform.position = spawnPosition;new Vector3(0,1,0)
        GameObject bag = Instantiate(trashBagPrefab,spawnPosition ,Quaternion.Euler(new Vector3(0,0,0)));

        //initialize the trash bag
        bag.GetComponent<TrashbagController>().createBag(Trash);

   
        //create new empty trash items list
        Trash = new List<GameObject>();
    }
}
