/*
 * Author: Peter An
 * 
 * This will manage the logic behind the gameplay screen
 * All elements on the gamescreen UI will be managed here
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScreen : MonoBehaviour
{
    // Player
    [SerializeField] Slider playerHPBar;

    //Level
    [SerializeField] TMP_Text timeRemainingInSecondsText;
    [SerializeField] TMP_Text currentStageText;
    [SerializeField] TMP_Text currentDeathsText;
    [SerializeField] TMP_Text currentScoreText;

    int timeRemainingInSeconds = 60;
    int currentStage = 0;
    int currentDeaths = 0;
    int currentScore = 0;

    private PlayerShip playerShip;

    void Start()
    {
        // Initiallize Player related   
        playerShip = FindObjectOfType<PlayerShip>();
        playerHPBar.value = playerShip.PlayerHealth.GetHealth();
        Health.OnHealthChanged += HealthChanged;

        // Initiallize UI related
        // currentStage = getCurrentStage();
        // currentDeaths = getCurrentGameManager().getTotalDeaths();
        // currentScore = getCurrentGameManager().getTotalScore();
        // plz implement these so I can finish this
    }
    // Subscribing to the Health. Updates value when player health is changed
    public void HealthChanged()
    {
        //Debug.Log("Health changed!");
        playerHPBar.value = playerShip.PlayerHealth.GetHealth();
    }
}
