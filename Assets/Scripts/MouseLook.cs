using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private Transform _playerBody;

    private Player _player;
    private float _mouseX;
    private float _mouseY;
    private float _rotationX;

    private void Awake()
    {
        _player = _playerBody.GetComponent<Player>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
    }

    private void Update()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationX -= _mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        transform.localEulerAngles = new Vector3(_rotationX, 0f, 0f);

        _playerBody.rotation = Quaternion.Euler(_playerBody.transform.rotation.eulerAngles + Vector3.up * _mouseX);
    }

    private void OnPlayerDied() 
    {
        enabled = false;
    }
}
