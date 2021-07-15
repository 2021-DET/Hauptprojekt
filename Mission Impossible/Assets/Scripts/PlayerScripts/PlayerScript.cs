using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //counters
    public int score = 0;
    public int gold = 0;
    public int ammo = 0;
    public int maxAmmo = 30;

    //stats
    public float speed = 6f;
    public float rotSpeed = 1f;
    public float jumpPush = 10f;
    public float extraGravity = -20f;
    public float bulletForce = 20f;

    private Vector3 moveVector;
    private Vector3 rotVector;
    private Rigidbody rd;
    private Animator anim;
    private AudioSource audioSrc;

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;

    public GameObject gameoverScreen;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;

    //booleans
    private bool canshoot = false;
    public bool playerCanMove = true;
    private bool canJump = false;

    //sound effects
    public AudioClip shotSound;
    public AudioClip salveSound;
    public AudioClip coinSound;
    public AudioClip ammoSound;
    public AudioClip deathSound;

    void Start()
    { 
        rd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (playerCanMove)
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            rotVector = new Vector3(0f, mouseX, 0f) * rotSpeed;
            transform.Rotate(new Vector3(0f, rotVector.y, 0f));

            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                anim.SetInteger("Anim", 1);
                float xDir = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
                float yDir = Input.GetAxis("Vertical") * speed * Time.deltaTime;
                moveVector = new Vector3(xDir, 0, yDir);
                transform.Translate(moveVector);
            }
            else
            {
                anim.SetInteger("Anim", 0);
            }

            // jump call
            if (Input.GetButtonDown("Jump") && !(canJump))
            {
                StartCoroutine(Jumping());
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
                }
            }
        }
        // death on fall
        if (gameObject.transform.position.y <= -2f && playerCanMove)
        {
            onDeath();
        }
    }

    private void Jump()
    {
            Vector3 power = rd.velocity;
            power.y = jumpPush;
            rd.velocity = power;
            rd.AddForce(new Vector3(0f, extraGravity, 0f));
    }

    IEnumerator Jumping()
    {
            canJump = true;
            Jump();
            yield return new WaitForSeconds(1.5f);
            canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            onDeath();
        }
    }

    public void onDeath()
    {
        audioSrc.PlayOneShot(deathSound);
        Cursor.lockState = CursorLockMode.None;
        TimerController.instance.EndTimer();
        Time.timeScale = 0;
        gameoverScreen.SetActive(true);
        playerCanMove = false;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rd = bullet.GetComponent<Rigidbody>();
        rd.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        audioSrc.PlayOneShot(shotSound);
    }

    void SalveShoot()
    {
        GameObject bullet1 = Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab2, firePoint2.position, firePoint2.rotation);
        GameObject bullet3 = Instantiate(bulletPrefab2, firePoint3.position, firePoint3.rotation);
        Rigidbody rd1 = bullet1.GetComponent<Rigidbody>();
        Rigidbody rd2 = bullet2.GetComponent<Rigidbody>();
        Rigidbody rd3 = bullet3.GetComponent<Rigidbody>();
        rd1.velocity = rd1.transform.forward * bulletForce;
        rd2.velocity = rd2.transform.forward * bulletForce;
        rd3.velocity = rd3.transform.forward * bulletForce;
        audioSrc.PlayOneShot(salveSound);
    }

    IEnumerator FireShot()
    {
        canshoot = true;
        anim.SetTrigger("gunShot");
        Shoot();
        yield return new WaitForSeconds(0.4f);
        canshoot = false;
    }

    IEnumerator BurstShot()
    {
        canshoot = true;
        anim.SetTrigger("gunShot");
        SalveShoot();
        ammo -= 3;
        yield return new WaitForSeconds(0.6f);
        canshoot = false;
    }

    public void collectCoin(int value)
    {
        gold += value;
        audioSrc.PlayOneShot(coinSound);
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
        audioSrc.PlayOneShot(ammoSound);
    }

    private bool enoughAmmo()
    {
        return ammo >= 3;
    }
}
