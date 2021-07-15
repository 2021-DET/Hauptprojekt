using UnityEngine;

public class ShoulderScript : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 rotation = transform.localEulerAngles;
        float rotationY = - mouseY * speed;
        if (rotation.x >= 310)
        {
            rotationY = Mathf.Clamp(rotationY + rotation.x, 310, 361);
        }
        else if (rotation.x < 310 && rotation.x > 180)
        {
            rotationY = 310;
        }
        else if (rotation.x <= 180 && rotation.x > 51)
        {
            rotationY = 50;
        }
        else
        {
            rotationY = Mathf.Clamp(rotationY + rotation.x, -50, 50);
        }
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    } 
}
