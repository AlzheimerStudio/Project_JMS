using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Distorter : MonoBehaviour {

	[SerializeField]	private PostProcessVolume volume;
						private LensDistortion lensDistortion;
	[SerializeField]	private int lensCap = -45;
	[Range(0,1000)]		public float speed;						// TODO:	Get speed value from movement controller
	[Range(0,2)]		public float distortionMultiplier;
	
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
