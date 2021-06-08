using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : Menu
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private MouseLook _camera;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
        Player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
        Player.Died -= OnPlayerDied;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasGroup.alpha = 1;
            CanvasGroup.gameObject.transform.position = Position;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Player.enabled = false;
            _camera.enabled = false;
        }
    }

    private void OnResumeButtonClick()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.gameObject.transform.position = Vector2.zero;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Player.enabled = true;
        _camera.enabled = true;
    }

    private void OnPlayerDied()
    {
        gameObject.SetActive(false);
    }
}
