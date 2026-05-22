using UnityEngine;

public class Pokeball : MonoBehaviour
{
    private bool capturado = false;

    void OnCollisionEnter(Collision collision)
    {
        UIManagerPokemon.Instance.Log(
            "Golpeó: " + collision.gameObject.name,
            2
        );

        if (capturado) return;

        if (collision.gameObject.CompareTag("Pokemon"))
        {
            capturado = true;

            UIManagerPokemon.Instance.Log(
                "Detectado Pokemon",
                2
            );

            CapturarPokemon(collision.gameObject);
        }
    }

    void CapturarPokemon(GameObject pokemon)
    {
        UIManagerPokemon.Instance.Log("¡Pokémon capturado con éxito!", 2);

        // Eliminar al Pokémon de la lista del CompassManager para que no de error
        CompassManager compass = FindObjectOfType<CompassManager>();
        if (compass != null)
        {
            compass.pokemons.Remove(pokemon.transform);
        }

        // Aquí puedes reproducir partículas, animaciones, etc.
        Destroy(pokemon);  // Hacemos desaparecer al Pokémon
        Destroy(gameObject); // Destruimos la Pokéball lanzada
    }
}
