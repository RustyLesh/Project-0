using System;
using UnityEngine;

namespace Project0
{
    public class Coin : MonoBehaviour
    {
        public delegate void CoinCollected();
        public static event CoinCollected OnCoinCollected;

        [SerializeField] GameObject goldCoin;
        [SerializeField] float speed = 1f;
        private bool spawned = false; 


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "PlayerShip")
            {
                //OnCoinCollected.Invoke();
                Destroy(gameObject);
                Debug.Log("Player ship touched coin");
            }
        }

        void Update() {
            //If sprite has rendered set spawned to true
            if (goldCoin.GetComponent<Renderer>().isVisible) {
                spawned = true;
            }
            MoveDownwards();
            //If coin has spawned and Renderer.IsVisible is false, destroy coin
            if (spawned && !IsInView()) {
                Destroy(gameObject);
            }
        }
        
        public void MoveDownwards() {
            //Move coin downwards
            transform.position += Vector3.down * Time.deltaTime * speed;
        }

        //Checks if coin is outside of camera view
        public bool IsInView() {
            return goldCoin.GetComponent<Renderer>().isVisible;
        }
    }
}

