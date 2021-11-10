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

    public bool endless = false; //switch to endless mode for debuging

    ScoreCalculator scoreCalculator;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        if (endless)
        {
            gameTimer.gameObject.SetActive(false);
        }
        else
        {
            gameTimer.gameObject.SetActive(true);
        }
        //make sure player camera is enabled
        playerCam.SetActive(true);
        endCam.SetActive(false);

        RandomizeDustSpots();
        dustText.SetActive(false);
        scoreCalculator = gameObject.GetComponent<ScoreCalculator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RandomizeDustSpots()
    {
        GameObject[] spots = GameObject.FindGameObjectsWithTag("Dust Spot");
        int numOfSpots = Random.Range(12, 17);
        int disabled = 0;
        while (disabled < numOfSpots)
        {
            int index = Random.Range(0, spots.Length);
            if (spots[index].activeSelf)
            {
                spots[index].SetActive(false);
                disabled++;
            }
        }
        Debug.Log("spawned " + (spots.Length - numOfSpots) + "dust spots");

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

