using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Item endItem;

    public void FeedPackageToKiosk()
    {
        if (Inventory.Instance.ReturnItem(endItem))
        {
            StartCoroutine(LoadAsync());
        }
        else
        {
            print("no package, dummy");
        }
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(4);

        while (!operation.isDone)
        {
            //loadBar.GetComponentInChildren<Slider>().value = operation.progress;
            print("Loading Ending");

            yield return null;
        }
    }
}