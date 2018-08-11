using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    GameManager gm;
    [SerializeField] private Transform backGround;  // Background to move
    [SerializeField] private Vector2 backGroundClamp = new Vector2(-10, 10);    // Clamps position of background so it never goes out of view

    [SerializeField] private float acceleration = 1f;   // Acceleration per spacebar press
    private float currentSpeed = 0f;    // Holds current speed
    public float CurrentSpeed { get { return currentSpeed; } }
    bool canMove = true;

    void Start()
    {
        gm = GameManager.instance;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentSpeed += acceleration;
            }
            Move();
            WrapAround();
            Friction();
            gm.UpdateSpeedText(currentSpeed);
            gm.UpdateDistance(currentSpeed);
        }
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
        currentSpeed -= Time.deltaTime / 4;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, float.MaxValue);
    }

    public void StopMovement()
    {
        canMove = false;
        currentSpeed = 0;
    }

    public void Deaccelerate(float deaccelerateAmount)
    {
        currentSpeed -= deaccelerateAmount;
    }
}
