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
                    GameManager.instance.audioManager.PlayFXAudio(0, 1f);
                    _movementController.timeSpeed = 1f;
                    GameManager.instance.audioManager.ChangePitchOnMixer(1);
                    Destroy(gameObject);

                }
                else
                {
                    // you shall not pass
                    _movementController.Die();
                    _movementController.StopMovement();
                    barrierActivated = true;
                }
            }
            else
            {
                if ((_movementController.CurrentSpeed + _movementController.StrengthBonus) >= speedRequired)
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
            GameManager.instance.audioManager.ChangePitchOnMixer(Mathf.Clamp01(Mathf.Abs(distance) / 50f));
            _movementController.timeSpeed = Mathf.Clamp(_movementController.timeSpeed, 0.1f, 1f);
        }

    }

    void MoveLeft()
    {
        transform.Translate((Vector3.left * speed) * _movementController.CurrentSpeed * _movementController.timeSpeed);
    }
}
