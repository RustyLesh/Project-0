using Project0;
using UnityEngine;

/// <summary>
/// Takes in event calls from the dynamic difficulty drop and then passes it on to the related classes.
/// Base difficulty multipliers are 1 ( 100% ). All multipliers from dyn diff drop are added to this.
/// </summary>
public class CSS_DynamicDifficultyManager : MonoBehaviour
{
    #region Singleton

    public static CSS_DynamicDifficultyManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one DynDiff Manager isntance found!");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
    private CSS_PlayerShip playerShip;
    private CSS_MoneyManager moneyManager;
    private CSS_Spawn spawnManager;

    public float enemyMaxHealthMultiplier = 1;
    public float enemyDamageMultiplier = 1;

    void Start()
    {
        moneyManager = CSS_MoneyManager.Instance;
        spawnManager = CSS_Spawn.Instance;
        playerShip = GetComponent<CSS_PlayerShip>();
    }

    private void AdjustMoneyMultiplier(float amount)
    {
        moneyManager.AdjustMoneyMultiplier(amount);
    }

    // Add or take away from multipler, clamped cannot go bellow 0

    ///SpawnManager

    private void AdjustBossDamage(float amount)
    {
        spawnManager.AdjustBossDamageMultiplier(amount);
    }

    private void AdjustBossHealth(float amount)
    {
        spawnManager.AdjustBossHealthMultiplier(amount);
    }

    private void AdjustMobDamage(float amount)
    {
        enemyDamageMultiplier = Mathf.Clamp(enemyDamageMultiplier + amount, 0, float.MaxValue);
    }

    private void AdjustMobHealth(float amount)
    {
        enemyMaxHealthMultiplier = Mathf.Clamp(enemyMaxHealthMultiplier + amount, 0, float.MaxValue);
    }

    //Player ship

    private void AdjustPlayerDamage(float amount)
    {
        playerShip.AdjustDamageMultiplier(amount);
    }

    private void AdjustPlayerHealth(float amount)
    {
        playerShip.AdjustMaxHealthMultiplier(amount);
    }

    //Subcribes to events
    private void OnEnable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply += AdjustMoneyMultiplier;
        CSS_DynamicDifficultyDrop.onMoneyMultiply += AdjustMoneyMultiplier;
        CSS_DynamicDifficultyDrop.onBossDamageMultiply += AdjustBossDamage;
        CSS_DynamicDifficultyDrop.onBossHealthMultiply += AdjustBossHealth;
        CSS_DynamicDifficultyDrop.onPlayerDamageMultiply += AdjustPlayerDamage;
        CSS_DynamicDifficultyDrop.onPlayerHealthMultiply += AdjustPlayerHealth;
        CSS_DynamicDifficultyDrop.onMobDamageMultiply += AdjustMobDamage;
        CSS_DynamicDifficultyDrop.onMobHealthMultiply += AdjustMobHealth;
    }

    private void OnDisable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply -= AdjustMoneyMultiplier;
        CSS_DynamicDifficultyDrop.onMoneyMultiply -= AdjustMoneyMultiplier;
        CSS_DynamicDifficultyDrop.onBossDamageMultiply -= AdjustBossDamage;
        CSS_DynamicDifficultyDrop.onBossHealthMultiply -= AdjustBossHealth;
        CSS_DynamicDifficultyDrop.onMobDamageMultiply -= AdjustMobDamage;
        CSS_DynamicDifficultyDrop.onMobHealthMultiply -= AdjustMobHealth;
    }
}
