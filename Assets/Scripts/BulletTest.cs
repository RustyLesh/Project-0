using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BulletTest : MonoBehaviour
    {
        public PlayerShip PlayerShipShoot;
        public float speed = 5.0f;
        public Rigidbody2D rb;
        public int direction = -1;
        public int baseDamage = 10;


    void Start()
        {
            this.transform.Rotate(new Vector3(0, 0, -90));
        }
        void Update()
        {
            this.rb.velocity = new Vector2(0.0f, direction * speed);
            Destroy(gameObject, 3f);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<CSS_Enemy>().TakeDamage(baseDamage); 
            Destroy(gameObject);
        }

        else if(collision.gameObject.tag == "PlayerShip")
        {
            //collision.gameObject.GetComponent<Health>().TakeDamage(baseDamage);
            Health player = collision.GetComponent<Health>();
            if (player != null)
            {
                player.TakeDamage(baseDamage);
                Destroy(gameObject);
            }
            
        }
    }
}

