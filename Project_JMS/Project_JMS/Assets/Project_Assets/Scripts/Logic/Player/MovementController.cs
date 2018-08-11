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
    private float oldSpeed = 0f;

    public float CurrentSpeed { get { return currentSpeed; } }
    bool canMove = true;

    public Transform playerTransform;
    public GameObject destructionParticles;

    void Start()
    {
        gm = GameManager.instance;
        if (playerTransform == null)
        {
            Debug.LogError("No player transform specified!");
            playerTransform = new GameObject("u forgot to put a playertransform").transform;
        }
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
            if (gm != null)
            {
                gm.UpdateSpeedText(currentSpeed);
                gm.UpdateDistance(currentSpeed);
            }
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

    public void Reset()
    {
        // Resets player after death

        foreach (Barrier barrier in FindObjectsOfType<Barrier>())
        {
            Destroy(barrier.gameObject);
        }
        backGround.position = new Vector3(-70, 0, 15);
        oldSpeed = 0;
        currentSpeed = 0;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        canMove = true;
    }

    public void RestartMovement()
    {
        canMove = true;
        currentSpeed = oldSpeed;
    }

    public void StopMovement()
    {
        canMove = false;
        oldSpeed = currentSpeed;
        currentSpeed = 0;
    }

    public void Die()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        if (destructionParticles != null)
            Instantiate(destructionParticles, playerTransform.position, Quaternion.Euler(-90, 0, 0));

        StartCoroutine(RespawnDelay(3f));
    }

    IEnumerator RespawnDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gm.Respawn();
    }

    public void Deaccelerate(float deaccelerateAmount)
    {
        currentSpeed -= deaccelerateAmount;
    }
}
