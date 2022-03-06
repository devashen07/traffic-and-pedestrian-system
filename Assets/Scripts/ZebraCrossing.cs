using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for the zebra crossing interactions. 
/// Cars must yield if a pedestrian is waiting or currently crossing the intersection. 
/// </summary>

public class ZebraCrossing : MonoBehaviour
{

    List<MovementController> pedestrianList = new List<MovementController>();
    List<MovementController> carList = new List<MovementController>();

    public bool pedestrianWaiting = false;
    public bool pedestrianWalking = false;
    private bool carMoving = false;
   
    // If pedestrian or car hits zebra crossing's box collider 
    private void OnTriggerEnter(Collider other)
    {
        // Check whether pedestrian using tag
        if (other.CompareTag("Pedestrian"))
        {
            var pedestrian = other.GetComponent<MovementController>(); 
            
            if (pedestrian != null && pedestrianList.Contains(pedestrian) == false)
            {
                pedestrianList.Add(pedestrian);
                pedestrian.Stop = true; 

                // Set Waiting Flag to true 
                pedestrianWaiting = true; 
            }
        }
        // Check if car 
        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();
            if (car != null && carList.Contains(car) == false)
            {
                // If a pedestrian is waiting or walking across
                if (pedestrianWaiting  || pedestrianWalking )
                {
                    // Add car to list and stop it 
                    carList.Add(car); 
                    car.Stop = true; 
                }
            }
        }
    }

    public void FixedUpdate()
    {
        // if a car is currently crossing as a pedestrian arrives set carMoving flag
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

        // if a car is not moving or no cars are present, let pedestrian walk across
        if (carMoving == false || carList.Count == 0)
        {
            foreach (var pedestrian in pedestrianList)
            {
                pedestrian.Stop = false;   
                pedestrianWalking = true; 
                pedestrianWaiting = false;  
            }
        }

        // If no pedestrian is waiting or walking across, let car move across
        if (pedestrianWalking == false && pedestrianWaiting == false )
        {
            foreach(var car in carList)
            {
                car.Stop = false; 
                Debug.Log("I am not stopping");
            }
        }
    }

    // Remove pedestrian from list and car from list as well once exited the collider 
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
