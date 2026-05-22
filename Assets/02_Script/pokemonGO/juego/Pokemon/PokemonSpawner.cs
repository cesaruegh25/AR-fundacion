using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokemonSpawner : MonoBehaviour
{
    public GameObject[] pokemonPrefabs;
    public int pokemonToSpawn = 3;

    private float minDistance = 3f;
    private float maxDistance = 10f;
    private bool espera = true;
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
            if (espera)
            { 
                //UIManagerPokemon.Instance.Log("coordenadas actuales: LAT: " + Latitude + " LON: " + Longitude, 1);
                espera = false;
                StartCoroutine(delay());
            }
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        espera = true;
    }

    void SpawnPokemons()
    {
        for (int i = 0; i < pokemonToSpawn; i++)
        {
            SpawnPokemon();
        }
    }

    // Modifica tu función SpawnPokemon dentro de PokemonSpawner:
    void SpawnPokemon()
    {
        // 1. Si el GPS no está listo, usamos coordenadas de prueba (Ej: Madrid)
        double playerLat = (Input.location.status == LocationServiceStatus.Running) ? Input.location.lastData.latitude : 40.416775;
        double playerLon = (Input.location.status == LocationServiceStatus.Running) ? Input.location.lastData.longitude : -3.703790;

        // 2. Calcular distancia y dirección aleatoria en metros
        float distance = Random.Range(minDistance, maxDistance);
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        // Conversión aproximada de metros a grados GPS (1 grado aprox. 111,000 metros)
        double latOffset = (randomDir.y * distance) / 111111d;
        double lonOffset = (randomDir.x * distance) / (111111d * System.Math.Cos(playerLat * Mathf.Deg2Rad));

        double pokemonLat = playerLat + latOffset;
        double pokemonLon = playerLon + lonOffset;

        // 3. Posición visual en AR (se genera cerca de la cámara del dispositivo)
        Vector3 visualOffset = new Vector3(randomDir.x * distance, 0, randomDir.y * distance);
        Vector3 spawnPosition = Camera.main.transform.position + visualOffset;
        spawnPosition.y = Camera.main.transform.position.y - 0.5f; // Un poco abajo del campo de visión

        GameObject randomPokemon = pokemonPrefabs[Random.Range(0, pokemonPrefabs.Length)];
        GameObject pokemon = Instantiate(randomPokemon, spawnPosition, Quaternion.identity);

        // 4. Guardar las coordenadas GPS reales en el Pokémon
        PokemonLocation loc = pokemon.AddComponent<PokemonLocation>();
        loc.Latitude = pokemonLat;
        loc.Longitude = pokemonLon;

        CompassManager compass = FindObjectOfType<CompassManager>();
        if (compass != null) compass.pokemons.Add(pokemon.transform);

        UIManagerPokemon.Instance.Log("Pokemon generado en GPS a " + distance + "m", 2);
    }


    public static double DistanceInMetres(double lat1, double lon1, double lat2, double lon2)
    {
        double rlat1 = Mathf.Deg2Rad * lat1;
        double rlat2 = Mathf.Deg2Rad * lat2;
        double rlon1 = Mathf.Deg2Rad * lon1;
        double rlon2 = Mathf.Deg2Rad * lon2;

        double dlat = rlat2 - rlat1;
        double dlon = rlon2 - rlon1;

        double a = Mathf.Sin((float)dlat / 2) * Mathf.Sin((float)dlat / 2) +
                   Mathf.Cos((float)rlat1) * Mathf.Cos((float)rlat2) *
                   Mathf.Sin((float)dlon / 2) * Mathf.Sin((float)dlon / 2);

        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));
        double radius = 6371000; // Radio de la Tierra en metros

        return radius * c;
    }
}