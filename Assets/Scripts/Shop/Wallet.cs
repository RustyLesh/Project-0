using UnityEngine;
using System;

namespace Project0
{
    [Serializable]
    public class Wallet
    {
        [SerializeField]private int coinCount;

        public int CoinCount
        {
            private set => coinCount = value;
            get => coinCount;
        }

        public Wallet()
        {
            coinCount = 0;
        }

        public Wallet(int Value)
        {
            coinCount = Value;
        }

        public void GainCoins(int amount)
        {
            if (amount > 0)
            {
                coinCount += amount;
            }
            else
            {
                Debug.LogWarning("Trying to gain negative amount or 0. Amount must be greater than 0\n Consider using \"PayCoins\" method");
            }
        }

        public bool PayCoins(int amount)
        {
            //TODO: Fix negative value purchases
            if (amount < 0)
            {
                Debug.LogWarning("Trying to pay a negative amount. Amount must be a positive value. \n Consider using \"GainCoins\" method");
                return false;
            }

            if (coinCount >= amount)
            {
                coinCount -= amount;
                return true;
            }
            else
            {
                Debug.Log("Not enough coinage"); //TODO: Notify the player in ui.
                return false;
            }
        }
    }
}
