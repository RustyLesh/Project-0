using Project0;
using UnityEngine;

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

    // Start is called before the first frame update
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

    private void OnEnable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply += AdjustMoneyMultiplier;
        DynamicDifficultyDrop.onMoneyMultiply += AdjustMoneyMultiplier;
        DynamicDifficultyDrop.onBossDamageMultiply += AdjustBossDamage;
        DynamicDifficultyDrop.onBossHealthMultiply += AdjustBossHealth;
        DynamicDifficultyDrop.onPlayerDamageMultiply += AdjustPlayerDamage;
        DynamicDifficultyDrop.onPlayerHealthMultiply += AdjustPlayerHealth;
        DynamicDifficultyDrop.onMobDamageMultiply += AdjustMobDamage;
        DynamicDifficultyDrop.onMobHealthMultiply += AdjustMobHealth;
    }

    private void OnDisable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply -= AdjustMoneyMultiplier;
        DynamicDifficultyDrop.onMoneyMultiply -= AdjustMoneyMultiplier;
        DynamicDifficultyDrop.onBossDamageMultiply -= AdjustBossDamage;
        DynamicDifficultyDrop.onBossHealthMultiply -= AdjustBossHealth;
        DynamicDifficultyDrop.onMobDamageMultiply -= AdjustMobDamage;
        DynamicDifficultyDrop.onMobHealthMultiply -= AdjustMobHealth;
    }
}
