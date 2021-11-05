using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject endCam;
    public Timer gameTimer;
    public GameObject dustText;

    ScoreCalculator scoreCalculator;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        //make sure player camera is enabled
        playerCam.SetActive(true);
        endCam.SetActive(false);

        dustText.SetActive(false);
        scoreCalculator = gameObject.GetComponent<ScoreCalculator>();
        Cursor.lockState = CursorLockMode.Locked;
        HideItemAreas();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TimeOut()
    {
        //switch to end camera which shows the room
        playerCam.SetActive(false);
        endCam.SetActive(true);

        float score = scoreCalculator.CalculateTotal();
        Debug.Log("ran out of time, score:" + score);
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("timeout scene", LoadSceneMode.Additive);
    }
    public void pauseGame()
    {

        paused = true;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("pause menu", LoadSceneMode.Additive);
    }

    public void Unpause()
    {
        paused = false;
        SceneManager.UnloadSceneAsync("pause menu");
        Cursor.lockState = CursorLockMode.Locked;
    }
    public bool isPaused()
    {
        return paused;
    }

    void HideItemAreas()
    {
        GameObject[] areas = GameObject.FindGameObjectsWithTag("ItemArea");
        foreach (GameObject area in areas)
        {
            for(int i = 0; i < area.transform.childCount; i++)
            {
                area.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    private void OnApplicationQuit()
    {
        GameObject[] areas = GameObject.FindGameObjectsWithTag("ItemArea");
        foreach (GameObject area in areas)
        {
            for (int i = 0; i < area.transform.childCount; i++)
            {
                area.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}

