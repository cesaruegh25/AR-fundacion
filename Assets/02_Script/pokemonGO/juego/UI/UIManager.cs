using TMPro;
using UnityEngine;

public class UIManagerPokemon : MonoBehaviour
{
    public static UIManagerPokemon Instance;

    public TextMeshProUGUI debugTextIzq;

    public TextMeshProUGUI debugTextDer;

    string logMessages = "";

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Log(string message, int numero)
    {
        Debug.Log(message);

        logMessages += message + "\n";

        if (logMessages.Length > 2000)
            logMessages = logMessages.Substring(logMessages.Length - 2000);
        if (numero == 1)
        {
            debugTextIzq.text = logMessages;
        }
        else
        {
            debugTextDer.text = logMessages;
        }
    }
}