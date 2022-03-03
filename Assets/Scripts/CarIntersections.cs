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

            Debug.Log("I am triggered");

            if (car != null && car != currentCar )
            {
                Debug.Log("I am stopping");
                trafficQueue.Enqueue(car);
                car.Stop = true; 
            }
        }
    }

    private void Update()
    {
        if (currentCar == null)
        {
            if (trafficQueue.Count > 0)
            {
                currentCar = trafficQueue.Dequeue();
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
