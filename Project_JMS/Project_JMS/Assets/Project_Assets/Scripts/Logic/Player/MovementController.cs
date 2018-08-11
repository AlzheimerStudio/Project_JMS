using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Transform backGround;
    [SerializeField] private Vector2 backGroundClamp = new Vector2(-10, 10);

    [SerializeField] private float acceleration = 1f;
    private float currentSpeed = 0f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentSpeed += acceleration;
        }
        Move();
        WrapAround();
        Friction();
    }

    void Move()
    {
        Vector3 newPosition = Vector3.zero;
        newPosition.x -= currentSpeed;


        backGround.Translate(newPosition);
    }

    void WrapAround()
    {
        if (backGround.position.x <= backGroundClamp.x)
        {
            backGround.position = new Vector3(backGroundClamp.y, backGround.position.y, backGround.position.z);
        }
    }

    void Friction()
    {
        
    }
}
