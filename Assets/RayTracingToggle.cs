using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class RayTracingToggle : MonoBehaviour
{
    public Button toggleRayTracingButton;
    private HDRenderPipelineAsset hdrpAsset;
    private bool isRayTracingEnabled;

    void Start()
    {
        hdrpAsset = GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset;

        if (hdrpAsset == null)
        {
            Debug.LogError("HDRP asset not found! Ensure HDRP is enabled and set as the active render pipeline.");
            return;
        }

        isRayTracingEnabled = hdrpAsset.currentPlatformRenderPipelineSettings.supportRayTracing;
        UpdateButtonText();

        if (toggleRayTracingButton != null)
        {
            toggleRayTracingButton.onClick.AddListener(ToggleRayTracing);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleRayTracing();
        }
    }

    void ToggleRayTracing()
    {
        isRayTracingEnabled = !isRayTracingEnabled;
        var settings = hdrpAsset.currentPlatformRenderPipelineSettings;
        settings.supportRayTracing = isRayTracingEnabled;
        hdrpAsset.currentPlatformRenderPipelineSettings = settings;
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        if (toggleRayTracingButton != null)
        {
            toggleRayTracingButton.GetComponentInChildren<Text>().text = isRayTracingEnabled ? "Disable Ray Tracing" : "Enable Ray Tracing";
        }
    }
}
