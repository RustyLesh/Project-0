using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Enemy : MonoBehaviour
{
    public CSS_PlayerShip EnemyShoot;

    enum EAIState
    {
        Moving,
        Targeting,
        Shooting,
        Retreating
    }

    [Header("Enemy Stats")] // Leave public for now until optimization process
    public int hp = 10;     // Base 10
    public int bodyDmg = 50;     // 
    public float movementSpeed = 3.0f;
    public float fireSpeed = 2.0f;
    [SerializeField] private float fireReload;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float stateTimer;
    [SerializeField] private EAIState state;
    [SerializeField] private GameObject bullet;

    [Space]
    [Header("Debug Functions")]
    public bool isTakingDamage = false;

    [Space]
    [Header("Pattern Info")]
    [SerializeField] private bool isRightSide = false;
    [SerializeField] private int waypointPos = 1;   // Skip 0 as they spawn at 0  
    public int movementPatternID;

    private Vector2 moveDown2D;
    private Transform[] movementPattern;
    private Vector3 playerShipPos;
    private Transform shootPos;

    [SerializeField] SpawnCoin spawnCoin;
    [SerializeField] SpawnParticleSystem spawnParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        //movementPatternID = 0;
        this.fireReload = this.fireSpeed;
        this.rotateSpeed = 40.0f;
        this.stateTimer = 0.0f;
        this.state = EAIState.Moving;
        //this.isRightSide = false;
        //this.waypointPos = 1;   
        this.moveDown2D = new Vector2(0, -1);
        this.shootPos = this.transform.GetChild(1);

        // Turn Forward Direction to down 
        // due to unity Z-axis is the forward dir
        this.transform.Rotate(new Vector3(0, 0, -90));
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateMovementPattern(this.movementPatternID);
        //this.Shooting();

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
            //Debug.Log("Enemy Mob is Firing");
            // Unity Forward always on the Z-axis pointing right -->

            //Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Vector3 spawnPos = this.shootPos.position;

            GameObject newBullet = Instantiate(bullet, spawnPos, Quaternion.identity);
            newBullet.GetComponent<BulletTest>().SetPlayerFired(false);

            // Setting shoot direction
            Vector3 shootDir = this.shootPos.position - this.transform.position;
            if (this.movementPatternID == 2)
            {
                newBullet.GetComponent<BulletTest>().SetVecDirection(shootDir);
            }

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
            collision.gameObject.GetComponent<CSS_PlayerShip>().PlayerHealth.TakeDamage(this.bodyDmg);
            this.OnDeath();
            
        }
    }

    public void SetIsRightSide(bool _isRight)
    {
        this.isRightSide = _isRight;
    }

    public void SetWaypointPos(int _pos)
    {
        this.waypointPos = _pos;
    }

    public void SetMovementSpeed(float _speed)
    {
        this.movementSpeed = _speed;
    }

    public void SetFireSpeed(float _speed)
    {
        this.fireSpeed = _speed;
        this.fireReload = _speed;
    }

    public void SetBodyDamage(int _dmg)
    {
        this.bodyDmg = _dmg;
    }

    public void SetHealth(int _hp)
    {
        this.hp = _hp;
    }

    public void SetPlayerShipPos(Vector3 _vec3)
    {
        this.playerShipPos = _vec3;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Setting Enemey Movement Patterns will determine 
    /// the enemies behaviour. 
    /// </summary>
    public void SetMovementID(int _id)
    {
        this.movementPatternID = _id;

        if(_id != 0)
        {
            this.movementPattern = CSS_Spawn.Instance.GetAtkRun(_id - 1);
        }
    }

    // TODO: After Sprint 1 optimize code if have time seperate behaviour into different scripts
    // which will force a different prefab creations and instantiating differnt objects in spawn class
    private void UpdateMovementPattern(int _id)
    {
        switch (_id)
        {
            case 0: // Basic Enemy moving down
                {
                    // Simple Basic move down 
                    // For any items, basic items that needs to move down to off the screen
                    this.transform.position = this.transform.position + new Vector3(0, -1 * this.movementSpeed * Time.deltaTime, 0);
                    this.Shooting();
                    break;
                }
            case 1:
                {
                    // MoveTowards follows towards target
                    // For more advance movement patterns that requires following waypoints 
                    // or targeting the player itself.

                    if (isRightSide)
                    {
                        //this.waypointPos = this.movementPattern.Length - 2;
                        this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[this.waypointPos].position, this.movementSpeed * Time.deltaTime);

                        if (Vector2.Distance(transform.position, this.movementPattern[this.waypointPos].position) <= 0.1f)
                        {
                            if (this.waypointPos > 0)
                            {
                                this.waypointPos--;
                            }
                            else
                            {
                                // Simple delete
                                this.DeleteItSelf();
                            }
                        }
                    }
                    else
                    {
                        this.transform.position = Vector2.MoveTowards(this.transform.position,
                                                                this.movementPattern[this.waypointPos].position,
                                                                this.movementSpeed * Time.deltaTime);

                        if (Vector2.Distance(transform.position, this.movementPattern[this.waypointPos].position) <= 0.1f)
                        {
                            if (this.waypointPos < this.movementPattern.Length - 1)
                            {
                                this.waypointPos++;
                            }
                            else
                            {
                                // Simple delete
                                this.DeleteItSelf();
                            }
                        }
                    }
                    this.Shooting();
                    break;
                }
            case 2:
                {
                    // Hit and run tactics
                    if (isRightSide)
                    {
                        if (this.state == EAIState.Moving)
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[3].position, this.movementSpeed * Time.deltaTime);

                            if (Vector2.Distance(transform.position, this.movementPattern[3].position) <= 0.1f)
                            {
                                this.state = EAIState.Targeting;
                            }

                        }
                        else if (this.state == EAIState.Targeting)
                        { // total 6 sec

                            this.stateTimer += Time.deltaTime;

                            this.playerShipPos = CSS_GameManager.Instance.playerShip.transform.position;
                            float rotateAngle = (Mathf.Atan2(this.playerShipPos.y - this.transform.position.y,
                                                             this.playerShipPos.x - this.transform.position.x) * Mathf.Rad2Deg);
                            Quaternion tempRotAngle = Quaternion.Euler(new Vector3(0, 0, rotateAngle));
                            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, tempRotAngle, rotateSpeed * Time.deltaTime);

                            if (this.stateTimer >= 3.0f)
                            {
                                this.state = EAIState.Shooting;
                            }

                        }
                        else if (this.state == EAIState.Shooting)
                        {

                            this.stateTimer += Time.deltaTime;
                            this.Shooting();

                            if (this.stateTimer >= 6.0f)
                            {
                                this.state = EAIState.Retreating;
                            }
                        }
                        else if (this.state == EAIState.Retreating)
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[2].position, this.movementSpeed * Time.deltaTime);

                            this.rotateSpeed = 500.0f;
                            Quaternion tempRotAngle = Quaternion.Euler(new Vector3(0, 0, 90));
                            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, tempRotAngle, rotateSpeed * Time.deltaTime);

                            if (Vector2.Distance(transform.position, this.movementPattern[2 ].position) <= 0.1f)
                            {
                                // Simple delete
                                this.DeleteItSelf();
                            }
                        }
                    }
                    else
                    {
                        if(this.state == EAIState.Moving)
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[1].position, this.movementSpeed * Time.deltaTime);

                            if (Vector2.Distance(transform.position, this.movementPattern[1].position) <= 0.1f)
                            {
                                this.state = EAIState.Targeting;
                            }

                        }
                        else if(this.state == EAIState.Targeting){ // total 6 sec

                            this.stateTimer += Time.deltaTime;

                            this.playerShipPos = CSS_GameManager.Instance.playerShip.transform.position;
                            float rotateAngle = (Mathf.Atan2(this.playerShipPos.y - this.transform.position.y, 
                                                             this.playerShipPos.x - this.transform.position.x) * Mathf.Rad2Deg);
                            Quaternion tempRotAngle = Quaternion.Euler(new Vector3(0, 0, rotateAngle));
                            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, tempRotAngle, rotateSpeed * Time.deltaTime);

                            if(this.stateTimer >= 3.0f)
                            {
                                this.state = EAIState.Shooting;
                            }

                        }
                        else if(this.state == EAIState.Shooting)
                        {

                            this.stateTimer += Time.deltaTime;
                            this.Shooting();

                            if (this.stateTimer >= 6.0f)
                            {
                                this.state = EAIState.Retreating;
                            }
                        }
                        else if(this.state == EAIState.Retreating)
                        {
                            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[0].position, this.movementSpeed * Time.deltaTime);

                            this.rotateSpeed = 500.0f;
                            Quaternion tempRotAngle = Quaternion.Euler(new Vector3(0, 0, 90));
                            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, tempRotAngle, rotateSpeed * Time.deltaTime);

                            if (Vector2.Distance(transform.position, this.movementPattern[0].position) <= 0.1f)
                            {
                                // Simple delete
                                this.DeleteItSelf();
                            }
                        }
                    }

                    break;
                }
            case 3:
                {
                    if (isRightSide)
                    {
                        this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[3].position, this.movementSpeed * Time.deltaTime);

                        if (Vector2.Distance(transform.position, this.movementPattern[3].position) <= 0.1f)
                        {
                            // Simple delete
                            this.DeleteItSelf();
                        }
                    }
                    else
                    {    
                        this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[1].position, this.movementSpeed * Time.deltaTime);

                        if (Vector2.Distance(transform.position, this.movementPattern[1].position) <= 0.1f)
                        {
                            // Simple delete
                            this.DeleteItSelf();
                        }
                    }
                    this.Shooting();
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
    private void DebugIsTakeDamage()
    {
        if(this.isTakingDamage == true)
        {
            this.TakeDamage(10);
            this.isTakingDamage = false;
        }
    }
}
