using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for moving both the car and pedestrian prefabs. 
/// It also contains methods for stopping the prefabs at intersections and not 
/// colliding with one another.
/// </summary>


public class MovementController : MonoBehaviour
{
    // Initial Declarations for Prefab Movement
    public float movementSpeed = 4; 
    public float rotationSpeed = 10000; 
    public float stopDistance = 2f; 
    public Vector3 destination; 
    public bool reachedDestination;
    public GameObject type; 

    private Vector3 lastPostion;

    // Declarations for Raycasts 
    public GameObject raycastMiddle = null;
    public GameObject raycastLeft = null;
    public GameObject raycastRight = null;
    public GameObject raycastAngleLeft = null;
    public GameObject raycastAngleRight = null;

    [SerializeField]
    public float collisionRaycastLength = 0.5f;
    
    private bool stop; 
    private bool collisionStop = false; 

    private void Awake()
    {
        // parameters for Car and Pedestrian --> Speed random range 
        if (type.tag == "Car")
        {
            movementSpeed = Random.Range(3f, 5.5f);
            rotationSpeed = 10000f; 
            collisionRaycastLength = 0.3f;
        }
        else 
        {
            movementSpeed = Random.Range(1f, 2f);
            rotationSpeed = 10000f; 
            collisionRaycastLength = 0.1f;
        }

    }

    public bool Stop 
    {
        get { return stop || collisionStop; }
        set { stop = value; }

    }


    // Update is called once per frame
    void Update()
    {
        //Movement travelling to next set waypoint

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
        }

        // Execute stop (ie. speed to zero)
        if (Stop)
        {
            if (type.tag == "Car")
            {
                movementSpeed = 0;
            }
            else
            {
                movementSpeed = 0; 
            }
            
        }
        else
        {
            if (type.tag == "Car")
            {
                movementSpeed = Random.Range(3f, 5.5f);
                
            }
            else 
            {
                movementSpeed = Random.Range(1f, 2f);
                
            }
        }

        CheckCollision();
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false; 
    }
    
    // Identifying hit for Raycast (hence Collision), action is to stop the car so a following distance can be obtained.
    private void CheckCollision()
    {
        if (type.tag == "Car")
        {

        
        if (Physics.Raycast(raycastLeft.transform.position, transform.forward, collisionRaycastLength, 1 << gameObject.layer) || 
            Physics.Raycast(raycastMiddle.transform.position, transform.forward, collisionRaycastLength, 1 << gameObject.layer) ||
            Physics.Raycast(raycastRight.transform.position, transform.forward, collisionRaycastLength, 1 << gameObject.layer) ||
            Physics.Raycast(raycastAngleLeft.transform.position, transform.forward, collisionRaycastLength, 1 << gameObject.layer) ||
            Physics.Raycast(raycastAngleRight.transform.position, transform.forward, collisionRaycastLength, 1 << gameObject.layer))
        {
            collisionStop = true; 
        }
        else
        {
            collisionStop = false; 
        }
        }
    }
}
