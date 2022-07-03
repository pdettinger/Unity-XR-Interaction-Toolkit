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
    [SerializeField] float TurnFactor = 1.0f;
    [SerializeField] float AirbreakFactor = 5.0f;
    

    Rigidbody _body;

    void Awake()
    {
        _body = GetComponent<Rigidbody>();
        _body.constraints = RigidbodyConstraints.FreezeRotation;
        
    }

    void FixedUpdate()
    {
        Vector3 crrntVelocity = _body.velocity;
        if (FlyActionReference.action.IsPressed())
        {
            Vector3 TrackingAcceleration = TrackingReference.TransformDirection(Vector3.forward);
            Vector3 TurnAmp = Vector3.zero;
            if (crrntVelocity != Vector3.zero) 
            {
            Vector3 bodyDirection = crrntVelocity.normalized;
            Vector3 TrackingDirection = TrackingAcceleration.normalized;
            TurnAmp = (TrackingDirection - bodyDirection) * TurnFactor;
            }
            _body.AddForce((TrackingAcceleration + TurnAmp) * FlyForce, ForceMode.Acceleration);        
        }
        if (AirbreakActionReference.action.IsPressed())
        {
            _body.AddForce(crrntVelocity * -1 * AirbreakFactor, ForceMode.Acceleration); 
        }
    }
}
