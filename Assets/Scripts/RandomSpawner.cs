using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for spawning cars and pedestrians onto an appropiate path. 
/// </summary>

public class RandomSpawner : MonoBehaviour
{
    // Gameobjects to be spawned, helps to determine whether you want to spawn a car or pedestrian
    [SerializeField]
    public List<GameObject> carPrefabs = new List<GameObject>(); 

    // List waypoints where spawning is not allowed 
    [SerializeField] 
    public List<GameObject> invalidWaypoints = new List<GameObject>();

    public int carsToSpawn; 

    private bool invalidWaypointFlag = false; 
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;

        while(count < carsToSpawn)
        {
            // Randomly select a car or pedestrian from prefabs 
            GameObject obj = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count -1)]);

            // Get a waypoint
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<Navigator>().currentWaypoint = child.GetComponent<Waypoint>();

            // Check if its non-spawning waypoint
            CheckIfInvalid(obj);
            if (invalidWaypointFlag)
            {
                // If it is destroy the gameObject and start again
                Destroy(obj);
                continue; 
            }

            // Place car or pedestrian at waypoint position
            obj.transform.position = child.position; 
            yield return new WaitForEndOfFrame();
            count++;

            
        }
    }

    private void CheckIfInvalid(GameObject obj)
    {
        foreach(var invalid in invalidWaypoints)
        {
            // compare if waypoint is equal to any waypoints in List 
            if (obj.GetComponent<Navigator>().currentWaypoint == invalid.GetComponent<Waypoint>())
            {
                // if waypoint is invalid set flag and break
                invalidWaypointFlag = true; 

                if (invalidWaypointFlag)
                {
                    break; 
                }
            }
            else
            {
                invalidWaypointFlag = false; 
            }
        }
    }

   
}
