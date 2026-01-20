using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded;
    public int vidas = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            Debug.LogError("Player is grounded.");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            vidas--;
            Debug.LogError("Player hit an enemy. Lives left: " + vidas);

            if (vidas <= 0)
            {
                Debug.LogError("derrota");
            }
        }
    }
}
