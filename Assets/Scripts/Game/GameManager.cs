using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    ScoreCalculator scoreCalculator;
    public Timer gameTimer;
    public Text currentScoreText;
    bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        scoreCalculator = gameObject.GetComponent<ScoreCalculator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TimeOut()
    {
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
        ItemAudioPlayer[] itemAudio = FindObjectsOfType<ItemAudioPlayer>();
        foreach (ItemAudioPlayer item in itemAudio)
        {
            item.UpdateVolume();
        }
    }
    public bool isPaused()
    {
        return paused;
    }
}

