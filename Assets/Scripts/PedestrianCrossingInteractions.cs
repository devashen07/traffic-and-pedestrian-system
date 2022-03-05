using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PedestrianCrossingInteractions : MonoBehaviour
{
    List<MovementController> pedestrianList = new List<MovementController>();
    Queue<MovementController> trafficQueue = new Queue<MovementController>(); 
    public MovementController currentCar;
    public MovementController currentPedestrian; 

    [field: SerializeField]
    public UnityEvent OnPedestrianEnter { get; set; }

    [field: SerializeField]
    public UnityEvent OnPedestrianExit { get; set; }

    private bool pedestrianWaiting = false; 
    private bool pedestrianWalking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            var pedestrian = other.GetComponent<MovementController>();
        }
        if (other.CompareTag("car"))
        {
            var car = other.GetComponent<MovementController>();
            trafficQueue.Enqueue(car);
        }

        if (pedestrian != null && pedestrianList.Contains(pedestrian) == false)
        {
            
            pedestrianList.Add(pedestrian);
            if (trafficQueue.Count > 0)
            {
                pedestrianWaiting = true; 
                pedestrian.Stop = true; 
            }
            else
            {
                pedestrianWalking = true; 
                pedestrianWaiting = false; 
            }
                 //OnPedestrianEnter?.Invoke();
        }
        if (pedestrianWaiting || pedestrianWalking)
        {
            
            car.Stop = true; 
        }

    }

    private void FixedUpdate()
    {
        if (pedestrianWaiting)
        {
            pedestrian.Stop = false; 
            pedestrianWaiting = false; 
            pedestrianWalking = true;
        }
        if (currentCar == null)
        {
            if (!pedestrianWaiting || !pedestrianWalking)
            {
                currentCar = trafficQueue.Dequeue();
                //currentCar.collisionRaycastLength = 0f; 
                currentCar.Stop = false;  
            }
           
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            var pedestrian = other.GetComponent<MovementController>();
            if (pedestrian !- null)
            {
                RemovePedestrian(pedestrian);
                pedestrianWalking = false;
                
            }
        }

        if (other.CompareTag("Car"))
        {
            var car = other.GetComponent<MovementController>();

            if (car != null)
            {
                RemoveCar(car);
                
            }
        }
    }

   

    private void RemovePedestrian(MovementController pedestrian)
    {
        pedestrianList.Remove(pedestrian);
        if(pedestrianList.Count <= 0)
        {
            OnPedestrianExit?.Invoke();
        }
    }

    public void MovePedestrians()
    {
        foreach (var pedestrian in pedestrianList)
        {
            pedestrian.Stop = false; 
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
