using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for player shooting
 */ 
public class ShootingBullet : MonoBehaviour
{
    // reference to the fire point/starting point for the bullets
    public Transform firePoint; 
    // reference the bullet object
    public GameObject bulletPrefab;
    // value for bullet force
    public float bulletForce = 20f;
    // boolean to set shooting availability
    private bool canshoot = false;

    void Update()
    {
        // button clicked and player able to shoot
        if (Input.GetButtonDown("Fire1") &&(!canshoot) )
        {
            StartCoroutine (FireShot ());
        }
    }

    void Shoot()
    {
        // generate bullet object
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // reference tot he rigidbody of the bullet
        Rigidbody rd = bullet.GetComponent<Rigidbody>();
        // give bullet the force value
        rd.AddForce(firePoint.up * bulletForce, ForceMode.Impulse);
    }

    IEnumerator FireShot() 
    {
            // ienum for coroutine and delay
             canshoot = true;
             Shoot ();
             yield return new WaitForSeconds (0.5f); // to stop rapid fire
             canshoot = false;
 
    }
}
