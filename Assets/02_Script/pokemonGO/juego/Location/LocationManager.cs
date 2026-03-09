using UnityEngine;
using System.Collections;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    public float Latitude { get; private set; }
    public float Longitude { get; private set; }

    public bool IsLocationReady { get; private set; }

    public float updateInterval = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLocation()
    {
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            UIManagerPokemon.Instance.Log("GPS desactivado", 1);
            yield break;
        }

        UIManagerPokemon.Instance.Log("Iniciando GPS...", 1);

        Input.location.Start();

        int maxWait = 10;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            UIManagerPokemon.Instance.Log("Inicializando GPS...", 1);
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            UIManagerPokemon.Instance.Log("Error obteniendo GPS", 2);
            yield break;
        }

        UIManagerPokemon.Instance.Log("GPS listo", 1);

        IsLocationReady = true;

        StartCoroutine(UpdateLocation());
    }

    IEnumerator UpdateLocation()
    {
        while (true)
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;

            UIManagerPokemon.Instance.Log("Lat: " + Latitude + " Lon: " + Longitude, 2);

            yield return new WaitForSeconds(updateInterval);
        }
    }
}