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

    void Update()
    {
        Transform closest = GetClosestPokemon();

        if (closest == null) return;

        Vector3 direction = closest.position - Camera.main.transform.position;
        direction.y = 0;

        float angle = Vector3.SignedAngle(
            Camera.main.transform.forward,
            direction,
            Vector3.up
        );

        arrowUI.localRotation = Quaternion.Euler(0, 0, -angle);

        float dist = Vector3.Distance(Camera.main.transform.position, closest.position);
        UIManagerPokemon.Instance.Log("Pokemon mas cercano a : "+dist+"m", 1);
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