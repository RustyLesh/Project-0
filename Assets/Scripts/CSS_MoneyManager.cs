using UnityEngine;
using Project0;
using System;
/// <summary>
/// Singleton
/// Stores amount of money players has and the corrisponding logic.
/// Contains event calls for UI and multipliers for dynamic difficulty.
/// </summary>

public class CSS_MoneyManager : MonoBehaviour, CSS_ISaveable
{
    #region Singleton

    public static CSS_MoneyManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one inventory instance found!");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [field: SerializeField] public int money { get; private set; }
    [field: SerializeField] public float moneyMultiplier { get; private set; } = 1;

    private int coinBaseValue = 1;

    public event Action onChange;

    void OnEnable()
    {
        Coin.OnCoinCollected += CoinCollected;
    }

    //To be called when a coin drop is collected
    private void CoinCollected()
    {
        GainCoins((int)(coinBaseValue * moneyMultiplier));
    }

    void OnDisable()
    {
        Coin.OnCoinCollected -= CoinCollected;
    }

    //Used when purchasing
    public bool PayCoins(int amount)
    {
        //TODO: Fix negative value purchases
        if (amount < 0)
        {
            Debug.LogWarning("Trying to pay a negative amount. Amount must be a positive value. \n Consider using \"GainCoins\" method");
            return false;
        }

        if (money >= amount)
        {
            money -= amount;

            // notify wallet changed
            onChange?.Invoke();

            return true;
        }
        else
        {
            Debug.Log("Not enough coinage"); //TODO: Notify the player in ui.
            return false;
        }
    }

    //Used when collecting coins( or selling items ).
    public void GainCoins(int amount)
    {
        if (amount > 0)
        {
            money += amount;

            // notify wallet changed
            onChange?.Invoke();

        }
        else
        {
            Debug.LogWarning("Trying to gain negative amount or 0. Amount must be greater than 0\n Consider using \"PayCoins\" method");
        }
    }

    public void AdjustMoneyMultiplier(float amount)
    {
        moneyMultiplier = Mathf.Clamp(moneyMultiplier + amount, 0, float.MaxValue);
    }

    #region Save/Load
    public object SaveState()
    {
        Debug.Log(money);
        return new SaveData()
        {
            Money = this.money,
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        money = saveData.Money;
    }

    [Serializable]
    private struct SaveData
    {
        public int Money;
    }
    #endregion
}
