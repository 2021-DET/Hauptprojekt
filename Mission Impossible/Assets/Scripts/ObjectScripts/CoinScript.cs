using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int value = 5;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.root.GetComponent<PlayerScript>().collectCoin(value);
            gameObject.SetActive(false);
        }
    }
}
