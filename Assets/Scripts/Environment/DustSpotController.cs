using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DustSpotController : MonoBehaviour
{
    public GameObject playerCam;
    public PlayerMovement playerMove;
    public PlayerAction playerAct;
    public TMP_Text dustText;

    Transform Sponge;
    GameObject dustCam;
    AudioSource dustAudio;
    GameObject particleSystem;
    ParticleSystem dustCloud;

    public float maxHP = 1;
    float currentHP;
    float cleaningSpeed = 0.5f;

    bool inCleaningMode = false;
    bool clean = false;

    Vector3 previousMousePos= Vector3.zero;
    Vector3 previousMouseDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        dustAudio = gameObject.GetComponent<AudioSource>();
        dustCam = transform.GetComponentInChildren<Camera>().gameObject;
        dustCam.SetActive(false);
        currentHP = maxHP;
        particleSystem = Resources.Load<GameObject>("Dust Cleaning particles");


    }
    public bool isCleaning()
    {
        return inCleaningMode;
    }

    public void EnterCleaningView()
    {
        Debug.Log("entering dust spot");

        playerCam.SetActive(false);
        dustCam.SetActive(true);

        inCleaningMode = true;

        playerMove.DisableMovement();
        playerAct.DisableAction();

        Cursor.lockState = CursorLockMode.Confined;

        GameObject SpongePrefab = Resources.Load<GameObject>("sponge");
        Sponge = Instantiate(SpongePrefab).transform;

        dustCloud = Instantiate<GameObject>(particleSystem).GetComponent<ParticleSystem>();
        dustCloud.Pause();
        dustCloud.transform.parent = Sponge.transform;
        dustCloud.transform.localPosition = Vector3.zero;

        dustText.gameObject.SetActive(true);

        previousMousePos = Input.mousePosition;

        //dustAudio.Play();
    }

    public void ExitCleaningView()
    {
        Debug.Log("exiting dust spot");

        Destroy(Sponge.gameObject);

        playerCam.SetActive(true);
        dustCam.SetActive(false);

        playerAct.SkipFrame();

        inCleaningMode = false;

        playerMove.EnableMovement();
        playerAct.EnableAction();

        Cursor.lockState = CursorLockMode.Locked;

        dustText.gameObject.SetActive(false);

        dustCloud.Pause();

        //dustAudio.Pause();
    }

    public bool isClean()
    {
        return clean;
    }
    // Update is called once per frame
    void Update()
    {
        if (inCleaningMode)
        {
            //if pressed the button exits back to the game
            if (Input.GetButtonDown("Interact"))
            {
                ExitCleaningView();
            }
            //doesn't exit back when the player finished cleaning only after he releases the mouse button (prevents unexepected jump back)
            else if(Input.GetMouseButtonUp(0) && clean)
            {
                ExitCleaningView();
            }
            else
            {
                //get mouse position in world space and clamp the sponge to it
                Ray ray = dustCam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    Sponge.position = hit.point;
                    //if mouse button is held down and moving cleans the dust
                    if (Input.GetMouseButton(0))
                    {
                        if(hit.collider.tag == "Dust Spot" && currentHP>0)
                        {
                            if (previousMousePos != Input.mousePosition)
                            {
                                /*Vector3 currentMouseDirection = Input.mousePosition - previousMousePos;
                                if (Vector3.Angle(currentMouseDirection, previousMouseDirection)>150)
                                {
                                    dustAudio.Play();
                                }
                                previousMouseDirection = currentMouseDirection;*/
                                previousMousePos = Input.mousePosition;
                                currentHP -= cleaningSpeed * Time.deltaTime;
                                dustText.text = Mathf.Round(Mathf.Max(currentHP / maxHP * 100, 0)).ToString() + "%";
                                if (!dustCloud.isEmitting)
                                {
                                    dustCloud.Play();
                                }
                            }
                            else
                            {
                                dustCloud.Stop();
                            }

                        }
                        else if (currentHP <= 0)
                        {
                            gameObject.GetComponent<Renderer>().enabled = false;
                            gameObject.GetComponent<MeshCollider>().enabled = false;
                            clean = true;
                        }
                        else
                        {
                            dustCloud.Stop();
                        }
                    }
                    else
                    {
                        dustCloud.Stop();
                    }
                }

            }

            
        }
    }
}
