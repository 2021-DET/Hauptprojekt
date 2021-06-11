using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Script to organize the state of the game, score etc.
 **/
public class GameStateScript : MonoBehaviour
{
    GameObject player;
    // current score
    //public int scoreValue = 0;
    // text in UI for score
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI goldValue;
    public TextMeshProUGUI ammoValue;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player") as GameObject;
        // initialize
    }

    void Update()
    {
        // update text to score value
        scoreValue.text = "Score: " + player.GetComponent<PlayerScript>().score.ToString();
        goldValue.text = "Gold: " + player.GetComponent<PlayerScript>().gold.ToString();
        ammoValue.text = "Salve Shots: " + player.GetComponent<PlayerScript>().ammo.ToString() + "/" + player.GetComponent<PlayerScript>().maxAmmo.ToString();
    }
}
