using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Mouse : MonoBehaviour
{

    private MovementController mC;
    public bool checkSPD;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;

        mC = GameManager.instance.movementController; ;
    }

    void Update()
    {
        if (checkSPD)
        {
            if (Application.isFocused && !PauseMenu.GamePaused && mC.CurrentSpeed > 0.01)
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
  
    


