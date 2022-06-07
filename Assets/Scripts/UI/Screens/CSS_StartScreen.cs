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
    [SerializeField] Button ShopButton;
    [SerializeField] Button QuitButton;

    // Load game screen
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLoadScene() 
    {
        SceneManager.LoadScene(3);
    }

    public void LoadSaveScene() {
        SceneManager.LoadScene(4);
    }

    public void LoadStartScreen() {
        SceneManager.LoadScene(0);
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
