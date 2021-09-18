using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unpause()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.Unpause();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
