using UnityEngine;

public class Barrier : MonoBehaviour
{
    MovementController _movementController;
    public float speedRequired = 10f;
    public float speed = 1f;
    public float deaccelerateAmount = 1f;

    void Start()
    {
        _movementController = GameManager.instance.movementController;
    }

    void Update()
    {
        if (transform.position == Vector3.right)
        {
            if (_movementController.CurrentSpeed >= speedRequired)
            {
                // you broke through the barrier
                _movementController.Deaccelerate(1f);
                Destroy(gameObject);

            }
            else
            {
                // you shall not pass
                _movementController.StopMovement();
            }
        }
        else
        {
            MoveLeft();

        }
    }

    void MoveLeft()
    {
        transform.Translate(Vector3.left * speed);
    }
}
