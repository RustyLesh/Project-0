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
            MoveDownwards();
            //If coin falls out of view destroy it
            if (!IsInView()) {
                Destroy(gameObject);
            }
        }

        public void DestroySelf() {
            if (!IsInView()) {
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

