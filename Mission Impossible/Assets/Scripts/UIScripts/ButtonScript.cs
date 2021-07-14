using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * very small script for buttons
 */ 
public class ButtonScript : MonoBehaviour
{
    public GameObject buttons;
    public GameObject bar;
    public Slider slider;

    // method to load the main game
    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        buttons.SetActive(false);
        bar.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
