using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for the enemy objects
 */ 
public class EnemyScript : MonoBehaviour
{
    // public boolean for the status of the enemy
    private int hp;
    // reference to the player
    GameObject player;
    // speed value
    public float speed = 4f;
    // rotation value
    float rotationSpeed = 1f;
    // reference to the rigidbody of the game object
    private Rigidbody rb;
    // reference to explosion object
    public GameObject explosionPrototype;
    // reference to the score object
    public int health;
    GameObject score;
    public int vision = 15;
    public AudioClip expl;
    public int points;

    void Start()
    {
        hp = health;
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // boolean can be set by other scripts
        if (hp <= 0)
        {
            // generate explosion
            GameObject explosion = Instantiate (explosionPrototype , transform.position , transform.rotation);
            explosion.GetComponent<AudioSource>().PlayOneShot(expl);
            if (player != null)
            {
                player.GetComponent<PlayerScript>().score +=  points;
            }
            this.gameObject.SetActive(false);
            hp = health;
        }

        // debug
        if (player == null) 
        { Debug.Log("[ERROR] => Target object is null , Can't Rotate"); 
            return; } 

        // get position of player
        Vector3 playerPosition = player.transform.position; 
        // get rotation of player
        Quaternion playerRotation = Quaternion.LookRotation(playerPosition - transform.position); 

        // set rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
    }

    public void takeDamage()
    {
        hp--;
    }
    private void FixedUpdate()
    {
        // set movement
        if (inRange(vision))
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(pos);
            transform.LookAt(player.transform);
        }
    }

    private bool inRange(int sight)
    {
        Vector3 playPos = player.transform.position;
        if (Mathf.Abs(rb.position.x - playPos.x) < sight && Mathf.Abs(rb.position.z - playPos.z) < sight)
        {
            return true;
        }
        else return false;
    }
}
