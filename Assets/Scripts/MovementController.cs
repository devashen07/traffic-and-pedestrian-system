using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 4; 
    public float rotationSpeed = 10000; 
    public float stopDistance = 2f; 
    public Vector3 destination; 
    public bool reachedDestination;

    private Vector3 lastPostion;

    public GameObject type; 
    
    
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
        

        //Random.Range(0, 5.5f)

    }

    public bool Stop 
    {
        get { return stop || collisionStop; }
        set { stop = value; }

        //
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

            
        }
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
            //movementSpeed = Random.Range(3f, 5.5f);
        }

        CheckCollision();
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false; 
    }

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
