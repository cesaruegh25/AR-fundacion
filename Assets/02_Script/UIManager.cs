using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] public TMP_Text text_ARI;
    [SerializeField] public TMP_Text text_ARD;
    [SerializeField] public TMP_Text text_ABI;
    [SerializeField] public TMP_Text text_ABD;

    [Header("Configuración del Cronómetro")]
    public TMP_Text text_Timer;

    public float tiempoTranscurrido = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 3. Usamos Update para actualizar el tiempo en cada fotograma
    private void Update()
    {
        // Time.deltaTime es la fracción de segundo que pasó desde el frame anterior.
        // Al sumarlo constantemente, obtenemos un contador de segundos perfecto.
        tiempoTranscurrido += Time.deltaTime;

        // Convertimos el tiempo total a minutos y segundos matemáticamente
        int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60f);
        int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60f);

        // 4. Formateamos el texto para que siempre muestre dos dígitos (ej: 02:05 en vez de 2:5)
        if (text_Timer != null)
        {
            text_Timer.text = string.Format("{0:00}:{1:00}", minutos, segundos);

            // Si quieres que aparezca una etiqueta antes del tiempo, puedes usar esta línea en su lugar:
            // text_Timer.text = "Tiempo: " + string.Format("{0:00}:{1:00}", minutos, segundos);
        }
    }

    public void MostarMensaje(string msj, int posicion)
    {
        if (posicion == 1)
        {
            text_ARI.text = msj;
        }
        else if (posicion == 2)
        {
            text_ARD.text = msj;
        }
        else if (posicion == 3)
        {
            text_ABI.text = msj;
        }
        else if (posicion == 4)
        {
            text_ABD.text = msj;
        }
    }
}
