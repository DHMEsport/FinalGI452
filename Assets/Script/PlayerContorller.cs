using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorller : MonoBehaviour
{
    [SerializeField] private TouchFormPlayer gas;
    [SerializeField] private TouchFormPlayer brake;
    [SerializeField] private Rigidbody2D frontWheel;
    [SerializeField] private Rigidbody2D backWheel;
    [SerializeField] private float speed;
    [SerializeField] private float torque;
    [SerializeField] private float breaktorque;
    private  float movementValue;
       
       
    private void Update()
    {
        if (gas.isTouch && !brake.isTouch)
        {
            movementValue = -torque ;
            Debug.Log("the gas pad touch from player" + movementValue);
        }
        else if (brake.isTouch && !gas.isTouch)
        {
            movementValue = torque  ;
            Debug.Log("the brake pad touch from player" + movementValue);
        }
        else
        {
            movementValue = 0;
        }
           
        Move(movementValue);
    }
   
    private void Move(float torque)
    {
        frontWheel.AddTorque(torque * speed * Time.deltaTime);
        backWheel.AddTorque(torque * speed * Time.deltaTime);
    }
}
