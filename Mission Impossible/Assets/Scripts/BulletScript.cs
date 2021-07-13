using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to manage the actions happening with the bullet
 */ 
public class BulletScript : MonoBehaviour
{
    // collision with object
    private void OnCollisionEnter(Collision collision)
    {
        // if it hits an enemy
        if (collision.transform.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.transform.GetComponent<EnemyScript>();
            enemyScript.takeDamage();
        }
        // delete the bullet after a small delay
        Destroy(this.gameObject, 0.1f);
    }
}
