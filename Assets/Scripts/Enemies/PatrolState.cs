using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveRange;

    private Vector3 _targetPosition;
    private bool _positionSet = false;

    private void OnEnable()
    {
        _positionSet = false;
    }

    private void Update()
    {
        if (_positionSet)
        {
            if (Vector3.Distance(transform.position, _targetPosition) <= 1f)
            {
                _positionSet = false;
                return;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            SetRotation();
        }
        else
        {
            SetTargetPosition();
        }
    }

    private void SetRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(_targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void SetTargetPosition()
    {
        float randomX = Random.Range(-_moveRange, _moveRange);
        float randomZ = Random.Range(-_moveRange, _moveRange);

        _targetPosition = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, _targetPosition, out hit))
        {
            _positionSet = false;
        }
        else
        {
            _positionSet = true;
        }
    }
}
