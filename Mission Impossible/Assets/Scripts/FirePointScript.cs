using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointScript : MonoBehaviour
{
    public CrosshairTarget target;
    public Transform crosshair;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null) {
            Debug.Log("Crosshair Skript funktioniert nicht");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target.collided == true)
        {
            this.transform.LookAt(crosshair);
        } else
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
    }
}
