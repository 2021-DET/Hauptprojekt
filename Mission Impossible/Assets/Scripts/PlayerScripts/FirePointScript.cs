using UnityEngine;

public class FirePointScript : MonoBehaviour
{
    public CrosshairTarget target;
    public Transform crosshair;

    void Update()
    {
        if (target.collided == true)
        {
            this.transform.LookAt(crosshair);
        } else
        {
            transform.localRotation = Quaternion.identity;
        }
    }
}
