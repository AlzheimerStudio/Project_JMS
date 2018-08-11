using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Distorter : MonoBehaviour
{

    [SerializeField] private MovementController controller;
    [SerializeField] private PostProcessVolume volume;
    private LensDistortion lensDistortion;
    [SerializeField] private int lensCap = -45;
    public float speed;                     // Linked with speed from movement controller
    [Range(0, 100)] public float distortionMultiplier;

    void Start()
    {
        if (volume == null)
        {
            PullPostProcessVolume();
        }
        controller = FindObjectOfType<MovementController>();
    }

    void Update()
    {
        speed = controller.CurrentSpeed;
        if (volume != null)
        {
            bool foundLensDistortionSettings = volume.profile.TryGetSettings<LensDistortion>(out lensDistortion);       // Try to find Lens Distortion on Post Process Volume
            if (!foundLensDistortionSettings)
            {
                Debug.Log("Can't find lens distortion settings");
            }
            else
            {
                float newIntensity = Mathf.RoundToInt(speed * -distortionMultiplier);
                newIntensity = Mathf.Clamp(newIntensity, lensCap, 0);
                lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, newIntensity, Time.deltaTime * 10f);        // Apply manipulation on Lens Distortion
            }
        }
        else
        {
            PullPostProcessVolume();
        }

    }

    void PullPostProcessVolume()
    {
        volume = GetComponent<PostProcessVolume>();
    }
}
