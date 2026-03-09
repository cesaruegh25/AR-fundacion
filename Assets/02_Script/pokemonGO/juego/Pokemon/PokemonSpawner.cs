using UnityEngine;
using System.Collections;

public class PokemonSpawner : MonoBehaviour
{
    public GameObject[] pokemonPrefabs;
    public int pokemonToSpawn = 3;

    public float minDistance = 5f;
    public float maxDistance = 15f;

    //public Transform playerARCamera;

    public double Latitude;
    public double Longitude;

    void Start()
    {
        SpawnPokemons();
        //StartCoroutine(SpawnWhenLocationReady());

    }
    private void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            UIManagerPokemon.Instance.Log("coordenadas actuales: LAT: " + Latitude + " LON: " + Longitude, 1);
        }
    }

    IEnumerator SpawnWhenLocationReady()
    {
        while (!LocationManager.Instance.IsLocationReady)
        {
            UIManagerPokemon.Instance.Log("Esperando GPS...", 1);
            yield return null;
        }

        SpawnPokemons();
    }

    void SpawnPokemons()
    {
        for (int i = 0; i < pokemonToSpawn; i++)
        {
            SpawnPokemon();
        }
    }

    void SpawnPokemon()
    {
        float distance = Random.Range(minDistance, maxDistance);

        Vector2 randomDir = Random.insideUnitCircle.normalized;

        Vector3 spawnOffset = new Vector3(
            randomDir.x * distance,
            0,
            randomDir.y * distance
        );

        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;

        spawnPosition.y = 1.5f;
        //Vector3 spawnPosition = playerARCamera.position + spawnOffset;

        GameObject randomPokemon = pokemonPrefabs[
            Random.Range(0, pokemonPrefabs.Length)
        ];

        GameObject pokemon = Instantiate(randomPokemon, spawnPosition, Quaternion.identity);

        CompassManager compass = FindObjectOfType<CompassManager>();
        compass.pokemons.Add(pokemon.transform);

        UIManagerPokemon.Instance.Log("Pokemon generado a " + distance + "m", 2);
    }
}