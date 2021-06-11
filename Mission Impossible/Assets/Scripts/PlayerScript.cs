using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Main script for player object
 */
public class PlayerScript : MonoBehaviour
{
    public int score = 0;
    public int gold = 0;
    // speed value
    public float speed = 6f;
    // rotation value
    public float rotSpeed = 1f;
    // jump value
    public float jumpPush = 10f;
    // fall value
    public float extraGravity = -15f;
    // vector for movement
    private Vector3 moveVector;
    // vector for rotation
    public Transform camTransform;
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
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;
    //public CharacterController controller;

    // attributes for ammunition
    public int ammo = 0;
    public int maxAmmo = 30;

    void Start()
    {
        //controller = GetComponent<CharacterController>();
        // initiate
        rd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        rotVector = new Vector3(mouseY, mouseX, 0f) * rotSpeed;
        //camTransform.Rotate(rotVector);
        transform.Rotate(new Vector3(0f, rotVector.y, 0f));
       
        // animator code
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            anim.SetInteger("Anim" , 1);
            float xDir = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            float yDir = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            moveVector = new Vector3(xDir, 0, yDir);
            transform.Translate(moveVector);

            /**
            moveVector = new Vector3(xDir, 0, yDir).normalized;
            float targetAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg + rotVector.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * moveVector;
            transform.Translate(moveDir.normalized * speed * Time.deltaTime);
            **/
        
            //TODO: Höhe und Tiefe von Kamera einschränken
        }
        else
        {
            anim.SetInteger("Anim" , 0);
        }

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
            if (enoughAmmo())
            {
                StartCoroutine(BurstShot());
            } else
            {
                Debug.Log("nicht genug Schuss");
            }
        }

        // death on fall
        if (gameObject.transform.position.y <= -5f)
        {
            Cursor.lockState = CursorLockMode.None;
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
            Cursor.lockState = CursorLockMode.None;
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
        anim.SetTrigger("gunShot");
        Shoot();
        yield return new WaitForSeconds(0.5f); // to stop rapid fire
        canshoot = false;
    }

    IEnumerator BurstShot()
    {
        // ienum for coroutine and delay
        canshoot = true;
        anim.SetTrigger("gunShot");
        SalveShoot();
        ammo -= 3;
        yield return new WaitForSeconds(0.8f); // to stop rapid fire
        canshoot = false;
    }

    public void addAmmo(int amount)
    {
        if ( this.ammo + amount <= maxAmmo)
        {
            this.ammo += amount;
        } else
        {
            this.ammo = maxAmmo;
        }
    }

    private bool enoughAmmo()
    {
        return ammo >= 3;
    }
}
