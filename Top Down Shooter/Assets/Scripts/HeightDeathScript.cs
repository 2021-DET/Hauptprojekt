using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * very small script for falling out of map
 * used for enemies in case they fall
 * Enemy game objects need to be destroyed for the WaveSpawner script
 */
public class HeightDeathScript : MonoBehaviour
{
    void Update()
    {
        // below ground level
        if (gameObject.transform.position.y <= -5f)
        {
            Destroy(gameObject, 0f); // delete game objecct
        }
    }
}
