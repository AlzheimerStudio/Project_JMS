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

    public bool useForExit = false;
    public AudioSource clipLength;
    public AudioSource turnOff;
    public GameObject CanvasDir;

    // Update is called once per frame
    void Update()
    {
        if (!useForExit)
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
        else if (Input.GetButton("Space"))
        {
            StartCoroutine(ExitSequence());
        }


    }

    IEnumerator ExitSequence()
    {
        if (Input.GetButtonDown("Space"))
        {
            turnOff.gameObject.SetActive(false);
            CanvasDir.SetActive(false);
            clipLength.PlayOneShot(clipLength.clip);
            yield return new WaitForSeconds(clipLength.clip.length);
            Application.Quit();
        }
    }

}

