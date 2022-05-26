/*
 * Author: Peter An
 * 
 * This will manage the logic behind the gameplay screen
 * All elements on the gamescreen UI will be managed here
 * 
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CSS_GameScreen : MonoBehaviour
{
    // Player
    [SerializeField] Slider playerHPBar;

    // Boss
    [SerializeField] GameObject bossObject;
    [SerializeField] Slider bossHPBar;

    //Level
    [SerializeField] TMP_Text timeRemainingInSecondsText;
    [SerializeField] TMP_Text currentStageText;

    [SerializeField] GameObject bossDefeatPanel;
    [SerializeField] GameObject playerDeadPanel;

    int currentStage = 1;

    // Flags
    bool timerOn = false;
    bool bossSpawned = false;

    // Cache
    GameObject playerShip;
    GameObject bossShip;


    void Start()
    {
        // Init Player related
        playerShip = CSS_GameManager.Instance.playerShip;
        playerHPBar.maxValue = playerShip.GetComponent<CSS_PlayerShip>().playerHealth.GetHealth();
        playerHPBar.value = playerShip.GetComponent<CSS_PlayerShip>().playerHealth.GetHealth();

        // Init Boss realted
        bossShip = CSS_GameManager.Instance.bossShip;

        // boss object off
        bossObject.SetActive(false);
        bossDefeatPanel.SetActive(false);

        // Init stage
        currentStageText.text = $"Stage: {currentStage}";


        timerOn = true;


    }

    void Update()
    {
        //Timer UI
        if (timerOn)
        {
            if (CSS_GameManager.Instance.gameTimer <= 140)
            {
                UpdateTime();
            }
            // When timer reaches 0, spawn boss UI
            else
            {
                timerOn = false;
                timeRemainingInSecondsText.text = "Boss Fight";
            }
        }


        // Update the boss health bar when spawned
        if (bossSpawned)
        {
            bossHPBar.value = bossShip.GetComponent<CSS_Boss>().GetTotalBossHealth();
        }

        // Scene management when game ends
        if (CSS_GameManager.Instance.isBossDead && !CSS_GameManager.Instance.isPlayerDead)
        {
            ReturnToMainMenu("boss");
        }

        if(CSS_GameManager.Instance.isPlayerDead && !CSS_GameManager.Instance.isBossDead)
        {
            ReturnToMainMenu("player");
        }

    }

    // Subscribe to listners (observers)
    void OnEnable()
    {
        CSS_Health.OnHealthChanged += HealthChanged;
        CSS_GameManager.onBossUpdate += ActivateBossHealthBar;
    }

    void OnDisable()
    {
        CSS_Health.OnHealthChanged -= HealthChanged;
        CSS_GameManager.onBossUpdate -= ActivateBossHealthBar;
    }

    // Subscribing to the Health. Updates value when player health is changed
    public void HealthChanged()
    {
        playerHPBar.value = playerShip.GetComponent<CSS_PlayerShip>().playerHealth.GetHealth();
    }

    // Subscribing to boss.
    void ActivateBossHealthBar()
    {
        timeRemainingInSecondsText.text = "Boss Fight";
        bossObject.SetActive(true);
        bossSpawned = true;
    }

    void UpdateTime()
    {
        timeRemainingInSecondsText.text = (140 - (int)CSS_GameManager.Instance.gameTimer).ToString();
    }

    void ReturnToMainMenu(string entityDead)
    {
        if(entityDead == "boss")
        {
            bossDefeatPanel.SetActive(true);
        }
        else if (entityDead == "player")
        {
            playerDeadPanel.SetActive(true);
        }

        playerShip.GetComponent<CSS_PlayerShip>().DisablePlayer();
        StartCoroutine(DelayToMainMenu());
    }

    // Delay for 5 seconds and enter main menu
    IEnumerator DelayToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
