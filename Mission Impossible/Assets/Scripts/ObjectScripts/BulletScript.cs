using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyScript>().takeDamage();
        }
        Destroy(this.gameObject, 0.1f);
    }
}
