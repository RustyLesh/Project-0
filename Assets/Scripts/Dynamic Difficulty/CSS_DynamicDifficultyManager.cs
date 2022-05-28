using Project0;
using UnityEngine;

public class CSS_DynamicDifficultyManager : MonoBehaviour
{

    private CSS_PlayerShip playerShip;
    private CSS_GameManager gameManager;
    private CSS_MoneyManager moneyManager;
    private CSS_Spawn spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager = CSS_MoneyManager.Instance;
    }


    private void AdjustMoneyMultiplier(float amount)
    {
        moneyManager.AdjustMoneyMultiplier(amount);
    }

    //SpawnManager
    // Add or take away from multipler, clamped cannot go bellow 0

    private void AdjustMobDamage(float amount)
    {
        spawnManager.AdjustMobDamageMultiplier(amount);
    }

    private void AdjustBossDamage(float amount)
    {
        spawnManager.AdjustBossDamageMultiplier(amount);
    }

    private void AdjustMobHealth(float amount)
    {
        spawnManager.AdjustMobHealthMultiplier(amount);
    }

    private void AdjustBossHealth(float amount)
    {
        spawnManager.AdjustBossHealthMultiplier(amount);
    }


    private void OnEnable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply += AdjustMoneyMultiplier;
        DynamicDifficultyDrop.onBossDamageMultiply += AdjustBossDamage;
        DynamicDifficultyDrop.onBossHealthMultiply += AdjustBossHealth;
    }

    private void OnDisable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply -= AdjustMoneyMultiplier;
        DynamicDifficultyDrop.onBossDamageMultiply -= AdjustBossDamage;
        DynamicDifficultyDrop.onBossHealthMultiply -= AdjustBossHealth;
    }
}
