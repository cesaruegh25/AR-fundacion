using UnityEngine;

public class PokeballThrower : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject pokeballPrefab;

    [Header("Lanzamiento")]
    public float spawnDistance = 0.5f;
    public float throwForce = 8f;
    public float upForce = 1.5f;

    public void LanzarPokeball()
    {
        if (Camera.main == null)
        {
            UIManagerPokemon.Instance.Log(
                "No existe Camera.main",
                1
            );
            return;
        }

        Transform cam = Camera.main.transform;

        // Sale desde delante de la cámara
        Vector3 spawnPos =
            cam.position +
            cam.forward * spawnDistance;

        GameObject ball =
            Instantiate(
                pokeballPrefab,
                spawnPos,
                Quaternion.identity
            );

        Rigidbody rb =
            ball.GetComponent<Rigidbody>();

        if (rb == null)
        {
            UIManagerPokemon.Instance.Log(
                "La Pokeball no tiene Rigidbody",
                1
            );
            return;
        }

        Vector3 fuerza =
            (cam.forward * throwForce) +
            (Vector3.up * upForce);

        rb.AddForce(
            fuerza,
            ForceMode.Impulse
        );

        UIManagerPokemon.Instance.Log(
            "Pokeball lanzada",
            1
        );
    }
}