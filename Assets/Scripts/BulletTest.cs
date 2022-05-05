using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project0
{
    public class BulletTest : MonoBehaviour
    {
        public PlayerShip PlayerShipShoot;
        public float speed = 5.0f;
        public Rigidbody2D rb;
        public int direction = -1;
        public bool firedFromPlayer = false;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.Rotate(new Vector3(0, 0, -90));
        }

        // Update is called once per frame
        void Update()
        {
            this.rb.velocity = new Vector2(0.0f, direction * speed);
        }

        public void SetPlayerFired(bool isFiredFromPlayer)
        {
            if (isFiredFromPlayer == true)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }
    }
}
