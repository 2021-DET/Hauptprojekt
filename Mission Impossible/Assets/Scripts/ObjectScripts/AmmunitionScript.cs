using UnityEngine;

public class AmmunitionScript : MonoBehaviour
{
    public int amount = 10;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.root.GetComponent<PlayerScript>().addAmmo(amount);
            gameObject.SetActive(false);
        }
    }
}
