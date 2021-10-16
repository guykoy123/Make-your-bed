using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbagController : MonoBehaviour
{

    List<GameObject> Trash = new List<GameObject>();
    GameObject poof;
    // Start is called before the first frame update
    void Start()
    {
        GameObject trashbagPoof = Resources.Load<GameObject>("Trashbag Poof");
        poof = Instantiate(trashbagPoof, transform.position-new Vector3(0,0.3f,0),Quaternion.Euler(-90,0,0));

        //add random upwards force
        Vector3 spawnForce = new Vector3(Random.Range(-1f, 1f), 4, Random.Range(-1f, 1f));
        gameObject.GetComponent<Rigidbody>().AddForce(spawnForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBag(List<GameObject> t)
    {
        //save the trash items list
        Trash = t;
        //disable the items and parent them to the trashbag
        for(int i = 0; i < Trash.Count; i++)
        {
            Trash[i].SetActive(false);
            Trash[i].transform.parent = transform; //parent to the trash bag
        }
        
    }

    public void DestroyBag()
    {
        //enable the trash items, remove the parent and update their position
        for (int i = 0; i < Trash.Count; i++)
        {
            Trash[i].SetActive(true);
            Trash[i].transform.parent = null; //parent to the trash bag
            Trash[i].transform.position = transform.position;
        }
        Destroy(poof);
        //the trashbag game object is the detroyed by the caller of this method
    }
}
