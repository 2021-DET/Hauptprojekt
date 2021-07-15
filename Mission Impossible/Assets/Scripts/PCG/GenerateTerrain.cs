using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
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
        for(int v = 0; v < vlength; v++)
        {
            vertices[v].y = Mathf.PerlinNoise((vertices[v].x + this.transform.position.x) / detailScale,
                                              (vertices[v].z + this.transform.position.z) / detailScale) * heightScale;
            if (v % 3 == 0)
            {
                SpawnCollectible(v, 1.5f, 2, 4, PoolScript.getItem());
                SpawnCollectible(v, 2f, 3, 4, PoolAmmo.getAmmo());

                SpawnNPC(v, 1.5f, 2, 8, PoolMummies.getMummy());
                SpawnNPC(v, 2.5f, 2, 8, PoolUndead.getUndead());

                SpawnInPerlinNoise(v, 3.6f, (vertices[v].x + 5) / 10, (vertices[v].z + 10) / 10, 6, PoolTree.getTree());
                SpawnInPerlinNoise(v, 1.8f, (vertices[v].x + 5) / 10, (vertices[v].z + 5) / 10, 2, PoolRock.getRock());
                SpawnInPerlinNoise(v, 1.4f, (vertices[v].x + 2) / 10, (vertices[v].z + 3) / 10, 5, PoolTreeStump.getTreeStump());
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
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
                items[i].SetActive(false);
        }
        items.Clear();
    }

    private void SpawnCollectible(int v, float height, int chance, int range, GameObject objectToSpawn)
    {
        if (vertices[v].y > height && Random.Range(1, 80) < chance && outsidePlayerRange(vertices[v], range))
        {
            if (objectToSpawn != null)
            {
                Vector3 objPos = new Vector3(vertices[v].x + this.transform.position.x,
                                              vertices[v].y,
                                              vertices[v].z + this.transform.position.z);
                objectToSpawn.transform.position = objPos;
                objectToSpawn.SetActive(true);
                items.Add(objectToSpawn);
            }
        }
    }
    private void SpawnNPC(int v, float height, int chance, int range, GameObject objectToSpawn)
    {
        if (vertices[v].y > height && Random.Range(1, 80) < chance && outsidePlayerRange(vertices[v], range))
        {
            if (objectToSpawn != null)
            {
                Vector3 objPos = new Vector3(vertices[v].x + this.transform.position.x,
                                              vertices[v].y,
                                              vertices[v].z + this.transform.position.z);
                objectToSpawn.transform.position = objPos;
                objectToSpawn.SetActive(true);
                //nicht zu items hinzugefügt, weil die NPCs sich bewegen
            }
        }
    }
    private void SpawnInPerlinNoise(int v, float height, float x, float y, int chance, GameObject objectToSpawn)
    {
        if (vertices[v].y > height && Mathf.PerlinNoise(x, y) * 10 > chance)
        {
            if (objectToSpawn != null)
            {
                Vector3 objPos = new Vector3(vertices[v].x + this.transform.position.x,
                                              vertices[v].y - 0.1f,
                                              vertices[v].z + this.transform.position.z);
                objectToSpawn.transform.position = objPos;
                objectToSpawn.SetActive(true);
                items.Add(objectToSpawn);
            }
        }
    }
}