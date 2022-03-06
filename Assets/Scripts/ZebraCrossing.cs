using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraCrossing : MonoBehaviour
{

    List<MovementController> pedestrianList = new List<MovementController>();
    List<MovementController> carList = new List<MovementController>();

    public bool pedestrianWaiting = false;
    public bool pedestrianWalking = false;
    private bool carMoving = false;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            var pedestrian = other.GetComponent<MovementController>(); 
            if (pedestrian != null && pedestrianList.Contains(pedestrian) == false)
            {
                pedestrianList.Add(pedestrian);
                pedestrian.Stop = true; 
                Debug.Log("I am stopping pedestrian");
                pedestrianWaiting = true; 
                Debug.Log("list count: " + pedestrianList.Count);
            }
        }

        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();
            if (car != null && carList.Contains(car) == false)
            {
                Debug.Log("I am a cake");
                if (pedestrianWaiting  || pedestrianWalking )
                {
                    carList.Add(car); 
                    car.Stop = true; 
                    Debug.Log(pedestrianWaiting);
                }
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (var car in carList)
        {
            if (car.Stop)
            {
                carMoving = false;
                continue;
            }
            else
            {
                carMoving = true;
                break;
            }
            
        }

        if (carMoving == false || carList.Count == 0)
        {
            foreach (var pedestrian in pedestrianList)
            {
                pedestrian.Stop = false;   
                pedestrianWalking = true; 
                Debug.Log("dev: " + pedestrian.Stop);
                Debug.Log("walking: " + pedestrianWalking);
                pedestrianWaiting = false;  
            }
        }

        if (pedestrianWalking == false && pedestrianWaiting == false )
        {
            foreach(var car in carList)
            {
                car.Stop = false; 
                Debug.Log("I am not stopping");
            }
        }
    }

     private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            var pedestrian = other.GetComponent<MovementController>();

            if (pedestrian != null)
            {
                pedestrianList.Remove(pedestrian);
                pedestrianWalking = false; 
            }
        }

        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();

            if (car != null)
            {
                carList.Remove(car);
            }
        }
    }

    
    

    
    
}
