using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player hits an enemy
        if (collision.collider.tag == "Player")
        {
            PlayerScript ps = collision.transform.GetComponent<PlayerScript>();
            ps.addAmmo(10);
        }
    }
}
