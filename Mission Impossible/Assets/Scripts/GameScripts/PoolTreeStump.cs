using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTreeStump : MonoBehaviour
{
    public static int num = 30;
    public GameObject prefab;
    static GameObject[] items;

    void Start()
    {
        items = new GameObject[num];
        for(int i = 0; i < num; i++)
        {
            items[i] = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            items[i].SetActive(false);
        }
    }

    static public GameObject getTreeStump()
    {
        for(int i = 0; i < num; i++)
        {
            if(!items[i].activeSelf)
            {
                return items[i];
            }
        }
        return null;
    }
}
