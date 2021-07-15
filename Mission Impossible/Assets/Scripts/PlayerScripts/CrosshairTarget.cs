using UnityEngine;

public class CrosshairTarget : MonoBehaviour
{
    Camera mainCam;
    Ray ray;
    RaycastHit hitInfo;
    public bool collided = false;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        ray.origin = mainCam.transform.position;
        ray.direction = mainCam.transform.forward;
        if (Physics.Raycast(ray, out hitInfo))
        {
            collided = true;
            transform.position = hitInfo.point;
        }
        else
        {
            collided = false;
            transform.position = ray.origin;
        }
    }
}
