using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _hasSpawned = false;

    public bool HasSpawned => _hasSpawned;

    public void SpawnedObject()
    {
        _hasSpawned = true;
    }

    public void Reset()
    {
        _hasSpawned = false;
    }
}
