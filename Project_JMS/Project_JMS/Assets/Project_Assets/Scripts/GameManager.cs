using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UpgradeManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MovementController movementController;
    private UpgradeManager upgradeManager;
    [HideInInspector] public AudioManager audioManager;

    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI distanceText;

    // public Transform gridMovement;
    public GameObject barrierPrefab;

    private int _points = 0;
    public int Points { get { return _points; } set { _points = value; _points = Mathf.Clamp(_points, 0, int.MaxValue); } }

    public float distanceTravelled = 0;
    [HideInInspector] public int barrierNumber = 0;
    [SerializeField] private float barrierSpawnDistance = 500;
    private float originalBarrierSpawnDistance;

    void Awake()
    {
        instance = this;
        originalBarrierSpawnDistance = barrierSpawnDistance;
        if (movementController == null)
        {
            movementController = FindObjectOfType<MovementController>();
        }
        upgradeManager = GetComponent<UpgradeManager>();
        audioManager = GetComponent<AudioManager>();
    }

    public void UpdateSpeedText(float speed)
    {
        if (speedText == null)
            return;
        speedText.text = "Speed: " + (speed * 10f).ToString("000.00");

    }

    public void UpdateDistance(float speed)
    {
        distanceTravelled += speed;

        if (distanceTravelled >= barrierSpawnDistance)
        {
            // Debug.Log(barrierSpawnDistance);
            SpawnBarrier();
            barrierSpawnDistance *= 2;
        }


        if (distanceText != null)
            distanceText.text = "Distance: " + distanceTravelled.ToString("00000000.00");
    }

    void SpawnBarrier()
    {
        if (barrierPrefab == null)
        {
            Debug.LogError("No prefab specified");
            return;
        }
        Barrier barrier = Instantiate(barrierPrefab, new Vector3((50 + (barrierNumber * 20)), 0, 0), Quaternion.identity).GetComponent<Barrier>();
        barrier.speedRequired *= (barrierSpawnDistance / 1000);
        barrier.deaccelerateAmount += barrierNumber;
        barrierNumber++;


    }

    public void ToggleUpgradeManagerScreen(bool state)
    {
        upgradeManager.ShowUpdateScreen(state);
    }

    public void Respawn()
    {
        movementController.Reset();
        Points += (int)(distanceTravelled / 1000f);
        barrierSpawnDistance = originalBarrierSpawnDistance;
        distanceTravelled = 0;
        barrierNumber = 0;
        ToggleUpgradeManagerScreen(true);
        upgradeManager.UpdateUI();
    }
}
