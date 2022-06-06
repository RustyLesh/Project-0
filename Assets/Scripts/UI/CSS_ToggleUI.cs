/*
 * Author: Peter An
 * 
 * This is a helper method to
 * toggle between two screens.
 * 
 * Mainly used for the shp and inventory to be toggled
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CSS_ToggleUI : MonoBehaviour
{
    [SerializeField] GameObject firstScreen;
    [SerializeField] GameObject secondScreen;

    bool firstScreenOn = true;
    bool secondScreenOn = false;

    // Register to controls
    PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        firstScreen.SetActive(firstScreenOn);
        secondScreen.SetActive(secondScreenOn);
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.UIControls.Inventory.started += OnToggle;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.UIControls.Inventory.started -= OnToggle;
    }

    void OnToggle(InputAction.CallbackContext context)
    {
        firstScreenOn = !firstScreenOn;
        secondScreenOn = !secondScreenOn;

        firstScreen.SetActive(firstScreenOn);
        secondScreen.SetActive(secondScreenOn);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
