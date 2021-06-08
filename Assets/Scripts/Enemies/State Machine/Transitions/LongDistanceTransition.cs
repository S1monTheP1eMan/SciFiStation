using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    private RaycastHit _hit;
    private bool _hitPlayer;
    private int _layerMask = 1 << 11;

    private void Start()
    {
        _layerMask = ~_layerMask;
        _transitionRange += Random.Range(_rangeSpread, _rangeSpread);
    }

    private void Update()
    {
        Physics.Linecast(transform.position, Target.transform.position, out _hit, _layerMask);

        CollideWithPlayer(_hit);

        if (Vector3.Distance(transform.position, Target.transform.position) >= _transitionRange || _hitPlayer == false)
        {
            NeedTransit = true;
        }
        else
        {
            NeedTransit = false;
        }
    }

    private bool CollideWithPlayer(RaycastHit hit)
    {
        if (hit.collider == null)
        {
            return _hitPlayer = false;
        }

        _hitPlayer = hit.collider.TryGetComponent<Player>(out Player player);

        return _hitPlayer;
    }
}
