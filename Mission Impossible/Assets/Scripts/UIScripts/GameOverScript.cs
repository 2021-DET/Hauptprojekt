using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public static GameOverScript instance;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI score;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI scoreText;
    private void OnEnable()
    {
        timer.text = "TIME PLAYED: " + timerText.text;
        gold.text = "COLLECTED " + goldText.text;
        score.text = "REACHED " + scoreText.text;
    }

    private void Start()
    {
        instance = this;
    }

    public void StartNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void StartMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
