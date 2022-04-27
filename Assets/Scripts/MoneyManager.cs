using UnityEngine;
using Project0;


public class MoneyManager : MonoBehaviour
{
    #region Singleton

    public static MoneyManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory instance found!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [SerializeField]Wallet wallet;

    public int CoinBaseValue = 1;
    public float MoneyMultiplier = 1;

    void OnEnable()
    {
        Coin.OnCoinCollected += CoinCollected;
    }

    void Start()
    {
        wallet = new Wallet();
    }

    public Wallet PlayerWallet
    {
        private set => wallet = value;
        get => wallet;
    }

    private void CoinCollected()
    {
        wallet.GainCoins((int)(CoinBaseValue * MoneyMultiplier));
    }

    void OnDisable()
    {
        Coin.OnCoinCollected -= CoinCollected;
    }

    public void DebugAddCoins(int coins)
    {
        wallet.GainCoins(coins);
    }

    public int GetCoinInWallet()
    {
        return wallet.CoinCount;
    }
}
