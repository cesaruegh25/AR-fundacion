using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Header("Velocidad de la Bala")]
    public float velocidadBase = 4.0f;

    [Tooltip("Cuánto aumenta la velocidad por cada segundo que pasa")]
    public float factorAceleracion = 0.08f;

    private float velocidadCalculada;

    void Start()
    {
        // 1. Miramos cuánto tiempo lleva la partida cuando nace esta bala en concreto
        float tiempoActual = 0f;
        if (UIManager.instance != null)
        {
            tiempoActual = UIManager.instance.tiempoTranscurrido;
        }

        // 2. Calculamos la velocidad fija que tendrá esta bala al nacer
        // Ejemplo: Si llevas 30 segundos: 4.0 + (30 * 0.08) = 6.4 de velocidad.
        velocidadCalculada = velocidadBase + (tiempoActual * factorAceleracion);

        // Ponemos un tope para que no sea imposible de esquivar
        velocidadCalculada = Mathf.Clamp(velocidadCalculada, velocidadBase, 18f);
    }

    void Update()
    {
        // 3. Movemos la bala hacia atrás (hacia el jugador) independientemente de la carretera
        // Usamos Space.World para que su movimiento no se altere de forma rara por el padre
        transform.Translate(Vector3.back * velocidadCalculada * Time.deltaTime, Space.World);
    }
}