using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    GameManager gm;
    [SerializeField] private Transform backGround;  // Background to move
    [SerializeField] private Vector2 backGroundClamp = new Vector2(-10, 10);    // Clamps position of background so it never goes out of view

    [SerializeField] private float acceleration = 1f;   // Acceleration per spacebar press
    private float _accelerationBonus = 0f;  // Acceleration bonus when upgraded
    public float AccelerationBonus { set { _accelerationBonus += value; } }

    private float _currentSpeed = 0f;    // Holds current speed
    public float CurrentSpeed { get { return _currentSpeed; } }
    private float _oldSpeed = 0f;   // Holds old speed (when paused etc)
    private float _strengthBonus = 0f;  // Bonus for smashing through barriers
    public float StrengthBonus { get { return _strengthBonus; } set { _strengthBonus += value; } }
    bool canMove = true;

    public float timeSpeed = 1f;    // used for barrier slow motion

    float timeBetweenSteps = 0f;

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
                _currentSpeed += (acceleration + _accelerationBonus);
                gm.ToggleUpgradeManagerScreen(false);
            }
            Move();
            WrapAround();
            Friction();

            if (gm != null)
            {
                gm.UpdateSpeedText(_currentSpeed);
                gm.UpdateDistance(_currentSpeed);

                if (_currentSpeed > 0)
                {
                    timeBetweenSteps -= Time.deltaTime;
                    if (timeBetweenSteps <= _currentSpeed)
                    {
                        if (_currentSpeed > 1)
                            gm.audioManager.PlayPlayerAudio(2, .2f, 1f);
                        else
                            gm.audioManager.PlayPlayerAudio(1, .1f, 1f);
                        timeBetweenSteps = 1f;
                    }
                }
            }
        }
    }

    void Move()
    {
        Vector3 newPosition = Vector3.zero;
        newPosition.x -= _currentSpeed * timeSpeed;


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
        if (_currentSpeed <= 1f) 
        {
            _currentSpeed -= Time.deltaTime / 5;
        } 
        else if (_currentSpeed <= 2.5f) 
        {
            _currentSpeed -= Time.deltaTime / 4;
        }
        else if (_currentSpeed <= 5f) 
        {
            _currentSpeed -= Time.deltaTime / 3;
        }
        else
        {
            _currentSpeed -= Time.deltaTime / 2;
        }
        
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, float.MaxValue);
    }

    public void Reset()
    {
        // Resets player after death

        foreach (Barrier barrier in FindObjectsOfType<Barrier>())
        {
            Destroy(barrier.gameObject);
        }
        backGround.position = new Vector3(-70, 0, 15);
        _oldSpeed = 0;
        _currentSpeed = 0;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        canMove = true;
    }

    public void RestartMovement()
    {
        canMove = true;
        _currentSpeed = _oldSpeed;
    }

    public void StopMovement()
    {
        canMove = false;
        _oldSpeed = _currentSpeed;
        _currentSpeed = 0;
    }

    public void Die()
    {
        gm.audioManager.PlayPlayerAudio(0, 1f, 1f);
        gm.audioManager.ChangePitchOnMixer(1f, 0f);
        timeSpeed = 1f;
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
        _currentSpeed -= deaccelerateAmount;
    }

    public void CanMove(bool value) 
    {
        canMove = value;
    }
}
