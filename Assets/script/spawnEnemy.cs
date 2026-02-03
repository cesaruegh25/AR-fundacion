using UnityEngine;
using System.Collections.Generic;

public class spawnEnemy : MonoBehaviour
{
    public GameObject trackPrefab;
    public GameObject obstaclePrefab;

    public int initialPlace = 5;
    public float placeLegth = 4.0f;
    public float speed = 2.0f;
    public float obstacleChance = 0.5f;

    private Queue<GameObject> trackQeue = new Queue<GameObject>();
    private float spawnZ = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < initialPlace; i++)
        {
            SpawnPiece();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject piece in trackQeue)
        {
            piece.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (trackQeue.Peek().transform.position.z < -placeLegth)
        {
            RemovePiece();
            SpawnPiece();
        }
    }

    void SpawnPiece()
    {
        GameObject piece = Instantiate(trackPrefab);
        piece.transform.SetParent(transform);
        piece.transform.position = new Vector3(0, 0, spawnZ);
        
        if (Random.value < obstacleChance)
        {
            SpawnObstacle(piece.transform);
        }
        trackQeue.Enqueue(piece);
        spawnZ += placeLegth;
    }

    void SpawnObstacle(Transform parent)
    {
        float[] lanes = { -1.5f, -0.75f, 0f, 0.75f, 1.5f };
        float x = lanes[Random.Range(0, lanes.Length)];

        GameObject obstacle = Instantiate(obstaclePrefab);
        obstacle.transform.SetParent(parent);
        obstacle.transform.localPosition = new Vector3(x, 1.0f, 0);
    }

    void RemovePiece()
    {
        GameObject oldPiece = trackQeue.Dequeue();
        Destroy(oldPiece);
    }
}
