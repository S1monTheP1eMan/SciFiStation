using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private TMP_Text _score;

    private int _currentHealth;

    public int Score { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction Died;

    private void Awake()
    {
        Time.fixedDeltaTime = 1f / Screen.currentResolution.refreshRate;
    }

    private void Start()
    {
        _currentHealth = _health;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _weapon.Shoot();
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke();
        enabled = false;
    }

    public void OnEnemyDied(int reward)
    {
        Score += reward;
        _score.text = Score.ToString();
    }
}
