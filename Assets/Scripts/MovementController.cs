using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 1; 
    public float rotationSpeed = 120; 
    public float stopDistance = 2f; 
    public Vector3 destination; 
    public bool reachedDestination;

    private Vector3 lastPostion;
    Vector3 velocity;

    private void Awake()
    {
        movementSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0; 
            float destinationDistance = destinationDirection.magnitude; 

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false; 
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }

            else
            {
                reachedDestination = true; 
            }

            velocity = (transform.position - lastPostion) / Time.deltaTime;
            velocity.y = 0; 
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized; 

            //var fwdDotProduct = Vector3.Dot(transform.forward, velocity);

            //transform.position = Vector3.MoveTowards(transform.position, , movementSpeed * Time.deltaTime);
            
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false; 
    }
}
