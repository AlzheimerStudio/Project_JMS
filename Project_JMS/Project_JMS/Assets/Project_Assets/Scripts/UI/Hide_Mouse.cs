using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Mouse : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Application.isFocused && !PauseMenu.GamePaused)
        {
            Cursor.visible = false;
        }
    }

}
