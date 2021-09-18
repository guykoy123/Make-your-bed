using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    AudioSource audioSource;

    bool open = false;
    Vector3 Move = new Vector3(-1f, 0, 0);
    Vector3 basePosition;
    Vector3 openPosition;
    float moveSpeed = 0.01f;
    Dictionary<Collider, Vector3> objectScales = new Dictionary<Collider, Vector3>();

    private void Start()
    {
        basePosition = transform.localPosition;
        openPosition = transform.localPosition + new Vector3(-0.007f,0,0);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = OptionsData.masterVolume;
    }
    void Update()
    {
        if (open)
        {
            //slide the drawer
            transform.localPosition = transform.localPosition + Move * Time.deltaTime * moveSpeed;
        }
        else
        {
            // slide the drawer
            transform.localPosition = transform.localPosition - Move * Time.deltaTime * moveSpeed;
        }
        transform.localPosition=new Vector3(Mathf.Clamp(transform.localPosition.x, openPosition.x, basePosition.x),basePosition.y,basePosition.z);
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
