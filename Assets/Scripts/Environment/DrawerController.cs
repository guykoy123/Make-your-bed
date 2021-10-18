using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    public bool SwitchDirection = false;
    float Direction = 1;
    public bool SwitchAxis = false;

    AudioSource audioSource;

    bool open = false;
    Vector3 basePosition;
    Vector3 openPosition;
    float moveSpeed = 0.01f;
    Dictionary<Collider, Vector3> objectScales = new Dictionary<Collider, Vector3>();

    private void Start()
    {
        basePosition = transform.localPosition;

        if (SwitchDirection)
        {
            Direction = -1;
        }
        if (SwitchAxis)
        {
            openPosition = transform.localPosition + new Vector3(0, -0.007f * Direction, 0);
        }
        else
        {
            openPosition = transform.localPosition + new Vector3(-0.007f * Direction, 0, 0);
        }
        
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = OptionsData.masterVolume;

    }
    void Update()
    {
        if (open)
        {
            //slide the drawer
            if (SwitchAxis)
            {
                transform.localPosition = transform.localPosition - new Vector3( 0, Time.deltaTime * moveSpeed * Direction, 0);
            }
            else
            {
                transform.localPosition = transform.localPosition - new Vector3(Time.deltaTime * moveSpeed * Direction, 0, 0);
            }
        }
        else
        {
            // slide the drawer
            if (SwitchAxis)
            {
                transform.localPosition = transform.localPosition + new Vector3( 0, Time.deltaTime * moveSpeed * Direction, 0);
            }
            else
            {
                transform.localPosition = transform.localPosition + new Vector3(Time.deltaTime * moveSpeed * Direction, 0, 0);
            }
        }
        transform.localPosition=new Vector3(Mathf.Clamp(transform.localPosition.x, openPosition.x, basePosition.x), Mathf.Clamp(transform.localPosition.y, openPosition.y, basePosition.y), basePosition.z);
    }
    public void ToggleDrawer()
    {
        audioSource.Play();
        if (open) { open = false; }
        else { open = true; }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            objectScales.Add(other, other.transform.localScale);
            other.transform.SetParent(transform);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object")
        {
            other.transform.parent = GameObject.Find("### Level ###").transform;
            Vector3 scale;
            objectScales.TryGetValue(other, out scale);
            other.transform.localScale = scale;
            objectScales.Remove(other);
        }
    }
}
