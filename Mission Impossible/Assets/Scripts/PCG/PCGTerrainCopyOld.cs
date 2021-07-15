using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGTerrainCopyOld : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Transform player;
    public int heightScale = 6;
    public float detailScale = 7.5f;
    List<GameObject> items = new List<GameObject>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mesh = this.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        int vlength = vertices.Length;
        for (int v = 0; v < vlength; v++)
        {
            vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale,
                                              (vertices[v].z + this.transform.position.z) / detailScale) * heightScale;
            if (v % 3 == 0)
            {
                if (vertices[v].y > 1.5 && Random.Range(1, 80) < 2 && outsidePlayerRange(vertices[v], 4))
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
                
                if (vertices[v].y > 2 && Random.Range(1, 80) < 3 && outsidePlayerRange(vertices[v], 4))
                {
                    GameObject newAmmo = PoolAmmo.getAmmo();
                    if (newAmmo != null)
                    {
                        Vector3 ammoPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                      vertices[v].y,
                                                      vertices[v].z + this.transform.position.z);
                        newAmmo.transform.position = ammoPos;
                        newAmmo.SetActive(true);
                        items.Add(newAmmo);
                    }
                }

                if (vertices[v].y > 1.5f && Random.Range(1, 80) < 2 && outsidePlayerRange(vertices[v], 8))
                {
                    GameObject newMummy = PoolMummies.getMummy();
                    if (newMummy != null)
                    {
                        Vector3 mummyPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                      vertices[v].y,
                                                      vertices[v].z + this.transform.position.z);
                        newMummy.transform.position = mummyPos;
                        newMummy.SetActive(true);
                    }
                }

                if (vertices[v].y > 2.5f && Random.Range(1, 80) < 2 && outsidePlayerRange(vertices[v], 8))
                {
                    GameObject newUndead = PoolUndead.getUndead();
                    if (newUndead != null)
                    {
                        Vector3 undeadPos = new Vector3(vertices[v].x + this.transform.position.x,
                                                      vertices[v].y,
                                                      vertices[v].z + this.transform.position.z);
                        newUndead.transform.position = undeadPos;
                        newUndead.SetActive(true);
                    }
                }

                if (vertices[v].y > 3.6 && Mathf.PerlinNoise((vertices[v].x + 5) / 10, (vertices[v].z + 10) / 10) * 10 > 6)
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
                if (vertices[v].y < 1.8 && Mathf.PerlinNoise((vertices[v].x + 5) / 10, (vertices[v].z + 5) / 10) * 10 > 2)
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
                if (vertices[v].y > 1.4 && Mathf.PerlinNoise((vertices[v].x + 2) / 10, (vertices[v].z + 3) / 10) * 10 > 5.4)
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
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }

    private bool outsidePlayerRange(Vector3 curPos, int sight)
    {
        if (curPos.x > player.position.x + sight || curPos.x < player.position.x - sight)
        {
            if (curPos.z > player.position.z + sight || curPos.z < player.position.z - sight)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                items[i].SetActive(false);
        }
        items.Clear();
    }
}