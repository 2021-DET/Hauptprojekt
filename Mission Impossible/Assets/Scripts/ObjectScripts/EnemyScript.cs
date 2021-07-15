using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //intern
    private int hp;
    private Rigidbody rb;
    private GameObject player;

    //stats
    public float speed = 4f;
    float rotationSpeed = 1f;
    public int points;
    public int health;
    public int vision = 15;

    //instantiate
    public AudioClip expl;
    public GameObject explosionPrototype;

    void Start()
    {
        hp = health;
        rb = this.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (hp <= 0)
        {
            GameObject explosion = Instantiate (explosionPrototype , transform.position , transform.rotation);
            explosion.GetComponent<AudioSource>().PlayOneShot(expl);
            player.GetComponent<PlayerScript>().score += points;
            this.gameObject.SetActive(false);
            hp = health;
        }

        Vector3 playerPosition = player.transform.position; 
        Quaternion playerRotation = Quaternion.LookRotation(playerPosition - transform.position); 
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
    }

    public void takeDamage()
    {
        hp--;
    }
    private void FixedUpdate()
    {
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
