using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _reward;

    private Player _target;
    private int _health;

    public Player Target => _target;

    public event UnityAction<int, GameObject> Died;

    public void Init(Player target)
    {
        _health = _maxHealth;
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            gameObject.SetActive(false);
            _health = _maxHealth;
            
            Died?.Invoke(_reward, gameObject);
        }
    }
}
