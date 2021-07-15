using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ButtonScript : MonoBehaviour
{
    public GameObject buttons;
    public GameObject bar;
    public Slider slider;
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
