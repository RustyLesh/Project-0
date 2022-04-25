using UnityEngine;

namespace Project0
{
    public class Wallet
    {
        private int coinCount;

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
            coinCount += amount;
        }

        public bool PayCoins(int amount)
        {
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

        public int getCoinCount()
        {
            return coinCount;
        }
    }
}
