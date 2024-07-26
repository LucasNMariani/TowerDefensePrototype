using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWaypointAssigner : MonoBehaviour
{
    public static FirstWaypointAssigner firstWaypointAssigner;

    public Transform firstWaypoint;

    private void Awake()
    {
        firstWaypointAssigner = this;
    }

    public Transform GetFirstWaypoint()
    {
        return firstWaypoint;
    }
}
