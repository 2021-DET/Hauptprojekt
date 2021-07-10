using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderScript : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 rotation = transform.localEulerAngles;
        float rotationY = - mouseY * speed;
        if (rotation.x >= 300)
        {
            rotationY += rotation.x;
        }
        else if (rotation.x < 300 && rotation.x > 180)
        {
            rotationY = 300;
        }
        else if (rotation.x <= 180 && rotation.x > 61)
        {
            rotationY = 60;
        }
        else
        {
            rotationY += rotation.x;
        }
        transform.localEulerAngles = new Vector3(rotationY, 0, 0);
    } 
}
