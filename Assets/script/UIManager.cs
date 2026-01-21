using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text text_1;
    public TMP_Text text_2;

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
        text_1.text = "Hola";
        text_2.text = "Mundo";
    }
    
    public void MostarMensaje_1(string msj)
    {
        text_1.text = msj;
    }
    public void MostarMensaje_2(string msj)
    {
        text_2.text = msj;
    }
}
