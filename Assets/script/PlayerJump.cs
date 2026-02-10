using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
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
