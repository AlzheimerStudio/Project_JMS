using System.Collections;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public CameraShakeur cameraShakeur;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.4f;

    MovementController _movementController;
    private ParticleSystem pExplosion;
    public float speedRequired = 10f;
    public float speed = 1f;
    public float deaccelerateAmount = 1f;
    bool barrierActivated = false;

    void Start()
    {
        cameraShakeur = Camera.main.GetComponent<CameraShakeur>() ?? Camera.main.gameObject.AddComponent<CameraShakeur>();
        _movementController = GameManager.instance.movementController;
        pExplosion = GetComponentInChildren<ParticleSystem>();
        pExplosion.gameObject.SetActive(false);
    }

    void Update()
    {
        MoveLeft();

        if (!barrierActivated)
        {
            if (transform.position.x <= 4.5f)
            {
                if ((_movementController.CurrentSpeed + _movementController.StrengthBonus) >= speedRequired)
                {
                    // you broke through the barrier
                    // _movementController.Deaccelerate(deaccelerateAmount);
                    // transform.position = new Vector3(2.5f, 0, 0);
                    _movementController.timeSpeed = 1f;
                    GameManager.instance.audioManager.ChangePitchOnMixer(1f, 0f);
                    GameManager.instance.audioManager.PlayFXAudio(0, 1f, 1f);
                    Camera.main.fieldOfView = 115f;

                    StartCoroutine(DestroyWait(3.5f));
                    if (cameraShakeur != null)
                        StartCoroutine(cameraShakeur.Shake(shakeDuration, shakeMagnitude));
                    barrierActivated = true;

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
                && _movementController.CurrentSpeed < 4f)
                {
                    ChangeTimeSpeed();
                }

            }

        }

    }

    void ChangeTimeSpeed()
    {
        float distance = 0 - transform.position.x;
        if (distance <= 50 + (GameManager.instance.barrierNumber * 20))
        {
            _movementController.timeSpeed = Mathf.Abs(distance) / 50f;
            _movementController.timeSpeed = Mathf.Clamp(_movementController.timeSpeed, 0.1f, 1f);

            Camera.main.fieldOfView = Mathf.Lerp(100f, 115f, Mathf.Abs(distance) / 50f);
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 100f, 115f);

            GameManager.instance.audioManager.ChangePitchOnMixer(Mathf.Clamp01(Mathf.Abs(distance) / 50f), -9f);
        }

    }

    void MoveLeft()
    {
        transform.Translate((Vector3.left * speed) * _movementController.CurrentSpeed * _movementController.timeSpeed);
    }

    IEnumerator DestroyWait(float time)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        if (pExplosion != null)
            pExplosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);

    }
}
