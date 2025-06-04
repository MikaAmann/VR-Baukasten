using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ElevatorController : MonoBehaviour
{
    public GameObject Elevator;
    public GameObject XROrigin;
    private bool isMoving = false;
    private Vector3 moveDirection;
    public float moveSpeed = 0.5f;
    private Vector3 startPos;
    public float maxHeight = 15f;
    private float minHeight;
    public PlayerOnPlattformManager pop;


    private void Awake()
    {
       minHeight = Elevator.transform.position.y;
       pop = Elevator.GetComponent<PlayerOnPlattformManager>(); 
    }

    void Update()
    {
        if (isMoving)
        {
            MovePlatform(moveDirection, moveSpeed);
        }
    }
    
    void MovePlatform(Vector3 direction, float speed)
    {
        Vector3 newPosition = Elevator.transform.position + direction * (speed * Time.deltaTime);
        Vector3 newPlayerPosition = XROrigin.transform.position + direction * (speed * Time.deltaTime);
        float newY = newPosition.y;
        float minY = startPos.y + minHeight;
        float maxY = startPos.y + maxHeight;

        if (newY >= minY && newY <= maxY)
        {
            Elevator.transform.position = newPosition;
            if (pop.playerOnPlattform)
            {
                XROrigin.transform.position = newPlayerPosition;
            }
            
        }
    }

    
    public void StartMovingUp()  
    {
        isMoving = true;
        moveDirection = Vector3.up;
    }

    public void StartMovingDown()  
    {
        isMoving = true;
        moveDirection = Vector3.down;
    }

    public void StopMoving()  
    {
        isMoving = false;
    }


}
