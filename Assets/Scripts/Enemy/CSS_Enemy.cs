using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    public int hp = 10;     // Base 10
    public int bodyDmg = 50;     // 
    public float movementSpeed = 3.0f;
    public float fireSpeed = 2.0f;
    public float fireReload = 2.0f;

    [Space]
    [Header("Debug Functions")]
    public bool isTakingDamage = false;

    [Space]
    [Header("Pattern Info")]
    public int waypointPos = 1;     // Skip 0 as they spawn at 0
    public int movementPatternID;

    private Vector2 moveDown2D = new Vector2(0, -1);

    [SerializeField] SpawnCoin spawnCoin;
    [SerializeField] SpawnParticleSystem spawnParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        //movementPatternID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.SetMovementPattern(this.movementPatternID);
        this.Shooting();

        // Debug Functions
        this.DebugIsTakeDamage();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Shooting()
    {
        if(fireReload > 0.0f)
        {
            fireReload -= Time.deltaTime;
        }
        else
        {
            // Spawn bullet class and do any set up needed for bullet modifcation here
            // NOTE: if trying to tailor specific bullet types and behaviour notify Leo
            // to set up check enemy type for easier spawning in bullet class (reminder for Leo simple enum struct for enemy type)
            // Input Spawn bullet here
            Debug.Log("Enemy Mob is Firing");

            // Reload
            fireReload = fireSpeed;
        }
        
    }

    // Delete itself when mob has survived and moved off screen
    // when hitting the offscreen collider
    public void DeleteItSelf()
    {
        Destroy(this.gameObject);
    }

    // When mob dies from player attacks
    public void OnDeath()
    {
        // Put VFX and lootdrop chance functions here from other classes
        // Both functions should be spawning objects itself so that it would 
        // be affected when this object delete itself.
        // Do stuff here....

        spawnCoin.SpawnCoinOnDeath(gameObject.transform);
        spawnParticleSystem.ParticleEffectOnDeath(gameObject.transform);

        // Delete itself
        this.DeleteItSelf();
    }

    // Bullet class should call this function and pass in the bullet damage 
    // value base dmg should be 10 to one hit ordinary mobs
    public void TakeDamage(int _dmg)
    {
        this.hp -= _dmg;

        if(this.hp <= 0)
        {
            this.OnDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerShip")
        {
            //Debug.Log("Enemy Touched Player");

            // Health script with take damage function Invoke line linked to UI 
            collision.gameObject.GetComponent<PlayerShip>().PlayerHealth.TakeDamage((float) this.bodyDmg);
            this.OnDeath();
            
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Setting Enemey Movement Patterns will determine 
    /// the enemies behaviour. 
    /// </summary>
    public void SetMovementID(int _id)
    {
        this.movementPatternID = _id;
    }

    void SetMovementPattern(int _id)
    {
        switch (_id)
        {
            case 0: // Basic Enemy moving down
                {
                    // Simple Basic move down 
                    // For any items, basic items that needs to move down to off the screen
                    this.transform.position = this.transform.position + new Vector3(0, -1 * this.movementSpeed * Time.deltaTime, 0);
                    break;
                }
            case 1:
                {
                    //CSS_Spawn.Instance.GetAtkRun01();

                    // MoveTowards follows towards target
                    // For more advance movement patterns that requires following waypoints 
                    // or targeting the player itself.

                    Transform[] tempPattern = CSS_Spawn.Instance.GetAtkRun01();
                    this.transform.position = Vector2.MoveTowards(this.transform.position,
                                                                tempPattern[this.waypointPos].position, 
                                                                this.movementSpeed * Time.deltaTime);

                    if(Vector2.Distance(transform.position, tempPattern[this.waypointPos].position) <= 0.1f)
                    {
                        if(this.waypointPos < tempPattern.Length - 1)
                        {
                            this.waypointPos++;
                        }
                        else
                        {
                            // Simple delete
                            this.DeleteItSelf();
                        }
                    }

                    break;
                }
            default:
                {
                    Debug.Log("Debug MSG From CSS_Enemt: Movement Pattern setting Error");
                    break;
                }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Debug Functions for live testing
    /// 
    public void DebugIsTakeDamage()
    {
        if(this.isTakingDamage == true)
        {
            this.TakeDamage(10);
        }
    }
}
