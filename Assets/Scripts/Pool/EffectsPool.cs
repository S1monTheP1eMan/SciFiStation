using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsPool : ObjectPool
{
    [SerializeField] private ParticleSystem _hitEffect;

    private void Start()
    {
        Initialize(_hitEffect.gameObject);
    }

    public void SetEffect(Transform effectPosition)
    {
        if (TryGetObject(out GameObject hitEffect))
        {
            hitEffect.SetActive(true);
            hitEffect.transform.position = effectPosition.position;
            hitEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}
