using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager gm;
    public static bool GamePaused = false;   
    private bool changingKey = false;

    [SerializeField] private Button changeButton;
    [SerializeField] private Text keyText;
    private int oldFontSize;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject keybindUI;
    [SerializeField] private AudioSource[] audioSources;
    private float[] oldVolumes;
    [SerializeField] private string menuName;

    void Start()
    {
        gm = GameManager.instance;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!changingKey)
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
        if (audioSources.Length > 0)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].volume = oldVolumes[i];
            }
        }
        pauseUI.SetActive(false);
        gm.movementController.RestartMovement();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        if (audioSources.Length > 0)
        {
            oldVolumes = new float[audioSources.Length];
            for (int i = 0; i < audioSources.Length; i++)
            {
                oldVolumes[i] = audioSources[i].volume;
                audioSources[i].volume = 0;
            }
        }
        pauseUI.SetActive(true);
        gm.movementController.StopMovement();

        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        if (menuName != "")
        {
            SceneManager.LoadScene(menuName);
        }
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
        oldFontSize = keyText.fontSize;
        keyText.fontSize = keyText.fontSize - 10;
        changingKey = true;
    }
}
