using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class FaceJumpController : MonoBehaviour
{
    public ARFace face;
    public Rigidbody playerRb;
    public PlayerJump player;

    [Header("Configuración del Salto")]
    public float jumpForce = 12f;
    public float cooldown = 0.8f;

    [Header("Detección de Gesto Rápido")]
    [Tooltip("Velocidad en grados/segundo necesaria para saltar. Valores más altos requieren un movimiento más brusco.")]
    private float pitchSpeedThreshold = 100f;

    private float lastJumpTime;
    private float previousPitch = 0f;
    private bool hasPreviousPitch = false;

    private void Start()
    {
        lastJumpTime = Time.time;
    }
    void Update()
    {
        if (face == null || playerRb == null)
        {
            hasPreviousPitch = false;
            return;
        }

        float currentPitch = face.transform.localEulerAngles.x;
        if (currentPitch > 180) currentPitch -= 360;

        if (!hasPreviousPitch)
        {
            previousPitch = currentPitch;
            hasPreviousPitch = true;
            return;
        }

        float pitchVelocity = (currentPitch - previousPitch) / Time.deltaTime;

        previousPitch = currentPitch;

        if (pitchVelocity >= pitchSpeedThreshold && player.isGrounded && Time.time - lastJumpTime > cooldown)
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