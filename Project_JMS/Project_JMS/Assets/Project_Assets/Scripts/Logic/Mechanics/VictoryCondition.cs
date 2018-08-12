using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour {

						private float playerSpeed;
	[SerializeField] 	private float victorySpeed = 300;
						private MovementController controller;

	// Use this for initialization
	void Start () 
	{
		if (controller == null) 
		{
			controller = GameObject.Find("[CONTROLLER]").GetComponent<MovementController>();
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
