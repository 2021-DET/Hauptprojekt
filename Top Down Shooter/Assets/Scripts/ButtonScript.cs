using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * very small script for buttons
 */ 
public class ButtonScript : MonoBehaviour
{
    // method to load the main game
    public void PlayGame ()
    {
        SceneManager.LoadScene(0);
    }
}
