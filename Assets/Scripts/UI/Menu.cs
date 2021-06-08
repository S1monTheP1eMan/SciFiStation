using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    [SerializeField] protected Button RestartButton;
    [SerializeField] protected Button ExitButton;
    [SerializeField] protected Player Player;

    protected CanvasGroup CanvasGroup;
    protected Vector2 Position;

    private void OnEnable()
    {
        Player.Died += OnDied;
        RestartButton.onClick.AddListener(OnRestartButtonClick);
        ExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        Player.Died -= OnDied;
        RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        ExitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
        Position = CanvasGroup.gameObject.transform.position;
        CanvasGroup.gameObject.transform.position = Vector2.zero;
    }

    private void OnDied()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.gameObject.transform.position = Position;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    protected void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    protected void OnExitButtonClick()
    {
        Application.Quit();
    }
}