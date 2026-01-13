using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceRunnerControlller : MonoBehaviour
{
    public ARFace face;
    public Transform player;


    // Update is called once per frame
    void Update()
    {
        if (face == null && player == null)
        {
            return;
        }

        Vector3 headRotation = face.transform.localEulerAngles;

        float giroCabeza = headRotation.y;

        if (giroCabeza > 180)
        {
            giroCabeza -= 360;
        }

        player.Translate(new Vector3(giroCabeza * Time.deltaTime, 0, 0));

    }
}
