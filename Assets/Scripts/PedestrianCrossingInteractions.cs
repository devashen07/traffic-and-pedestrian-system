using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PedestrianCrossingInteractions : MonoBehaviour
{
    List<MovementController> pedestrianList = new List<MovementController>();

    [field: SerializeField]
    public UnityEvent OnPedestrianEnter { get; set; }

    [field: SerializeField]
    public UnityEvent OnPedestrianExit { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            var pedestrian = other.GetComponent<MovementController>();
            if (pedestrian != null && pedestrianList.Contains(pedestrian) == false)
            {
                pedestrianList.Add(pedestrian);
                pedestrian.Stop = false; 

                OnPedestrianEnter?.Invoke();

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
                RemovePedestrian(pedestrian);
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


}
