using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ScoreCalculator : MonoBehaviour
{
    GameObject[] ItemAreas;
    public FloorController floor;

    // Start is called before the first frame update
    void Start()
    {
        ItemAreas = GameObject.FindGameObjectsWithTag("ItemArea");
    }
    public float CalculateTotal()
    {
        float totalPercent = 0;
        //add up all the scores based on the areas (mattress, pillow ...)
        for(int i = 0; i < ItemAreas.Length; i++)
        {
            totalPercent += ItemAreas[i].GetComponent<PositionCheck>().Check();
        }

        //add score for clean floor
        totalPercent += Mathf.Max(100 - floor.GetItemsOnFloor() * 5,0);  //reduce 5 point for each object on the floor, is a non negative value
        totalPercent = totalPercent / (ItemAreas.Length+1); //get the total percent by calculating average
        return (float)Math.Round(totalPercent,2);
    }

    public Dictionary<string,float> ScoreBreakdown()
    {
        Dictionary<string, float> scores = new Dictionary<string, float>();

        //calculate score for each item type
        int namesCount = Type.GetNames(typeof(Type)).Length;
        string[] typeNames = Type.GetNames(typeof(Type));
        for (int i = 0; i < namesCount; i++)
        {
            scores.Add(typeNames[i], (float)Math.Round(AreaTypeScore(typeNames[i]), 2));
        }

        //calculate floor clean score
        scores.Add("Floor", Mathf.Max(100 - floor.GetItemsOnFloor() * 5, 0));
        return scores;
    }

    private float AreaTypeScore(string areaType)
    {
        int areaTypeAmount = 0;
        float areaTypeScore = 0;
        for (int i = 0; i < ItemAreas.Length; i++)
        {
            if (ItemAreas[i].GetComponent<PositionCheck>().areaType.ToString()==areaType)
            {
                areaTypeScore += ItemAreas[i].GetComponent<PositionCheck>().Check();
                areaTypeAmount++;
            }
        }
        areaTypeScore = areaTypeScore / areaTypeAmount; //calculate the average

        return areaTypeScore;
    }
}
