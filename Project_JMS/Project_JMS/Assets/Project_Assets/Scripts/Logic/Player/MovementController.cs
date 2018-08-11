using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform backGround;

    [SerializeField] private float acceleration = 1f;
    private float currentSpeed = 0f;
	

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
            Move();
        }

    }

	void Move()
	{

	}
}
