using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : ObjectPool
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private EffectsPool _effectsContainer;

    private void OnDisable()
    {
        for (int i = 0; i < PrefabsCount; i++)
        {
            GetPrefab(i).GetComponent<Bullet>().Died -= OnBulletDied;
        }
    }

    private void Start()
    {
        Initialize(_bulletPrefab);
    }

    public void SetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;

        bullet.GetComponent<Bullet>().Died += OnBulletDied;
    }

    private void OnBulletDied(GameObject bullet)
    {
        bullet.GetComponent<Bullet>().Died -= OnBulletDied;
        Transform bulletPosition = bullet.transform;

        _effectsContainer.SetEffect(bulletPosition);
    }
}
