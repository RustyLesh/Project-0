using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    [SerializeField] private bool isFiredFromPlayer = true;
    public float speed = 5.0f;
    public float angle;
    public Rigidbody2D rb;
    [SerializeField] private Vector2 velocityDir;
    [SerializeField] private int direction = 1;
    public int baseDamage = 10;

    void Start()
    {
        this.velocityDir = new Vector2(0.0f, direction * speed);
        //angle = Mathf.Atan2(this.transform.rotation.x, this.transform.rotation.y) * Mathf.Rad2Deg;

        this.transform.Rotate(new Vector3(0, 0, -90));
    }
    void Update()
    {
        this.rb.velocity = this.velocityDir;
        //this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //this.velocityDir = new Vector2(this.transform.rotation.x, this.transform.rotation.y * speed);
        Destroy(gameObject, 3f);
    }

    public void SetPlayerFired(bool _isFiredFromPlayer)
    {
        this.isFiredFromPlayer = _isFiredFromPlayer;
        if (isFiredFromPlayer == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    public void SetVelocityDirection(Vector3 _angle){
        //this.velocityDir = new Vector2(0.0f, direction * speed);
        float angle = Mathf.Atan2(_angle.x, _angle.y) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.velocityDir = new Vector2(this.transform.rotation.x, this.transform.rotation.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && this.isFiredFromPlayer == true)
        {
            collision.gameObject.GetComponent<CSS_Enemy>().TakeDamage(baseDamage); 
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Boss" && this.isFiredFromPlayer == true)
        {
            collision.gameObject.GetComponent<CSS_BossModules>().TakeDamage(baseDamage);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "PlayerShip" && this.isFiredFromPlayer == false)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(baseDamage);
            Destroy(this.gameObject);
        }
    }
}

