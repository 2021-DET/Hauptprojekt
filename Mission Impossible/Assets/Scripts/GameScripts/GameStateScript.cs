using UnityEngine;
using TMPro;

/**
 * Script to organize the GameState UI
 **/
public class GameStateScript : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI goldValue;
    public TextMeshProUGUI ammoValue;
    public PlayerScript player;

    void Update()
    {
        //update the UI text
        scoreValue.text = "SCORE: " + player.GetComponent<PlayerScript>().score.ToString();
        goldValue.text = "GOLD: " + player.GetComponent<PlayerScript>().gold.ToString();
        ammoValue.text = "SHOTGUN: " + player.GetComponent<PlayerScript>().ammo.ToString() + "/" + player.maxAmmo.ToString();
    }
}
