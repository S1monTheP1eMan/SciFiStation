using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _prefabs = new List<GameObject>();

    public int PrefabsCount { get; private set; }

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);

            _prefabs.Add(spawned);
        }

        PrefabsCount = _prefabs.Count;
    }

    protected void Initialize(GameObject[] enemies, Player target)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject spawned = Instantiate(enemies[randomIndex], _container.transform);
            spawned.GetComponent<Enemy>().Init(target);
            spawned.SetActive(false);

            _prefabs.Add(spawned);
        }

        PrefabsCount = _prefabs.Count;
    }

    protected void AddPrefabs(GameObject[] enemies, Player target, int prefabsNumber)
    {
        for (int i = 0; i < prefabsNumber; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject spawned = Instantiate(enemies[randomIndex], _container.transform);
            spawned.GetComponent<Enemy>().Init(target);
            spawned.SetActive(false);

            _prefabs.Add(spawned);
        }

        PrefabsCount = _prefabs.Count;
    }

    public bool TryGetObject(out GameObject result)
    {
        result = _prefabs.FirstOrDefault(predicate => predicate.activeSelf == false);

        return result != null;
    }

    protected GameObject GetPrefab(int index)
    {
        return _prefabs[index];
    }
}
