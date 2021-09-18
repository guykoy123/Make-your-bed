using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TImeoutSceneManager : MonoBehaviour
{
    ScoreCalculator scoreCalculator;
    public TMP_Text totalScoreText;
    public TMP_Text breakdownText;
    public GameObject breakdownPanel;

    // Start is called before the first frame update
    void Start()
    {
        scoreCalculator = FindObjectOfType<ScoreCalculator>();
        totalScoreText.text = scoreCalculator.CalculateTotal().ToString() + "%";
        Dictionary<string, float> breakdown = scoreCalculator.ScoreBreakdown();
        string breakdownString = "";
        foreach(string key in breakdown.Keys)
        {
            breakdownString += key + ": " + breakdown[key] + "%\r\n";
        }
        breakdownText.text = breakdownString;
        breakdownPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pressMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
    public void pressBreakdown()
    {
        if (breakdownPanel.activeSelf)
        {
            breakdownPanel.SetActive(false);
        }
        else
        {
            breakdownPanel.SetActive(true);
        }
        
    }
}
