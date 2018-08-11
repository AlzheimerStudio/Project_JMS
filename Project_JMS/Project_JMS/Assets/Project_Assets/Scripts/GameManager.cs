using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MovementController movementController;

    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI distanceText;

    public Transform gridMovement;
    public GameObject barrierPrefab;

    private int _score = 0;
    public int Score { get { return _score; } set { _score = value; } }

    public float distanceTravelled = 0;
    [HideInInspector] public int barrierNumber = 0;
    [SerializeField] private float barrierSpawnDistance = 5000;

    void Awake()
    {
        instance = this;
        if (movementController == null)
        {
            movementController = FindObjectOfType<MovementController>();
        }
    }

    public void UpdateSpeedText(float speed)
    {
        if (speedText == null)
            return;
        speedText.text = "Speed: " + (speed * 10f).ToString("000.000");

    }

    public void UpdateDistance(float speed)
    {
        distanceTravelled += speed;

        if (distanceTravelled >= barrierSpawnDistance)
        {
            Debug.Log(barrierSpawnDistance);
            SpawnBarrier();
            barrierSpawnDistance *= 2;
        }


        if (distanceText != null)
            distanceText.text = "Distance: " + distanceTravelled.ToString("00000000.000");
    }

    void SpawnBarrier()
    {
        if (barrierPrefab == null)
        {
            Debug.LogError("No prefab specified");
            return;
        }
        Barrier barrier = Instantiate(barrierPrefab, new Vector3(50, 0, 0), Quaternion.identity).GetComponent<Barrier>();
        barrier.speedRequired *= barrierNumber + (barrierSpawnDistance / 1000);
        barrier.deaccelerateAmount += barrierNumber ;
        barrierNumber++;


    }
}
