/*
 * Author: Peter An
 * 
 * Displays the basic main menu
 * Players and interact with the UI
 * with this class
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSS_StartScreen : MonoBehaviour
{
    // Cached buttons
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button QuitButton;

    // Load game screen
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
