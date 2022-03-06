using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for how cars move through an intersection. 
/// According to the principle of First In First Out (FIFO).
/// </summary>


public class CarIntersections : MonoBehaviour
{
    Queue<MovementController> trafficQueue = new Queue<MovementController>(); 

    public MovementController currentCar; 

    // Car hits intersection's box collider 
    private void OnTriggerEnter(Collider other)
    {
        // See whether its a car using tags
        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();
            if (car != null && car != currentCar )
            {
                // Add car to queue and stop car 
                trafficQueue.Enqueue(car);
                car.Stop = true; 
                
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentCar == null)
        {
            if (trafficQueue.Count > 0)
            {
                // Dequeue cars in an orderly fashion 
                currentCar = trafficQueue.Dequeue();
                currentCar.Stop = false;  
            }
           
        }
        
    }

    // Car exits intersections box collider, remove car 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();

            if (car != null)
            {
                RemoveCar(car);
                
            }
        }
    }

    private void RemoveCar(MovementController car)
    {
        if (car == currentCar)
        {
            currentCar = null; 
        }
    }

}
