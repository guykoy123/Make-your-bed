using System.Collections.Generic;
using UnityEngine;

public class TrashcanController : MonoBehaviour
{
    public List<GameObject> Trash = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

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
            Trash.Add(other.gameObject);
        }
    }

    public void TakeoutTrash()
    {
        GameObject t= Resources.Load<GameObject>("full trashbag");
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 0.5f;
        GameObject bag = Instantiate(t,spawnPosition,Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        
        bag.GetComponent<TrashbagController>().createBag(Trash);
        for(int i = 0; i < Trash.Count; i++)
        {
            Trash.RemoveAt(i);
        }
    }
}
