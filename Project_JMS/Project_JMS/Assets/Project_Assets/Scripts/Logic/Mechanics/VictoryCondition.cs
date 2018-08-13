﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{

    AudioManager am;
    GameManager gm;
    private float playerSpeed;
    public const float speedOfLight = 299.792458f;
    [SerializeField] private float victorySpeed;
    private MovementController controller;
    [SerializeField] private Distorter distorter;
    private bool victory = false;
    float lerpTime = 0;
    [SerializeField] private AudioSource dissableSource;
    bool levelLoading = false;//quick toggle to only fire level loading once.
    [SerializeField] private string levelToLoad;



    // Use this for initialization
    void Start()
    {
        if (am == null)
            am = GetComponent<AudioManager>();
        if (distorter == null)
            distorter = FindObjectOfType<Distorter>();
        if (victorySpeed == 0)
            victorySpeed = speedOfLight;

        gm = GameManager.instance;
        if (controller == null)
            controller = gm.movementController;
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = controller.CurrentSpeed;
        if (playerSpeed >= victorySpeed / 10)
        {
            if (!victory)
            {
                am.PlayFXAudio(1, 1f, 1f);
                victory = true;

            }
            else if (victory)
            {
                dissableSource.volume = dissableSource.volume - Time.deltaTime / 3;

                //Swap to 3Dworld
                if (!levelLoading)
                {
                    levelLoading = true;
                    StartCoroutine(GetComponent<AudioManager>().NextLVL(1, levelToLoad));
                }


            }
            gm.movementController.CanMove(false);
            lerpTime += Time.deltaTime / 4;
            distorter.LerpExposure(lerpTime);


        }

    }


}
