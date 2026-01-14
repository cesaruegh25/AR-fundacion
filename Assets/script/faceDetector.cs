using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class faceDetector : MonoBehaviour
{

    public ARFaceManager faceManager;
    public FaceRunnerControlller controller;

    void OnEnable()
    {
        faceManager.facesChanged += OnFacesChanged;
    }
    void OnDisable()
    {
        faceManager.facesChanged -= OnFacesChanged;
    }
    void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        if (args.added.Count > 0)
        {
            controller.face = args.added[0];
        }
    }
}
