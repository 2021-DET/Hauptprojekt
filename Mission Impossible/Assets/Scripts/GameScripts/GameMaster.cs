using System.Collections;
using UnityEngine;
using TMPro;

/**
 * GM Script that handles the start of the game
 * Countdown implemented here
 */
public class GameMaster : MonoBehaviour
{
    public int countdownTime;
    public GameObject countdownUI;
    public TextMeshProUGUI countdownText;
    public PlayerScript player;

    private void Start()
    {
        player.playerCanMove = false;
        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
            countdownTime--;
        }
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);
        player.playerCanMove = true;
        countdownText.gameObject.SetActive(false); //deactivate Countdown Text
        countdownUI.SetActive(false); //deactivate Countdown UI
        TimerController.instance.BeginTimer(); //begin timer UI
    }
}
