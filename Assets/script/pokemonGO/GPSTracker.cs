using UnityEngine;
using System;

public class GPSTracker : MonoBehaviour
{

    double targetLat = 37.19222625752842;
    double targetLon = -3.6165878879561926;

    private bool spawned = false;
    private double distanceMin = 20.0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running && spawned == false)
        {
            double currentLatitude = Input.location.lastData.latitude;
            double currentLongitude = Input.location.lastData.longitude;

            double distance = CalcularDistancia(currentLatitude, currentLongitude, targetLat, targetLon);
            if (distance < distanceMin)
            {
                spawned = true;
                UIManager.instance.MostarMensaje_1("¡Has encontrado el Pokémon!");
            }
             else
            {
                UIManager.instance.MostarMensaje_1("Distancia al Pokémon: " + distance.ToString("F2") + " metros");
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
    return R* c;
}
public double ToRadians(double degrees) => degrees * Math.PI / 180;

}