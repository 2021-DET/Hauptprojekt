using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{

    int heightScale = 6;
    float detailScale = 7.5f;

    List<GameObject> items = new List<GameObject>();

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
                    items.Add(newCoin);
                }
            }
            if (vertices[v].y > 2 && Random.Range(1,100) < 2)
            {
                GameObject newTree = PoolTree.getTree();
                if (newTree != null)
                {
                    Vector3 TreePos = new Vector3(vertices[v].x + this.transform.position.x,
                                                  vertices[v].y,
                                                  vertices[v].z + this.transform.position.z);
                    newTree.transform.position = TreePos;
                    newTree.SetActive(true);
                    items.Add(newTree);
                }
            }
            if (vertices[v].y > 2 && Random.Range(1,100) < 2)
            {
                GameObject newRock = PoolRock.getRock();
                if (newRock != null)
                {
                    Vector3 RockPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                  vertices[v].y,
                                                  vertices[v].z + this.transform.position.z);
                    newRock.transform.position = RockPos;
                    newRock.SetActive(true);
                    items.Add(newRock);
                }
            }
            if (vertices[v].y > 2 && Random.Range(1,100) < 2)
            {
                GameObject newTreeStump = PoolTreeStump.getTreeStump();
                if (newTreeStump != null)
                {
                    Vector3 TreeStumpPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                  vertices[v].y,
                                                  vertices[v].z + this.transform.position.z);
                    newTreeStump.transform.position = TreeStumpPos;
                    newTreeStump.SetActive(true);
                    items.Add(newTreeStump);
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
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                items[i].SetActive(false);
        }
        items.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}