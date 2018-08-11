using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Distorter : MonoBehaviour {

	[SerializeField]	private MovementController controller;
	[SerializeField]	private PostProcessVolume volume;
						private LensDistortion lensDistortion;
	[SerializeField]	private int lensCap = -45;
	[SerializeField]	private float currentDistortionValue;	// Just to check if it updates		
						public float speed;						// Linked with speed from movement controller
	[Range(0,100)]		public float distortionMultiplier;
	
	void Start () 
	{
		if (volume == null) 
		{
			PullPostProcessVolume();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		speed = controller.CurrentSpeed;
		if (volume != null) 
		{
			bool foundLensDistortionSettings = volume.profile.TryGetSettings<LensDistortion>(out lensDistortion);		// Try to find Lens Distortion on Post Process Volume
			if (!foundLensDistortionSettings) 
			{
				Debug.Log("Can't find lens distortion settings");
			} 
			else 
			{				
				lensDistortion.intensity.value = Mathf.Clamp(Mathf.RoundToInt(speed * -distortionMultiplier), lensCap, 0);		// Apply manipulation on Lens Distortion
				currentDistortionValue = lensDistortion.intensity.value;
			}
		} 
		else 
		{
			PullPostProcessVolume();
		}
		
	}

	void PullPostProcessVolume () 
	{
		volume = GetComponent<PostProcessVolume>();
	}
}
