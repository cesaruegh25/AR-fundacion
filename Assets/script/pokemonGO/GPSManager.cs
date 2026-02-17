using UnityEngine;

public class GPSManager : MonoBehaviour
{

    public static GPSManager Instance;
    public double Latitude;
    public double Longitude;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        if (!Input.location.isEnabledByUser) return;
        UIManager.instance.MostarMensaje("Ejecuta Start", 1);
        Input.location.Start();
        Input.compass.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            UIManager.instance.MostarMensaje("coordenadas actuales: LAT: " + Latitude + " LON: " + Longitude, 1);
        }
    }
}
