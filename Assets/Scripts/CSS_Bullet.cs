/*
 * Author: Luke Jordens
 * This class handles all the movement and behaviours of the bullets fired from both players and enemies.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Bullet : MonoBehaviour
{
    // Checks if bullet is fired from player
    [SerializeField] private bool isFiredFromPlayer = true; 
    
    // Handles directions that bullets fire
    [SerializeField] private float angle;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 velocityDir;
    [SerializeField] private int direction = 1;
    private Vector3 vecDirection = new Vector3(0, 0, 0);

    // Holds data about different bullet types (Scriptable Objects)
    [SerializeField]  private SO_Bullet data;
    public  SO_Bullet enemyData;
    
    // Holds data for audio clips related to shooting
    CSS_AudioPlayer audioPlayer;

    //Base damage for boss bullets
    private int baseDamage = 15;

    

    private void Awake()
    {
        audioPlayer = FindObjectOfType<CSS_AudioPlayer>();
    }

    void Update()
    {
        Shoot(); // Calls shoot function every frame
    }

    // Creates movement for the bullet
    public void Shoot()
    {
        if (isFiredFromPlayer == true)
        {
            this.transform.position += this.vecDirection * data.speed * Time.deltaTime;
            this.transform.eulerAngles = new Vector3(0, 0, this.GetAngleFromVectorFloat(this.vecDirection));
        }
        else
        {
            this.transform.position += this.vecDirection * enemyData.speed * Time.deltaTime;
            this.transform.eulerAngles = new Vector3(0, 0, this.GetAngleFromVectorFloat(this.vecDirection));
        }
        
        Destroy(gameObject, 5f); // Destroys bullet after 5 seconds
    }

    // Check weather bullet is fired from player
    public void SetPlayerFired(bool _isFiredFromPlayer)
    {
        this.isFiredFromPlayer = _isFiredFromPlayer;
        if (isFiredFromPlayer == true)
        {
            direction = 1;
            SpriteRenderer sprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = data.sprite;
        }
        else
        {
            direction = -1;
            SpriteRenderer sprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = enemyData.sprite;
        }

        this.SetVecDirection(new Vector3(0, this.direction, 0));
    }

    // Calculates position of player for enemies that aim directly for the player
    public void SetVelocityDirection(Vector3 _angle){
        float angle = Mathf.Atan2(_angle.x, _angle.y) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.velocityDir = new Vector2(this.transform.rotation.x, this.transform.rotation.y * enemyData.speed);
    }

    public void SetVecDirection(Vector3 _direction)
    {
        this.vecDirection = _direction;
    }

    // Handles collisions when bullet collides with player or enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && this.isFiredFromPlayer == true)
        {
            collision.gameObject.GetComponent<CSS_Enemy>().TakeDamage(data.baseDamage); 
            Destroy(this.gameObject);
            audioPlayer.PlayCollisionClip();

        }
        else if (collision.gameObject.tag == "Boss" && this.isFiredFromPlayer == true)
        {
            collision.gameObject.GetComponent<CSS_BossModules>().TakeDamage(baseDamage);
            Destroy(this.gameObject);
            audioPlayer.PlayCollisionClip();

        }
        else if(collision.gameObject.tag == "PlayerShip" && this.isFiredFromPlayer == false)
        {
            collision.gameObject.GetComponent<CSS_Health>().TakeDamage(enemyData.baseDamage);
            Destroy(this.gameObject);
            audioPlayer.PlayCollisionClip();

        }
    }

    // Takes in a targeted object transform angle, start calculation and normalizes the direction vector between the object and the bullet
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

