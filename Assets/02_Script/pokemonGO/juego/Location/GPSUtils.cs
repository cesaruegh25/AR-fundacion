using UnityEngine;

public static class GPSUtils
{
    const float EarthRadius = 6378137f;

    public static Vector2 GPSToMeters(
        float lat1, float lon1,
        float lat2, float lon2)
    {
        float dLat = Mathf.Deg2Rad * (lat2 - lat1);
        float dLon = Mathf.Deg2Rad * (lon2 - lon1);

        float x = dLon * EarthRadius * Mathf.Cos(Mathf.Deg2Rad * lat1);
        float z = dLat * EarthRadius;

        return new Vector2(x, z);
    }

    public static Vector2 GenerateRandomGPS(
        float lat,
        float lon,
        float minDistance,
        float maxDistance)
    {
        float distance = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float dx = Mathf.Cos(angle) * distance;
        float dz = Mathf.Sin(angle) * distance;

        float newLat = lat + (dz / EarthRadius) * Mathf.Rad2Deg;
        float newLon = lon + (dx / (EarthRadius * Mathf.Cos(Mathf.Deg2Rad * lat))) * Mathf.Rad2Deg;

        return new Vector2(newLat, newLon);
    }
}