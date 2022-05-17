/*
 * Author: Peter An
 * 
 * This will manage the logic behind the gameplay screen
 * All elements on the gamescreen UI will be managed here
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameScreen : MonoBehaviour
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

    bool timerOn = false;
    bool bossSpawned = false;

    // CACHE
    GameObject playerShip;
    GameObject bossShip;
    //CSS_GameManager gameManager = CSS_GameManager.Instance;


    void Start()
    {
        // Initiallize Player related
        playerShip = CSS_GameManager.Instance.playerShip;
        playerHPBar.maxValue = playerShip.GetComponent<PlayerShip>().PlayerHealth.GetHealth();
        playerHPBar.value = playerShip.GetComponent<PlayerShip>().PlayerHealth.GetHealth();

        // Init stage
        currentStageText.text = $"Stage: {currentStage}";

        // boss object off
        bossObject.SetActive(false);
        bossDefeatPanel.SetActive(false);

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
            else
            {
                timerOn = false;
                timeRemainingInSecondsText.text = "Boss Fight";
            }
        }


        // Update the boss health bar
        if (bossSpawned)
        {
            bossHPBar.value = bossShip.GetComponent<CSS_Boss>().GetTotalBossHealth();
        }

        if (CSS_GameManager.Instance.isBossDead)
        {
            ReturnToMainMenu("boss");
        }

        if(CSS_GameManager.Instance.isPlayerDead)
        {
            ReturnToMainMenu("player");
        }

    }

    void OnEnable()
    {
        Health.OnHealthChanged += HealthChanged;
        CSS_GameManager.onBossUpdate += ActivateBossHealthBar;
    }

    void OnDisable()
    {
        Health.OnHealthChanged -= HealthChanged;
        CSS_GameManager.onBossUpdate -= ActivateBossHealthBar;
    }

    // Subscribing to the Health. Updates value when player health is changed
    public void HealthChanged()
    {
        playerHPBar.value = playerShip.GetComponent<PlayerShip>().PlayerHealth.GetHealth();
    }

    // Subscribing to boss.
    void ActivateBossHealthBar()
    {
        timeRemainingInSecondsText.text = "Boss Fight";
        bossShip = CSS_GameManager.Instance.bossShip;
        bossObject.SetActive(true);
        bossSpawned = true;

        // TODO: Fix null error (prob instatiate timing different to Invoke)
        //bossHPBar.maxValue = bossShip.GetComponent<CSS_Boss>().GetTotalBossHealth();
    }

    void UpdateTime()
    {

        timeRemainingInSecondsText.text = (140 - (int)CSS_GameManager.Instance.gameTimer).ToString();
        //timeRemainingInSecondsText.text = ((int) Game).ToString();
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

        playerShip.GetComponent<PlayerShip>().DisablePlayer();
        StartCoroutine(DelayToMainMenu());
    }

    IEnumerator DelayToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
