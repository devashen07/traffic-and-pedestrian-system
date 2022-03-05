using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> objPrefabs = new List<GameObject>(); 

    [SerializeField] 
    public List<GameObject> invalidWaypoints = new List<GameObject>();

    public int objsToSpawn; 


    //private bool carIsAlreadyThereFlag = false;
    private bool invalidWaypointFlag = false; 
    //public int distanceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        

        int count = 0;
        //int placeCarWaypoint = 0; 

        while(count < objsToSpawn)
        {
            GameObject obj = Instantiate(objPrefabs[Random.Range(0, objPrefabs.Count -1)]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            //Transform child = transform.GetChild(placeCarWaypoint);
            obj.GetComponent<Navigator>().currentWaypoint = child.GetComponent<Waypoint>();
            CheckIfInvalid(obj);

            if (invalidWaypointFlag)
            {
                Destroy(obj);
                continue; 
            }

            obj.transform.position = child.position; 
            yield return new WaitForEndOfFrame();
            count++;

           // 
        }
    }

    private void CheckIfInvalid(GameObject obj)
    {
        foreach(var invalid in invalidWaypoints)
        {
            if (obj.GetComponent<Navigator>().currentWaypoint == invalid.GetComponent<Waypoint>())
            {
                invalidWaypointFlag = true; 
                Debug.Log("I am invalid");

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
