using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] public TMP_Text text_ARI;
    [SerializeField] public TMP_Text text_ARD;
    [SerializeField] public TMP_Text text_ABI;
    [SerializeField] public TMP_Text text_ABD;

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
        //text_1.text = "Hola";
        //text_2.text = "Mundo";
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
