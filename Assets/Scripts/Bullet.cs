using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    private float _rayDistance = 3f;
    private float _maxLifeTime = 5f;
    private float _elapsedTime;
    private bool _positionSet = false;
    private RaycastHit _hit;

    public event UnityAction<GameObject> Died;

    private void OnEnable()
    {
        _positionSet = false;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _maxLifeTime)
        {
            _elapsedTime = 0f;
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (_positionSet)
        {
            Die();
            return;
        }

        if (Physics.Raycast(transform.position, transform.forward, out _hit, _rayDistance))
        {
            if (_hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }

            transform.position = _hit.point;
            _positionSet = true;

            return;
        }

        transform.Translate(transform.forward * _speed * Time.fixedDeltaTime, Space.World);
    }

    private void Die()
    {
        Died?.Invoke(gameObject);
        gameObject.SetActive(false);
    }
}
