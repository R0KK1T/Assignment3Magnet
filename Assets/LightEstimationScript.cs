using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LightEstimationScript : MonoBehaviour
{
    private Light light;
    public ARCameraManager cameraManager;

    private void Start()
    {
        light = GetComponent<Light>();
        RenderSettings.ambientMode = AmbientMode.Skybox;
    }

    private void OnEnable()
    {
        cameraManager.frameReceived += handleChange;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= handleChange;
    }

    void handleChange(ARCameraFrameEventArgs args)
    {
        light.intensity = args.lightEstimation.averageBrightness.Value;
        light.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        light.color = args.lightEstimation.colorCorrection.Value;
        light.transform.rotation = Quaternion.LookRotation(args.lightEstimation.mainLightDirection.Value);
        light.intensity = args.lightEstimation.mainLightIntensityLumens.Value;

        RenderSettings.ambientProbe = args.lightEstimation.ambientSphericalHarmonics.Value;
    }
}
