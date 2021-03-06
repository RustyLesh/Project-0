/*
 * Author: Peter An
 * 
 * Manages the pause screen when player enters the game menu
 * All logic related to pausing is handled in this class
 * 
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CSS_PauseScreen : MonoBehaviour
{
    public CSS_GameManager gameManager;
    
    public static bool GameIsPaused = false;

    // Cache gameobject
    [SerializeField] GameObject pauseMenuUI;

    // Register to controls
    PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        gameManager = CSS_GameManager.Instance;
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
        if (gameManager.isPlayerDead || gameManager.isBossDead) return;

        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // Set global time to 1 and hide pause UI
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    // Set global time to 0 and show pause UI
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Exit Application
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
