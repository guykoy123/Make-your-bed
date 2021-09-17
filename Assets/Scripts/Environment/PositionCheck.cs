using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    Collider[] colliders;
    Collider currentCollider;
    public Type areaType;

    // Start is called before the first frame update
    void Start()
    {
        colliders = gameObject.GetComponentsInChildren<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains(areaType.ToString().ToLower()))
        {
            currentCollider = other;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == currentCollider)
        {
            currentCollider = null;
        }
    }
    public float Check()
    {
        float percentage = 0;
        int colliderAmount = 0;
        if(currentCollider != null)
        {
            if (currentCollider.name.Contains(areaType.ToString().ToLower()))
            {

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].bounds.Intersects(currentCollider.bounds))
                    {
                        colliderAmount++;
                    }
                }
                percentage = 100 / colliders.Length * colliderAmount;

            }
        }
        return percentage;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Type
{
    Mattress,
    Pillow
}



