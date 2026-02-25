using UnityEngine;
using System;
using NUnit.Framework;
using UnityEngine.EventSystems;
using Unity.Mathematics;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class GPSTracker : MonoBehaviour
{

    double targetLat = 37.19222625752842;
    double targetLon = -3.6165878879561926;

    private bool spawned = false;
    private double distanceMin = 15.0;
    [SerializeField] public GameObject enemyPrefab;
    GameObject SpawnedObject;
    [SerializeField] public ARRaycastManager raycastManager;


    void Update()
    {
        if (raycastManager == null)
        {
            UIManager.instance.MostarMensaje("ARRaycastManager no asignado", 3);
            return;
        }
        if (Input.location.status == LocationServiceStatus.Running)
        {
            double distance = CalcularDistancia(GPSManager.Instance.Latitude, GPSManager.Instance.Longitude, targetLat, targetLon);
            if (distance < distanceMin)
            {
                UIManager.instance.MostarMensaje("¡Has encontrado el Pokémon! :" + distance + " A", 2);

                spawnEnemyRaycast();
            }
            else
            {
                UIManager.instance.MostarMensaje("Distancia al Pokémon: " + distance.ToString("F2") + " metros", 4);
            }
        }

    }

    public double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371000;
        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);
        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }
    public double ToRadians(double degrees) => degrees * Math.PI / 180;

    void spawnEnemy()
    {
        UIManager.instance.MostarMensaje("¡El Pokémon ha aparecido!", 3);
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0f;

        spawnPosition.y = 1.5f;

        SpawnedObject = Instantiate( enemyPrefab, spawnPosition, Quaternion.identity);
        spawned = true;
    }

    void spawnEnemyRaycast()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        UIManager.instance.MostarMensaje("raycast antes del if ", 3);
        Vector2 centro = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(
            centro,
            hits,
            TrackableType.Planes))
        {
            UIManager.instance.MostarMensaje("¡El Pokémon ha aparecido!", 3);
            if (!spawned)
            {
                Instantiate(enemyPrefab, hits[0].pose.position, Quaternion.identity); //Quaternion.identity
            }
            
            spawned = true;
            UIManager.instance.MostarMensaje("spawn hecho" + spawned, 4);
        }
        else
        {
            UIManager.instance.MostarMensaje("No se ha detectado una superficie para colocar el Pokémon.", 3);
        }
            
    }
}