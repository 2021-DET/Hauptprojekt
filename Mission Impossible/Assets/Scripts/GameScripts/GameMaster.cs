using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public int countdownTime;
    public GameObject gameplay;
    public TextMeshProUGUI countdownText;
    private PlayerScript player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(CountdownToStart());
        player.playerCanMove = false;
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
        countdownText.gameObject.SetActive(false);
        gameplay.SetActive(false);
        TimerController.instance.BeginTimer();
    }
}
