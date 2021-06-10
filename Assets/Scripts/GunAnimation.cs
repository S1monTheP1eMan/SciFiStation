using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private Player _player;

    private List<Collider> _colliders = new List<Collider>();
    private bool _hideAnimationHasPlayed = false;
    private bool _bringupAnimationHasPlayed = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            return;
        }

        _colliders.Add(other);

        if (_colliders.Count == 1)
        {
            _gunAnimator.Play("HideGun");
            _player.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _colliders.Remove(other);

        if (_colliders.Count == 0)
        {
            _gunAnimator.Play("BringUpGun");
            _player.enabled = true;
        }
    }
}