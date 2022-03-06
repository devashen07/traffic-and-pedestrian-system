using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for navigating the a car or pedestrian along a path.
/// </summary>

public class Navigator : MonoBehaviour
{
    MovementController controller; 
    public Waypoint currentWaypoint; 

    private void Awake()
    {
        controller = GetComponent<MovementController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDestination)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
