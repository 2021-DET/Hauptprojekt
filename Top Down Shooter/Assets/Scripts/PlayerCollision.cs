using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Script to handle player collisions
 */ 
public class PlayerCollision : MonoBehaviour
{
    // reference to the try again menu
    public int nextMenu = 2;
    private void OnCollisionEnter(Collision collision)
    {
        // if player hits an enemy
        if (collision.collider.tag == "Enemy")
        {
            // load try again menu for a new game
            SceneManager.LoadScene(nextMenu);
        }
    }
}
