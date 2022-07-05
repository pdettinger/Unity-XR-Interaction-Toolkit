using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class CardHomeFollow : MonoBehaviour
{

    [SerializeField] Transform CardObject; // 
    [SerializeField] Transform HomeObject; // 
    [SerializeField] float MoveSpeed = 50.0f; //
    [SerializeField] float RotateSpeed = 90.0f; //

    Rigidbody _Cardbody;


    void Awake()
    {
        _Cardbody = CardObject.GetComponent<Rigidbody>();  
        _Cardhome = HomeObject.transform;
        _Cardbody.collisionDetectionMode = CollisionDetectionMode.Continuous;   
        _Cardbody.interpolation - RigidbodyInterpolation.Interpolate;  
        _Cardbody.mass = 20f; 
    }

    void Update()
    {
        PhysicsTranspose();
    }

private void PhysicsTranspose()
{
//Position
var distance = Vector3.Distance(_Cardhome.position, _Cardbody.transform);
_Cardbody.velocity = (_Cardhome.position - _Cardbody.position).normalized * (MoveSpeed * distance);
//Rotation
var q = _Cardhome.rotation * Quaternion.Inverse(_Cardbody.rotation);
q.ToAngleAxis(out float angle, out Vector3 axis);
_Cardbody.angularVelocity = axis * (angle * Mathf.Deg2Rad * RotateSpeed);
}

}
