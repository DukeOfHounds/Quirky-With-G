using Unity.XR.XREAL;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("We startin up...");

        // Force switch to use 6 DOF tracking on start
        _ = XREALPlugin.SwitchTrackingTypeAsync(TrackingType.MODE_6DOF);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
