using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class FaceJumpController : MonoBehaviour
{
    public TMP_Text txtPrueba;
    public ARFace face;
    public Rigidbody playerRb;
    public PlayerJump player;

    public float jumpFloat = 30.0f;
    public float jumpForce = 4f;
    public float pitchThreshold = -15f;
    public float cooldown = 0.8f;

    private bool isGrounded = true;
    private float lastJumpTime;
    

    // Update is called once per frame
    void Update()
    {
        if (face == null || playerRb == null) return;

        Vector3 rotation = face.transform.localEulerAngles;
        float pitch = rotation.x;
        //UIManager.instance.MostarMensaje_1("Pitch_1: " + pitch);
        if (pitch > jumpFloat) pitch -= 360;
        //UIManager.instance.MostarMensaje_2("Pitch_2: " + pitch);

        if (pitch < pitchThreshold && player.isGrounded && Time.time - lastJumpTime > cooldown)
        {
            //UIManager.instance.MostarMensaje_1("Jump detected with pitch: " + pitch);
            Jump();
        }
    }
    void Jump()
    {
        UIManager.instance.MostarMensaje_2("Jumping!");
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        player.isGrounded = false;
        lastJumpTime = Time.time;
    }
}
