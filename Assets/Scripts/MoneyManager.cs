using UnityEngine;
using Project0;
using System;


public class MoneyManager : MonoBehaviour, ISaveable
{
    #region Singleton

    public static MoneyManager Instance;

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

    [SerializeField] public int Money { get; private set; }
    public int CoinBaseValue = 1;
    public float MoneyMultiplier = 1;

    void OnEnable()
    {
        Coin.OnCoinCollected += CoinCollected;
    }

    private void CoinCollected()
    {
        GainCoins((int)(CoinBaseValue * MoneyMultiplier));
    }

    void OnDisable()
    {
        Coin.OnCoinCollected -= CoinCollected;
    }

    public void DebugAddCoins(int coins)
    {
        GainCoins(coins);
    }

    public bool PayCoins(int amount)
    {
        //TODO: Fix negative value purchases
        if (amount < 0)
        {
            Debug.LogWarning("Trying to pay a negative amount. Amount must be a positive value. \n Consider using \"GainCoins\" method");
            return false;
        }

        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough coinage"); //TODO: Notify the player in ui.
            return false;
        }
    }

    public void GainCoins(int amount)
    {
        if (amount > 0)
        {
            Money += amount;
        }
        else
        {
            Debug.LogWarning("Trying to gain negative amount or 0. Amount must be greater than 0\n Consider using \"PayCoins\" method");
        }
    }

    public object SaveState() {
        Debug.Log(Money);
        return new SaveData() {
            Money = this.Money,
        };
    }

    public void LoadState(object state) {
        var saveData = (SaveData)state;
        Money = saveData.Money;
    }

    [Serializable]
    private struct SaveData {
        public int Money;
    }
}
