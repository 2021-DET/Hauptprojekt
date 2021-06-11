using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Script to manage the score showed in the UI
 */
public class ScoreScript : MonoBehaviour
{
    GameObject player;
    // current score
    //public int scoreValue = 0;
    // text in UI for score
    TextMeshProUGUI score;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        // initialize
        score = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // update text to score value
        score.text = "Score: " + player.GetComponent<PlayerScript>().score.ToString();
    }
}
