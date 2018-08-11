using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    GameManager gm;

    [SerializeField] private GameObject upgradeScreen;

    [SerializeField] private int upgradeVelocity = 0;   // more speed up per space bar press
    [SerializeField] private int upgradeStrength = 0;   // easier to break through barriers

    [SerializeField] private TextMeshProUGUI labelPoints;
    [SerializeField] private TextMeshProUGUI labelVelocity;
    [SerializeField] private TextMeshProUGUI labelStrength;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void Upgrade(string upgrade)
    {
        if (upgrade == "Velocity")
        {
            if (gm.Points > 0)
            {
                upgradeVelocity++;
                gm.Points--;
            }
        }
        else if (upgrade == "Strength")
        {
            if (gm.Points > 0)
            {
                upgradeStrength++;
                gm.Points--;
            }
        }
        else
        {
            Debug.Log("Upgrade not recognized");
        }

        UpdateLabels();
    }

    public void UpdateLabels()
    {
        if (labelVelocity == null || labelStrength == null || labelPoints == null)
            return;

        labelPoints.text = gm.Points.ToString();
        labelVelocity.text = upgradeVelocity.ToString();
        labelStrength.text = upgradeStrength.ToString();
    }

    public void ShowUpdateScreen(bool state)
    {
        if (upgradeScreen != null)
        {
            upgradeScreen.SetActive(state);
        }
    }
}
