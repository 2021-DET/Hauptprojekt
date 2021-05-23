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
    public int nextMenu = 2;

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
}
