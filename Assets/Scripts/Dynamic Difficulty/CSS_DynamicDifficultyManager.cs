using Project0;
using UnityEngine;

public class CSS_DynamicDifficultyManager : MonoBehaviour
{

    private CSS_PlayerShip playerShip;
    private CSS_GameManager gameManager;
    private CSS_MoneyManager moneyMultiplier;
    private float enemyDamageIncrease;

    // Start is called before the first frame update
    void Start()
    {
        moneyMultiplier = CSS_MoneyManager.Instance;
    }


    private void AdjustMoneyMultiplier(float amount)
    {
        Debug.Log("oo");
        moneyMultiplier.AdjustMoneyMultiplier(amount);
    }
    private void AdjustEnemyDamage(float amount)
    {
        enemyDamageIncrease += amount;
    }

    private void OnEnable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply += AdjustMoneyMultiplier;
    }

    private void OnDisable()
    {
        CSS_MoneyMultiplierDrop.onMoneyMultiply -= AdjustMoneyMultiplier;
    }
}
