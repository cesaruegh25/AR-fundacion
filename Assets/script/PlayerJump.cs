using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    public int point;
    public int vidas = 3;

    void Start()
    {
        UIManager.instance.MostarMensaje("Vidas restantes: " + vidas, 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            //UIManager.instance.MostarMensaje("Player is grounded.");
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("coin"))
        {
            point++;
            UIManager.instance.MostarMensaje("puntos: " + point, 2);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            vidas--;
            UIManager.instance.MostarMensaje("Vidas restantes: " + vidas, 1);

            if (vidas <= 0)
            {
                
                UIManager.instance.MostarMensaje("Game Over", 1);
            }
        }
    }
}
