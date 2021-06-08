using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private Player _player;

    private float _rayDistance = 1.2f;
    private bool _hitDetected = false;
    private bool _hideAnimationHasPlayed = true;
    private bool _bringupAnimationHasPlayed = false;
    private RaycastHit _hit;

    private void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out _hit, _rayDistance);

        CheckCollision(_hit);

        if (_hitDetected == true && _hideAnimationHasPlayed == false)
        {
            _gunAnimator.Play("HideGun");
            _hideAnimationHasPlayed = true;
            _bringupAnimationHasPlayed = false;
            _player.enabled = false;
        }

        if (_hitDetected == false && _bringupAnimationHasPlayed == false)
        {
            _gunAnimator.Play("BringUpGun");
            _hideAnimationHasPlayed = false;
            _bringupAnimationHasPlayed = true;
            _player.enabled = true;
        }
    }

    private void CheckCollision(RaycastHit hit)
    {
        if (hit.collider == null || hit.collider.GetComponent<Bullet>())
        {
            _hitDetected = false;
        }
        else
        {
            _hitDetected = true;
        }
    }
}