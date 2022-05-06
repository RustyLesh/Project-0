using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;

    PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.UIControls.Pause.started += OnPause;
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerControls.UIControls.Pause.started -= OnPause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log("Paused");
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
