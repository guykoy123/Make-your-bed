using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startPress()
    {
        SceneManager.LoadScene("test scene", LoadSceneMode.Single);
    }

    public void exitPress()
    {
        Debug.Log("closing game");
        Application.Quit();
        
    }

    public void pressOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void pressBack()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }

    public void pressControls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
}
