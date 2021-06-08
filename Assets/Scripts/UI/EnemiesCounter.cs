using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _counter;

    private void OnEnable()
    {
        _spawner.EnemiesCountChanged += OnEnemiesCountChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemiesCountChanged -= OnEnemiesCountChanged;
    }

    private void OnEnemiesCountChanged(int enemiesCount)
    {
        _counter.text = enemiesCount.ToString();
    }
}
