using System;
using UnityEngine;

namespace Project0
{
    public class Coin : MonoBehaviour
    {
        public delegate void CoinCollected();
        public static event CoinCollected OnCoinCollected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "PlayerShip")
            {
                //OnCoinCollected.Invoke();
                Destroy(gameObject);
                Debug.Log("Player ship touched coin");
            }
        }
    }
}

