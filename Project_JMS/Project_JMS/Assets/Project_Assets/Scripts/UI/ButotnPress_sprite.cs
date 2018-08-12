using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButotnPress_sprite : MonoBehaviour
{


    public Sprite unpressed;
    public Sprite pressed;

    public Image img;
    public GameObject textObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Space"))
        {
            img.sprite = pressed;
            textObj.SetActive(false);

        }
        else
        {
            img.sprite = unpressed;
            textObj.SetActive(true);


        }
    }
}
