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

    private void OnTriggerEnter(Collider col)
    {
        // if player hits an ammunition pack
        if (col.gameObject.tag == "Player")
        {
            PlayerScript ps = col.transform.GetComponent<PlayerScript>();
            ps.addAmmo(10);
            Destroy(gameObject);
        }
    }
}
