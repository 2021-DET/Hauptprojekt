using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{

    int heightScale = 6;
    float detailScale = 7.5f;

    List<GameObject> myCoins = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for(int v = 0; v < vertices.Length; v++)
        {
            vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x)/detailScale,
                                              (vertices[v].z+this.transform.position.z)/detailScale)*heightScale;
        
            if (vertices[v].y > 2 && Random.Range(1,100) < 2)
            {
                GameObject newCoin = PoolScript.getItem();
                if (newCoin != null)
                {
                    Vector3 coinPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                  vertices[v].y,
                                                  vertices[v].z + this.transform.position.z);
                    newCoin.transform.position = coinPos;
                    newCoin.SetActive(true);
                    myCoins.Add(newCoin);
                }
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }

    private void OnDestroy()
    {
        for(int i = 0; i < myCoins.Count; i++)
        {
            if (myCoins[i] != null)
                myCoins[i].SetActive(false);
        }
        myCoins.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}