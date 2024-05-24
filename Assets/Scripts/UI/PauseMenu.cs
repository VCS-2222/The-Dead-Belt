using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] bool isPaused;
    [SerializeField] GameObject pauseMenuCanvus;
    [SerializeField] Button firstSelectedButton;
    [SerializeField] AudioSource pauseMenuBreathing;
    public Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void Start()
    {
        pauseMenuBreathing.ignoreListenerPause = true;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (controls.UI.Cancel.WasPerformedThisFrame())
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvus.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        firstSelectedButton.Select();
    }

    public void UnpauseGame()
    {
        isPaused = false;
        pauseMenuCanvus.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    public void GoBackToMenu()
    {
        UnpauseGame();
        SceneManager.LoadScene(0);
    }
}