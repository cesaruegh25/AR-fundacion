using UnityEngine;
using System.Collections.Generic;
using UnityEditor.U2D;

public class spawnEnemy : MonoBehaviour
{
    public GameObject trackPrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefabs;

    public float placeLegth;
    public float speed = 2.0f;
    public float obstacleChance = 0.5f;
    public float objetoChance = 1.0f;
    public float tiempoPorFase = 30.0f;

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
            RemovePiece();
            SpawnPiece();
        }
        if (speed < 5.0f)
        {
            speed += Time.deltaTime * 0.02f;
        }
    }

    void SpawnPiece()
    {
        GameObject newPiece = Instantiate(trackPrefab);
        newPiece.transform.SetParent(transform);
        newPiece.transform.position = new Vector3(0, 0, spawnZ);

        float tiempoActual = 0f;
        if (UIManager.instance != null)
        {
            tiempoActual = UIManager.instance.tiempoTranscurrido;
        }
        int intentosObstaculos = 1 + Mathf.FloorToInt(tiempoActual / tiempoPorFase);
        int intentosMonedas = 1 + Mathf.FloorToInt(tiempoActual / (tiempoPorFase + 10f));
        intentosObstaculos = Mathf.Clamp(intentosObstaculos, 1, 3);
        intentosMonedas = Mathf.Clamp(intentosMonedas, 1, 4);

        float dificultadObstaculo = Mathf.Clamp(obstacleChance + (tiempoActual * 0.004f), obstacleChance, 0.8f);
        float margenZ = placeLegth / 3f;

        for (int i = 0; i < intentosObstaculos; i++)
        {
            if (Random.value < dificultadObstaculo)
            {
                float zAleatorio = Random.Range(-margenZ, margenZ);
                SpawnObject(newPiece.transform, obstaclePrefab, zAleatorio);
            }
        }
        for (int i = 0; i < intentosMonedas; i++)
        {
            if (Random.value < objetoChance)
            {
                float zAleatorio = Random.Range(-margenZ, margenZ);
                SpawnObject(newPiece.transform, coinPrefabs, zAleatorio);
            }
        }

        trackQueue.Enqueue(newPiece);
        spawnZ += placeLegth;
    }

    void SpawnObject(Transform parent, GameObject prefabToSpawn, float offsetZ)
    {
        if (prefabToSpawn == null) return;

        float[] lanes = { -1.5f, -0.75f, 0f, 0.75f, 1.5f };
        float x = lanes[Random.Range(0, lanes.Length)];

        GameObject objeto = Instantiate(prefabToSpawn);
        objeto.transform.SetParent(parent);

        objeto.transform.localPosition = new Vector3(x, 0.75f, offsetZ);
    }

    void RemovePiece()
    {
        GameObject oldPiece = trackQueue.Dequeue();
        Destroy(oldPiece);
    }
}