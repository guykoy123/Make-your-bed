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
    // Start is called before the first frame update
    void Start()
    {
        scoreCalculator = gameObject.GetComponent<ScoreCalculator>();
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = "current score:" + scoreCalculator.CalculateTotal();
    }

    public void TimeOut()
    {
        float score = scoreCalculator.CalculateTotal();
        Debug.Log("ran out of time, score:" + score);
        SceneManager.LoadScene("timeout scene", LoadSceneMode.Additive);
    }
}
