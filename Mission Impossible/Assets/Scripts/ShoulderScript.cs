using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderScript : MonoBehaviour
{
    public float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*float height = Input.GetAxis("Mouse Y");
        transform.Rotate(0, height * speed * Time.deltaTime, 0);
        */
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        Vector3 rotVector = new Vector3(mouseY, mouseX, 0f) * speed;
        //camTransform.Rotate(rotVector);
        transform.Rotate(new Vector3(-Mathf.Clamp(rotVector.x, -35, 60), 0f, 0f));
        
    }
}
