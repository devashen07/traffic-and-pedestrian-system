using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarIntersections : MonoBehaviour
{
    Queue<MovementController> trafficQueue = new Queue<MovementController>(); 

    public MovementController currentCar; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I am at an intersection");

        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();
            if (car != null && car != currentCar )
            {
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
                currentCar = trafficQueue.Dequeue();
                //currentCar.collisionRaycastLength = 0f; 
                currentCar.Stop = false;  
            }
           
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();

            if (car != null)
            {
                //car.collisionRaycastLength = 0.5f; 
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
