﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    GameManager gm;
    MovementController movementController;

    [SerializeField] private GameObject upgradeScreen;

    private int upgradeVelocity = 0;   // more speed up per space bar press
    private int upgradeStrength = 0;   // easier to break through barriers

    private int upgradeVelocityCost = 1;
    private int upgradeStrengthCost = 1;

    [SerializeField] private TextMeshProUGUI labelPoints;
    [SerializeField] private TextMeshProUGUI labelVelocity;
    [SerializeField] private TextMeshProUGUI labelStrength;
    [SerializeField] private TextMeshProUGUI labelToolTipCost;
    [SerializeField] private Button buttonVelocityUpgrade;
    [SerializeField] private Button buttonStrengthUpgrade;

    void Start()
    {
        gm = GameManager.instance;
        this.movementController = gm.movementController;
        UpdateUI();
    }

    public void Upgrade(string upgrade)
    {
        if (upgrade == "Velocity")
        {
            if (gm.Points > 0 && gm.Points - upgradeVelocityCost >= 0)
            {
                upgradeVelocity++;
                movementController.AccelerationBonus = 0.01f;
                gm.Points -= upgradeVelocityCost;
                upgradeVelocityCost = (int)(upgradeVelocity * 1.5f);
            }
        }
        else if (upgrade == "Strength")
        {
            if (gm.Points > 0 && gm.Points - upgradeStrengthCost >= 0)
            {
                upgradeStrength++;
                movementController.StrengthBonus = 0.1f;
                gm.Points -= upgradeStrengthCost;
                upgradeStrengthCost = (int)(upgradeStrength * 1.5f);

            }
        }
        else
        {
            Debug.Log("Upgrade not recognized");
        }

        UpdateUI();
    }

    public void UpdateUpgradeCostUI(string tip)
    {
        if (labelToolTipCost != null)
        {
            if (tip == "Velocity")
                labelToolTipCost.text = "Cost: " + upgradeVelocityCost.ToString();
            if (tip == "Strength")
                labelToolTipCost.text = "Cost: " + upgradeStrengthCost.ToString();
        }
    }

    public void UpdateUI()
    {
        try
        {
            labelPoints.text = gm.Points.ToString();
            labelVelocity.text = upgradeVelocity.ToString();
            labelStrength.text = upgradeStrength.ToString();
            buttonVelocityUpgrade.interactable = gm.Points - upgradeVelocityCost >= 0 ? true : false;
            buttonStrengthUpgrade.interactable = gm.Points - upgradeStrengthCost >= 0 ? true : false;

        }
        catch (Exception e)
        {
            Debug.LogError("UpdateUI() failed! Exception catched: " + e);
            return;
        }
    }

    public void ShowUpdateScreen(bool state)
    {
        UpdateUI();
        if (upgradeScreen != null)
        {
            upgradeScreen.SetActive(state);
            Cursor.visible = state;

        }
    }
}
