using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private BulletsPool _pool;

    private AudioSource _gunShotEffect;

    private void Awake()
    {
        _gunShotEffect = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (_pool.TryGetObject(out GameObject bullet))
        {
            _pool.SetBullet(bullet);
            PlaySoundEffect();
        }
    }

    private void PlaySoundEffect()
    {
        float randomPitch = Random.Range(1f, 1.12f);
        _gunShotEffect.pitch = randomPitch;
        _gunShotEffect.Play();
    }
}
