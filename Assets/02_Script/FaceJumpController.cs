using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class FaceJumpController : MonoBehaviour
{
    public TMP_Text txtPrueba;
    public ARFace face;
    public Rigidbody playerRb;
    public PlayerJump player;

    [Header("Configuración del Salto")]
    public float jumpForce = 8f;
    public float cooldown = 0.8f;

    [Header("Detección de Gesto Rápido")]
    [Tooltip("Velocidad en grados/segundo necesaria para saltar. Valores más altos requieren un movimiento más brusco.")]
    public float pitchSpeedThreshold = 180f;

    private float lastJumpTime;
    private float previousPitch = 0f;
    private bool hasPreviousPitch = false;

    void Update()
    {
        if (face == null || playerRb == null)
        {
            hasPreviousPitch = false; // Reiniciamos si se pierde el rastreo
            return;
        }

        // 1. Conseguimos el ángulo de rotación X actual y lo normalizamos a [-180, 180]
        float currentPitch = face.transform.localEulerAngles.x;
        if (currentPitch > 180) currentPitch -= 360;

        // 2. Si es el primer frame con la cara detectada, solo guardamos el valor actual
        if (!hasPreviousPitch)
        {
            previousPitch = currentPitch;
            hasPreviousPitch = true;
            return;
        }

        // 3. CALCULAMOS LA VELOCIDAD: (Ángulo Actual - Ángulo Anterior) / Tiempo del Frame
        // Al mover la cabeza rápido hacia ARRIBA, este valor se vuelve NEGATIVO.
        float pitchVelocity = (currentPitch - previousPitch) / Time.deltaTime;

        // Guardamos el ángulo actual para que sea el "anterior" en el siguiente frame
        previousPitch = currentPitch;

        // 4. Mostramos la velocidad en pantalla para poder ajustarla con precisión
        if (txtPrueba != null)
        {
            txtPrueba.text = $"Velocidad Pitch: {pitchVelocity.ToString("F0")}°/s\nSuelo: {player.isGrounded}";
        }

        // 5. CONDICIÓN DE SALTO:
        // Si la velocidad es menor que el umbral negativo (un tirón rápido hacia arriba)
        if (pitchVelocity < -pitchSpeedThreshold && player.isGrounded && Time.time - lastJumpTime > cooldown)
        {
            Jump();
        }
    }

    void Jump()
    {
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        player.isGrounded = false;
        lastJumpTime = Time.time;
        player.animator.SetTrigger("Jump");
    }
}