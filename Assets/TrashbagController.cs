using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbagController : MonoBehaviour
{

    List<GameObject> Trash = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBag(List<GameObject> t)
    {
        Trash = t;
        for(int i = 0; i < Trash.Count; i++)
        {
            Trash[i].SetActive(false);
            Trash[i].transform.parent = transform; //parent to the trash bag
        }
    }

    public void DestroyBag()
    {
        for (int i = 0; i < Trash.Count; i++)
        {
            Trash[i].SetActive(true);
            Trash[i].transform.parent = null; //parent to the trash bag
            Trash[i].transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
