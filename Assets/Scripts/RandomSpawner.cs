using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public int carsToSpawn; 

    private bool carIsAlreadyThereFlag = false;
    //public int distanceMultiplier;

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
            GameObject obj = Instantiate(carPrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<Navigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position; 

            if (carIsAlreadyThereFlag)
            {
                Destroy(obj);
                continue; 
            }

            yield return new WaitForEndOfFrame();

            count++;

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Car"))
        {
            carIsAlreadyThereFlag = true;      
        }
        else
        {
            carIsAlreadyThereFlag = false; 
        }
    }
}
