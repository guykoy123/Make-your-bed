using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    public float remainingTime = 150;
    TMP_Text clockText;
    public GameManager gameManager;
    bool timeout = false;
    // Start is called before the first frame update
    void Start()
    {
        clockText = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (remainingTime <= 0 && !timeout)
        {
            timeout = true;
            clockText.text = "Time Left: 00:00";
            gameManager.TimeOut();
        }
        else if(!timeout)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            string timeStr;
            if (minutes > 9)
            {
                timeStr = minutes.ToString();
            }
            else
            {
                timeStr = "0" + minutes;
            }
            if (seconds > 9)
            {
                timeStr += ":" + seconds;
            }
            else
            {
                timeStr += ":0" + seconds;
            }
            clockText.text = "Time Left: " + timeStr;
        }
    }

    public bool gotTime()
    {
        return !timeout;
    }
    public float getRemainingTime()
    {
        //returns the remaining time in seconds
        return remainingTime;
    }
}
