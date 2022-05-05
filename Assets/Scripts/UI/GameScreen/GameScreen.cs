/*
 * Author: Peter An
 * 
 * This will manage the logic behind the gameplay screen
 * All elements on the gamescreen UI will be managed here
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    float timeRemainingInSeconds = 140;
    int currentStage = 1;

    bool timerOn = false;
    bool bossSpawned = false;

    // CACHE
    PlayerShip playerShip;
    GameObject bossShip;
    CSS_GameManager gameManager = CSS_GameManager.Instance;

    

    void Start()
    {
        // Initiallize Player related   
        playerShip = FindObjectOfType<PlayerShip>();
        playerHPBar.value = playerShip.PlayerHealth.GetHealth();

        // Init stage
        currentStageText.text = $"Stage: {currentStage}";

        // boss object off
        bossObject.SetActive(false);

        timerOn = true;
    }

    void Update()
    {
        //Timer UI
        if (timerOn)
        {
            if (timeRemainingInSeconds > 0)
            {
                timeRemainingInSeconds -= Time.deltaTime;
                UpdateTime();
            }
            else
            {
                timerOn = false;
                timeRemainingInSeconds = 0;
                timeRemainingInSecondsText.text = "Boss Fight";
            }
        }


        // Update the boss health bar
        if (bossSpawned)
        {
            Debug.Log(bossShip.GetComponent<CSS_Boss>().GetTotalBossHealth());
            bossHPBar.value = bossShip.GetComponent<CSS_Boss>().GetTotalBossHealth();
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
        //Debug.Log("Health changed!");
        playerHPBar.value = playerShip.PlayerHealth.GetHealth();
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
        timeRemainingInSecondsText.text = ((int) timeRemainingInSeconds).ToString();
    }
}
