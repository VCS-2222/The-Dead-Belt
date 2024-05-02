using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] GameObject loadBar;
    [SerializeField] Button startSelectButton;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        loadBar.SetActive(false);
        startSelectButton.Select();
    }

    public void PlayGame()
    {
        loadBar.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            loadBar.GetComponentInChildren<Slider>().value = operation.progress;

            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}