using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    public int point;
    public int vidas = 3;

    void Start()
    {
        UIManager.instance.MostarMensaje_1("Vidas restantes: " + vidas);
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
            UIManager.instance.MostarMensaje_1("puntos: " + point);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            vidas--;
            UIManager.instance.MostarMensaje_1("Vidas restantes: " + vidas);

            if (vidas <= 0)
            {
                
                UIManager.instance.MostarMensaje_2("Game Over");
            }
        }
    }
}
