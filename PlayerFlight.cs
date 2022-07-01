using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerFlight2 : MonoBehaviour
{

    [SerializeField] InputActionReference FlyActionReference; // Set to XRI righthand/fly
    [SerializeField] InputActionReference AirbreakActionReference; // Set to XRI righthand/Airbreak
    [SerializeField] Transform TrackingReference; // Drag Your hand gameobject into this field
    [SerializeField] float FlyForce = 50.0f;
    [SerializeField] float AirbreakFactor = 5.0f;
    

    Rigidbody _body;
    Vector3 TurnVector;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _body.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (FlyActionReference.action.IsPressed())
        {
            Vector3 worldRotation = TrackingReference.TransformDirection(Vector3.forward);
            _body.AddForce(worldRotation * FlyForce, ForceMode.Acceleration);        
        } 
        if (!FlyActionReference.action.IsPressed() && AirbreakActionReference.action.IsPressed())
        {
            Vector3 crrntVelocity = _body.velocity;
            _body.AddForce(crrntVelocity * -1 * AirbreakFactor, ForceMode.Acceleration); 
        }
    }
}
