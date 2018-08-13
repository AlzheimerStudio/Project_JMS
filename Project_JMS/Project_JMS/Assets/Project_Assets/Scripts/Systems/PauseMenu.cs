using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameManager gm;
    public static bool GamePaused = false;
    private bool changingKey = false;

    [SerializeField] private Button changeButton;
    [SerializeField] private TextMeshProUGUI keyText;
    private int oldFontSize;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject keybindUI;

    private AudioMixer mixer;

    void Start()
    {
        gm = GameManager.instance;
        mixer = gm.audioManager.mixer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!changingKey)
        {
            if (!keybindUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (GamePaused)
                    {
                        Resume();
                    }
                    else
                    {
                        Pause();
                    }
                }
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                keyText.fontSize = oldFontSize;
                keyText.text = "SPACE";
                changeButton.interactable = true;
                changingKey = false;
            }
        }
    }

    public void Resume()
    {
        mixer.SetFloat("_LowPass", 5000f);
        pauseUI.SetActive(false);
        gm.movementController.RestartMovement();
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.visible = false;
    }

    void Pause()
    {
        mixer.SetFloat("_LowPass", 600f);
        pauseUI.SetActive(true);
        gm.movementController.StopMovement();

        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void ChangeKeyBind()
    {
        changeButton.interactable = false;
        keyText.text = "<ANY KEY>";
//        oldFontSize = keyText.fontSize;
        keyText.fontSize = keyText.fontSize - 10;
        changingKey = true;
    }
}
