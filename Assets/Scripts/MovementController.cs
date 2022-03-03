using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 1; 
    public float rotationSpeed = 360; 
    public float stopDistance = 2f; 
    public Vector3 destination; 
    public bool reachedDestination;

    private Vector3 lastPostion;
    
    
    public GameObject raycastStartingPoint = null;

    [SerializeField]
    private float collisionRaycastLength = 0.1f;
    
    private bool stop; 
    private bool collisionStop = false; 

    private void Awake()
    {
        
        movementSpeed = Random.Range(3f,6f);
        rotationSpeed = 240f; 

    }

    

    public bool Stop 
    {
        get { return stop || collisionStop; }
        set { stop = value; }
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
            movementSpeed = 0; 
        }
        else
        {
            movementSpeed = Random.Range(4f, 6f);
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
        if (Physics.Raycast(raycastStartingPoint.transform.position, transform.forward, collisionRaycastLength, 1 << gameObject.layer))
        {
            collisionStop = true; 
        }
        else
        {
            collisionStop = false; 
        }
    }
}
