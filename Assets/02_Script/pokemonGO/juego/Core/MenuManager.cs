using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName = "Game";
    public float gpsTimeout = 10f;

    public void StartGame()
    {
        StartCoroutine(InitializeGame());
    }

    IEnumerator InitializeGame()
    {
        // comprobar si el usuario tiene activado el GPS
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPS desactivado por el usuario");
            yield break;
        }

        // iniciar GPS
        Input.location.Start();

        int waitTime = (int)gpsTimeout;

        while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
        }

        // si falla
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("No se pudo obtener la ubicación");
            yield break;
        }

        // si funciona
        if (Input.location.status == LocationServiceStatus.Running)
        {
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;

            Debug.Log("Ubicación obtenida: " + latitude + " , " + longitude);
        }

        // cargar escena del juego
        SceneLoader.Instance.LoadScene(gameSceneName);
    }
}