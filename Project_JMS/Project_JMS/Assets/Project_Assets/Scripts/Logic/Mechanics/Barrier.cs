using UnityEngine;

public class Barrier : MonoBehaviour
{
    MovementController _movementController;
    public float speedRequired = 10f;
    public float speed = 1f;
    public float deaccelerateAmount = 1f;
    bool barrierActivated = false;

    void Start()
    {
        _movementController = GameManager.instance.movementController;
    }

    void Update()
    {
        if (!barrierActivated)
        {
            if (transform.position.x <= 2.5f)
            {
                if ((_movementController.CurrentSpeed + _movementController.StrengthBonus) >= speedRequired)
                {
                    // you broke through the barrier
                    // _movementController.Deaccelerate(deaccelerateAmount);
                    transform.position = new Vector3(2.5f, 0, 0);
                    _movementController.timeSpeed = 1f;
                    GameManager.instance.audioManager.ChangePitchOnMixer(1);
                    GameManager.instance.audioManager.PlayFXAudio(0, 1f, 1f);
                    Camera.main.fieldOfView = 115f;
                    Destroy(gameObject);

                }
                else
                {
                    // you shall not pass
                    transform.position = new Vector3(2.5f, 0, 0);
                    _movementController.Die();
                    _movementController.StopMovement();
                    barrierActivated = true;
                }
            }
            else
            {
                if ((_movementController.CurrentSpeed + _movementController.StrengthBonus) >= speedRequired
                && (_movementController.CurrentSpeed + _movementController.StrengthBonus) < GameManager.instance.barrierNumber * 4f)
                {
                    ChangeTimeSpeed();
                }

                MoveLeft();

            }
        }
    }

    void ChangeTimeSpeed()
    {
        float distance = 0 - transform.position.x;
        if (distance <= 50 + (GameManager.instance.barrierNumber * 10))
        {
            _movementController.timeSpeed = Mathf.Abs(distance) / 50f;

            Camera.main.fieldOfView = Mathf.Lerp(100f, 115f, Mathf.Abs(distance) / 50f);
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 100f, 115f);

            GameManager.instance.audioManager.ChangePitchOnMixer(Mathf.Clamp01(Mathf.Abs(distance) / 50f));
            _movementController.timeSpeed = Mathf.Clamp(_movementController.timeSpeed, 0.1f, 1f);
        }

    }

    void MoveLeft()
    {
        transform.Translate((Vector3.left * speed) * _movementController.CurrentSpeed * _movementController.timeSpeed);
    }
}
