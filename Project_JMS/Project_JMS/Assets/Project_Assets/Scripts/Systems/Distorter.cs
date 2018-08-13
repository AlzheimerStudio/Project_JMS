using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class Distorter : MonoBehaviour
{

    [SerializeField] private MovementController controller;
    [SerializeField] private PostProcessVolume volume;

    private LensDistortion lensDistortion;
    [Header("Lens Distortion")]
    [SerializeField] private int lensCap = -45;
    public float speed;                     // Linked with speed from movement controller
    [Range(0, 100)] public float distortionMultiplier;

    private ChromaticAberration chromAbb;
    [Header("Chromatic Abberation")]
    [SerializeField] private float chromAbbIntensityCap = 1f;
    [Range(0, 100)] public float chromaticIntensityModifier;

    private ColorGrading colorGrading;
    [Header("Color grading")]
    [SerializeField] private float hueShiftCap = 180f;
    [Range(0, 100)] public float hueShiftModifier = 1f;

    private ColorGrading transition;
    [Header("Victory transition")]
    [SerializeField] private AnimationCurve EVcurve;

    void Start()
    {
        if (volume == null)
        {
            volume = GetComponent<PostProcessVolume>();
        }
        controller = FindObjectOfType<MovementController>();
    }

    void LateUpdate()
    {
        speed = controller.CurrentSpeed / 4;

        if (volume != null)
            UpdatePostProcessingSettings();

    }

    void UpdatePostProcessingSettings()
    {

        if (volume.profile.TryGetSettings<LensDistortion>(out lensDistortion))       // Try to find Lens Distortion on Post Process Volume
        {
            float newLensDistortionIntensity = Mathf.RoundToInt(speed * -distortionMultiplier);
            newLensDistortionIntensity = Mathf.Clamp(newLensDistortionIntensity, lensCap, 0);
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, newLensDistortionIntensity, Time.deltaTime * 10f);        // Apply manipulation on Lens Distortion
        }
        if (volume.profile.TryGetSettings<ChromaticAberration>(out chromAbb))
        {
            float newChromAbbIntensity = Mathf.RoundToInt(speed * chromaticIntensityModifier);
            newChromAbbIntensity = Mathf.Clamp(newChromAbbIntensity, 0, chromAbbIntensityCap);
            chromAbb.intensity.value = Mathf.Lerp(chromAbb.intensity.value, newChromAbbIntensity, Time.deltaTime * 10f);        // Apply manipulation on Chromatic Abberation
        }
        if (volume.profile.TryGetSettings<ColorGrading>(out colorGrading))
        {
            float newHueShift = Mathf.RoundToInt(speed * hueShiftModifier);
            newHueShift = Mathf.Clamp(newHueShift, 0, hueShiftCap);
            colorGrading.hueShift.Interp(colorGrading.hueShift.value, newHueShift, Time.deltaTime * 5f);
        }
    }

    public void LerpExposure(float t)
    {
        if (volume.profile.TryGetSettings<ColorGrading>(out transition))
        {
            transition.postExposure.value = EVcurve.Evaluate(t);
//            Debug.Log(EVcurve.Evaluate(t));
        }
    }
}


