using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerFlight : MonoBehaviour
{

    [SerializeField] InputActionReference FlyActionReference; // Set to XRI righthand/fly
    [SerializeField] Transform TrackingReference; // Drag Your hand gameobject into this field
    [SerializeField] private float FlyForce = 50.0f;
    [SerializeField] bool StopImmediately = true;

    Rigidbody _body;

    bool FlightState;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _body.constraints = RigidbodyConstraints.FreezeRotation;
        FlightState = false;
    }

    void FixedUpdate()
    {
        if (FlyActionReference.action.IsPressed())
        {
            FlightState = true;
            Vector3 worldRotation = TrackingReference.TransformDirection(Vector3.forward);
            _body.AddForce(worldRotation * FlyForce, ForceMode.Acceleration);        
        } 
        if (!FlyActionReference.action.IsPressed() && StopImmediately)
        {
            

            if (FlightState)
            {
            _body.constraints = RigidbodyConstraints.FreezePosition; // Stop immediately
            _body.constraints = RigidbodyConstraints.None; // turn gravity back on
            _body.constraints = RigidbodyConstraints.FreezeRotation; //prevent rotations
            FlightState = false;
            }

        
        }
    }
}
