using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Main script for player object
 */
public class Player : MonoBehaviour
{
    // speed value
    public float speed = 10f;
    // rotation value
    public float rotSpeed = 1f;
    // jump value
    public float jumpPush = 10f;
    // fall value
    public float extraGravity = -15f;
    // vector for movement
    private Vector3 moveVector;
    // vector for rotation
    private Vector3 rotVector;
    // rigidbody reference
    private Rigidbody rd;
    // animator reference
    private Animator anim;
    // boolean for jumping availability
    private bool canJump = false;
    // reference to try again menu
    public int nextMenu = 1;

    // reference to the fire point/starting point for the bullets
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;

    // reference the bullet object
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    // value for bullet force
    public float bulletForce = 20f;
    // boolean to set shooting availability
    private bool canshoot = false;
    // bullet offset
    private Vector3 vecDis = new Vector3(0.8f, 0f, 0f);

    void Start()
    {
        // initiate
        rd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
            // animator code
            if(Input.GetAxis("Vertical") != 0)
            {
                anim.SetInteger("Anim" , 1);
            }
            else
            {
                anim.SetInteger("Anim" , 0);
            }
            // reference to input values
            float xDir = Input.GetAxis("Vertical") * speed * Time.deltaTime ;
            float yDir = Input.GetAxis("Horizontal") * rotSpeed  ;
            // set vector objects
            moveVector = new Vector3 ( 0 , 0 , xDir );
            rotVector   = new Vector3(0 , yDir , 0);
            // apply new vectors
            transform.Rotate(rotVector);
            transform.Translate(moveVector);
            // jump call
            if (Input.GetButtonDown("Jump") && !(canJump))
            {
                StartCoroutine( Jumping());
            }

        // button clicked and player able to shoot
        if (Input.GetButtonDown("Fire1") && (!canshoot))
        {
            StartCoroutine(FireShot());
        }

        if (Input.GetButtonDown("Fire2") && (!canshoot))
        {
            StartCoroutine(BurstShot());
        }

        // death on fall
        if (gameObject.transform.position.y <= -5f)
            {
                SceneManager.LoadScene(nextMenu);
            }
    }

    // jump method 
    private void Jump()
    {
            Vector3 power = rd.velocity;
            power.y = jumpPush;
            rd.velocity = power;
            //rd.AddForce(new Vector3(0f, extraGravity, 0f));
    }

    IEnumerator Jumping()
    {
            //ienum for coroutine and delay
            canJump = true;
            Jump();
            yield return new WaitForSeconds(1.5f); // player can't jump multiple times
            canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if player hits an enemy
        if (collision.collider.tag == "Enemy")
        {
            // load menu for a new game
            SceneManager.LoadScene(nextMenu);
        }
    }

    void Shoot()
    {
        // generate bullet object
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // reference to the rigidbody of the bullet
        Rigidbody rd = bullet.GetComponent<Rigidbody>();
        // give bullet the force value
        rd.AddForce(firePoint.up * bulletForce, ForceMode.Impulse);
    }

    void SalveShoot()
    {
        // generate bullet object
        GameObject bullet1 = Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab2, firePoint2.position, firePoint2.rotation);
        GameObject bullet3 = Instantiate(bulletPrefab2, firePoint3.position, firePoint3.rotation);
        //bullet2.transform.Rotate(0f, 80f, 0f);
        //bullet3.transform.Rotate(0f, -80f, 0f);
        // reference to the rigidbody of the bullet
        Rigidbody rd1 = bullet1.GetComponent<Rigidbody>();
        Rigidbody rd2 = bullet2.GetComponent<Rigidbody>();
        Rigidbody rd3 = bullet3.GetComponent<Rigidbody>();
        // give bullet the force value
        rd1.velocity = rd1.transform.up * bulletForce;
        rd2.velocity = rd2.transform.up * bulletForce;
        rd3.velocity = rd3.transform.up * bulletForce;
    }

    IEnumerator FireShot()
    {
        // ienum for coroutine and delay
        canshoot = true;
        Shoot();
        yield return new WaitForSeconds(0.5f); // to stop rapid fire
        canshoot = false;
    }

    IEnumerator BurstShot()
    {
        // ienum for coroutine and delay
        canshoot = true;
        SalveShoot();
        yield return new WaitForSeconds(0.8f); // to stop rapid fire
        canshoot = false;
    }
}
