using UnityEngine;
using System.Collections.Generic;

public class spawnEnemy : MonoBehaviour
{
    public GameObject trackPrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefabs;

    public float placeLegth;
    public float speed = 2.0f;
    public float obstacleChance = 0.5f;
    public float objetoChance = 1.0f;

    private Queue<GameObject> trackQueue = new Queue<GameObject>();
    private float spawnZ = 0.0f;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnPiece();
        }
    }

    void Update()
    {
        foreach (GameObject track in trackQueue)
        {
            if (track != null)
            {
                track.transform.Translate(Vector3.back * speed * Time.deltaTime);
            }
        }

        spawnZ -= speed * Time.deltaTime;

        if (trackQueue.Count > 0 && trackQueue.Peek().transform.position.z < -placeLegth)
        {
            UIManager.instance.MostarMensaje("terreno eliminado en :" + trackQueue.Peek().transform.position.z, 2);

            RemovePiece();
            SpawnPiece();
        }
    }

    void SpawnPiece()
    {
        GameObject newPiece = Instantiate(trackPrefab);
        newPiece.transform.SetParent(transform);
        newPiece.transform.position = new Vector3(0, 0, spawnZ);

        if (Random.value < obstacleChance)
        {
            SpawnObject(newPiece.transform, obstaclePrefab);
        }
        if (Random.value < objetoChance)
        {
            SpawnObject(newPiece.transform, coinPrefabs);
        }

        trackQueue.Enqueue(newPiece);
        spawnZ += placeLegth;
    }

    void SpawnObject(Transform parent, GameObject prefabToSpawn)
    {
        if (prefabToSpawn == null) return;

        float[] lanes = { -1.5f, -0.75f, 0f, 0.75f, 1.5f };
        float x = lanes[Random.Range(0, lanes.Length)];

        GameObject objeto = Instantiate(prefabToSpawn);
        objeto.transform.SetParent(parent);

        objeto.transform.localPosition = new Vector3(x, 0.75f, 0);
    }

    void RemovePiece()
    {
        GameObject oldPiece = trackQueue.Dequeue();
        Destroy(oldPiece);
    }
}