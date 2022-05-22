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
    private Vector3 vecDirection = new Vector3(0,0,0);
    [SerializeField] private int direction = 1;
    public int baseDamage = 10;
    public Sprite[] bulletSprites;

  

    void Start()
    {
        //this.velocityDir = new Vector2(0.0f, direction * speed);
        //this.transform.Rotate(new Vector3(0, 0, -90));
    }
    void Update()
    {
        //this.rb.velocity = this.velocityDir;
        this.transform.position += this.vecDirection * this.speed * Time.deltaTime;
        this.transform.eulerAngles = new Vector3(0, 0, this.GetAngleFromVectorFloat(this.vecDirection));
        // Replace with offscreen deletetion instead Refer to coin deletetion code
        Destroy(gameObject, 5f);
    }

    public void SetPlayerFired(bool _isFiredFromPlayer)
    {
        this.isFiredFromPlayer = _isFiredFromPlayer;
        if (isFiredFromPlayer == true)
        {
            direction = 1;
            SpriteRenderer sprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = bulletSprites[0];
        }
        else
        {
            /*if (gameObject.tag == "Boss")
            {
                SpriteRenderer sprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
                sprite.sprite = bulletSprites[1];
            }*/
            direction = -1;
            /**/
        }

        this.SetVecDirection(new Vector3(0, this.direction, 0));
    }

    public void SetVelocityDirection(Vector3 _angle){
        //this.velocityDir = new Vector2(0.0f, direction * speed);
        float angle = Mathf.Atan2(_angle.x, _angle.y) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.velocityDir = new Vector2(this.transform.rotation.x, this.transform.rotation.y * speed);
    }

    public void SetVecDirection(Vector3 _direction)
    {
        this.vecDirection = _direction;
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

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float angleTemp = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angleTemp < 0)
        {
            angleTemp += 360;
        }

        return (angleTemp);
    }
}

