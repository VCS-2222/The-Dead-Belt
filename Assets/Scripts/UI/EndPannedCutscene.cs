using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPannedCutscene : MonoBehaviour
{
    public string[] sentances;
    [SerializeField] float scrollSpeed;
    [SerializeField] TextMeshProUGUI cutsceneText;
    [SerializeField] Button menuButton;

    private void Start()
    {
        StartCoroutine(Cutscene());
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator Cutscene()
    {
        menuButton.Select();

        for (int i = 0; i < sentances.Length; i++)
        {
            yield return new WaitForSeconds(scrollSpeed);

            cutsceneText.text = sentances[i];
        }
    }
}