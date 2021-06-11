using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldScript : MonoBehaviour
{
    // current gold
    PlayerScript player;
    // text in UI for score
    TextMeshProUGUI score;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        // initialize
        score = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // update text to score value
        score.text = "Gold: " + player.gold.ToString();
    }
}
