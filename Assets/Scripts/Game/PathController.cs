using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathController : MonoBehaviour
{
    
    List<Transform> Waypoints = new List<Transform>();

    bool finished = false;
    int nextWaypoint = 0;

    bool hasMinDis = false;
    float minimumDistance;

    // Start is called before the first frame update
    void Awake()
    {
        Transform[] temp = gameObject.GetComponentsInChildren<Transform>();
        for(int i = 1; i < temp.Length; i++)
        {
            Waypoints.Add(temp[i]);
        }
        Debug.Log(" waypoints-" + (Waypoints.Count));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool isFinished()
    {
        return finished;
    }

    public void setMinDistance(float dis)
    {
        if (dis >= 0)
        {
            hasMinDis = true;
            minimumDistance = dis;
        }
        else
        {
            throw new ArgumentOutOfRangeException("minimum distance must be non negtive");
        }
    }
    public Vector3 GetDirection(Vector3 currentPos)
    {
        /*
         * get distance to waypoint
         * if distance is greater the the set minimum return the direction to point
         * if reached the point then move to next point until reaching the end
         */
        if (!hasMinDis)
        {
            throw new ArgumentNullException("minimum distance");
        }
        else
        {
            
            float distanceToPoint = (currentPos - Waypoints[nextWaypoint].position).magnitude;
            if (distanceToPoint > minimumDistance)
            {
                Vector3 direction = Waypoints[nextWaypoint].position - currentPos;
                direction.y = 0;
                Vector3 normalized = direction / direction.magnitude;
                return normalized;
            }
            else
            {
                if (nextWaypoint + 1 == Waypoints.Count)
                {
                    finished = true;
                    return Vector3.zero;
                }
                else
                {
                    Debug.Log("reached point " + nextWaypoint);
                    nextWaypoint++;
                    return Vector3.zero;
                }
            }
        }

    }
}



