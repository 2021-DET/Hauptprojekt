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
            col.transform.root.GetComponent<PlayerScript>().gold += value;
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    //TODO: bei Spielerberührung zerstören, Punkte geben
}
