using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private Vector3 _targetVelocity;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _velocity = transform.TransformDirection(_targetVelocity);
    }

    private void FixedUpdate()
    {
        _velocity = Vector3.ClampMagnitude(_velocity, 1f);
        _rigidbody.position += _velocity * _speed * Time.deltaTime;
    }
}