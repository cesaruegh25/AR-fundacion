using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Animator animator;
    public bool isGrounded;
    public int point = 0;
    public int vidas = 3;

    void Start()
    {
        UIManager.instance.MostarMensaje("Vidas restantes: " + vidas, 1);
        UIManager.instance.MostarMensaje("puntos: " + point, 2);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            UIManager.instance.MostarMensaje("Player is grounded.", 4);
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            point++;
            UIManager.instance.MostarMensaje("puntos: " + point, 2);
            Destroy(other.gameObject); // Destruye la moneda para que desaparezca al cogerla
        }

        if (other.CompareTag("enemy"))
        {
            vidas--;
            UIManager.instance.MostarMensaje("Vidas: " + vidas, 1);
            
            if (vidas <= 0)
            {
                UIManager.instance.MostarMensaje("Game Over", 4);
                Time.timeScale = 0;  // Detiene el juego
            }
        }
    }
}
