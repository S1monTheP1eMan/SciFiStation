using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _count;

    private void OnEnable()
    {
        _spawner.WaveChanged += OnWaveCountChanged;
    }

    private void OnDisable()
    {
        _spawner.WaveChanged -= OnWaveCountChanged;
    }

    private void OnWaveCountChanged(int waveNumber)
    {
        _count.text = waveNumber.ToString();
    }
}
