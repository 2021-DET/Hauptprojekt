using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int value = 5;


    private void OnTriggerEnter(Collider col)
    {
        // if player hits coins
        if (col.gameObject.tag == "Player")
        {

                // increase score value
                //score.GetComponent<ScoreScript>().scoreValue++;
                col.gameObject.GetComponent<PlayerScript>().gold += value;
            Destroy(gameObject);
        }
    }
    //TODO: bei Spielerber�hrung zerst�ren, Punkte geben
}
