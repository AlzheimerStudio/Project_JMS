using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Mouse : MonoBehaviour
{

    private MovementController mC;
    public bool checkSPD;

    void Start()
    {
        if (checkSPD)
        {
            mC = GameManager.instance.movementController;
        }
    }

    void Update()
    {
        if (checkSPD)
        {
            if (Application.isFocused && !PauseMenu.GamePaused && mC.CurrentSpeed > 0.1f)
            {
                Cursor.visible = false;
            }
        }
        else if (Application.isFocused)
        {
            Cursor.visible = false;
        }
    }
}




