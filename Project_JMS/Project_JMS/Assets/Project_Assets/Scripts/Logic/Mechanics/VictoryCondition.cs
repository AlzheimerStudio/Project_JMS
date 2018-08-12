using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour {

						GameManager gm;
						private float playerSpeed;
						public const float speedOfLight = 299.792458f;
	[SerializeField] 	private float victorySpeed;
						private MovementController controller;

	// Use this for initialization
	void Start () 
	{
		if (victorySpeed == 0) 
		{
			victorySpeed = speedOfLight;
		}
		gm = GameManager.instance;
		if (controller == null) 
		{
			controller = gm.movementController;
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerSpeed = controller.CurrentSpeed;
		if (playerSpeed >= victorySpeed/10) 
		{
			controller.CanMove(false);
			// TODO : swap to 3D world
		}
	}
}
