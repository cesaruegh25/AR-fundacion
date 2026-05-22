using UnityEngine;
using System.Collections.Generic;

public class CompassManager : MonoBehaviour
{
    public Transform arrowUI;
    //public Transform playerCamera;

    public List<Transform> pokemons = new List<Transform>();

    void Start()
    {
        Input.compass.enabled = true;
    }

    // Modifica el Update de tu CompassManager:
    void Update()
    {
        Transform closest = GetClosestPokemon();
        if (closest == null) return;

        // Mantener la flecha apuntando usando la rotación de Unity (esto sigue funcionando en distancias cortas)
        Vector3 direction = closest.position - Camera.main.transform.position;
        direction.y = 0;
        float angle = Vector3.SignedAngle(Camera.main.transform.forward, direction, Vector3.up);
        arrowUI.localRotation = Quaternion.Euler(0, 0, -angle);

        // NUEVO: Obtener la distancia real por GPS
        double playerLat = (Input.location.status == LocationServiceStatus.Running) ? Input.location.lastData.latitude : 40.416775;
        double playerLon = (Input.location.status == LocationServiceStatus.Running) ? Input.location.lastData.longitude : -3.703790;

        PokemonLocation pokLoc = closest.GetComponent<PokemonLocation>();

        if (pokLoc != null)
        {
            double realDist = PokemonSpawner.DistanceInMetres(playerLat, playerLon, pokLoc.Latitude, pokLoc.Longitude);
        }
    }


    Transform GetClosestPokemon()
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (Transform p in pokemons)
        {
            float dist = Vector3.Distance(Camera.main.transform.position, p.position);

            if (dist < minDist)
            {
                minDist = dist;
                closest = p;
            }
        }

        return closest;
    }
}