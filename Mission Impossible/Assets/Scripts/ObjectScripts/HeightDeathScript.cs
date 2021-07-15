using UnityEngine;
public class HeightDeathScript : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.position.y <= -2f)
        {
            gameObject.SetActive(false);
        }
    }
}
