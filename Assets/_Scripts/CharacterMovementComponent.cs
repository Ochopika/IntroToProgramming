using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementComponent : MonoBehaviour
{

    public float Acceleration = 100.0f;

    private float CurrentMaxVelocity;
    public float MaxWalkVelocity = 10.0f;
    public float MaxSprintVelocity = 20.0f;

    //Current RB on main object
    private Rigidbody RB;

    //Player Input
    Vector3 InputVector;
    private bool Sprint;
    

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    //Dynamic function able to move any RB based on horizontal and vertical input
    public void Movement(Rigidbody rb, Vector3 IPVector, float Accel, float MaxVel)
    {

        IPVector.x = Input.GetAxisRaw("Horizontal");
        IPVector.z = Input.GetAxisRaw("Vertical");
        Sprint = Input.GetKey(KeyCode.LeftShift);

        if (Sprint)
        {
            CurrentMaxVelocity = MaxSprintVelocity;
        }
        else
        {
            CurrentMaxVelocity = MaxWalkVelocity;
        }
       

        rb.AddForce(IPVector * Accel * Time.deltaTime);
            //set velocity to the clamp we created
        rb.velocity = VectorClamp(rb.velocity, -MaxVel, MaxVel, true, false);
    }

    //Takes in one vector and stops all axis from going out of bounds of set parameter
    //void VectorClamp(out Vector3 CurrentVector, float Min, float Max)
    //{
    //    CurrentVector = Vector3.zero; 

    //   // return Vector3.zero;

    //}

    Vector3 VectorClamp(Vector3 CurrentVector, float Min, float Max)
    {
        Vector3 Result = CurrentVector;   

        CurrentVector.x = Mathf.Clamp(CurrentVector.x, Min, Max);

        CurrentVector.x = Mathf.Clamp(CurrentVector.y, Min, Max);

        CurrentVector.x = Mathf.Clamp(CurrentVector.z, Min, Max);

        return Vector3.zero;

    }

    Vector3 VectorClamp(Vector3 CurrentVector, float Min, float Max, bool ClampX = true, bool ClampY = true, bool ClampZ = true)
    {
        Vector3 Result = CurrentVector;

        if (ClampX == true)
        {
            Result.x = Mathf.Clamp(CurrentVector.x, Min, Max);
        }

        if (ClampX == true)
        {
            Result.y = Mathf.Clamp(CurrentVector.y, Min, Max);
        }

        if (ClampX == true)
        {
            Result.z = Mathf.Clamp(CurrentVector.z, Min, Max);
        }

        return Result;

    }

    // Update is called once per frame
    void Update()
    {
        Movement(RB, InputVector, Acceleration, CurrentMaxVelocity);
    }
}
