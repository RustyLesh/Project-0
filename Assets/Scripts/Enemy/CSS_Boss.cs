using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Boss : MonoBehaviour
{
    public enum EBossState
    {
        Approach,
        Engagement,
        Death
    }

    [Header("Boss Stats")]
    bool isBossDead = false;
    public int bossID = 1;  // Only 1 boss atm
    public int bodyDmg = 50;
    [SerializeField] private int totalHP;
    private int waypoint;
    [SerializeField] private float movementSpeed = 2.0f;
    public EBossState state;

    private Transform[] movementPattern;
    private List<CSS_BossModules> modules;

    // Debug
    [SerializeField] private bool isTakingDamage = false;

    public void Init()
    {
        this.waypoint = 0;
        this.state = EBossState.Approach;
        this.modules = new List<CSS_BossModules>();
        this.SetModules();
        this.totalHP = this.GetTotalBossHealth();

        // Turn Forward Direction to down 
        // due to unity Z-axis is the forward dir
        this.transform.Rotate(new Vector3(0, 0, -90));
    }

    // Update is called once per frame
    void Update()
    {
        this.MovementUpdate();

        // debug
        this.DebugIsTakeDamage();
    }

    private void MovementUpdate()
    {
        if(this.state == EBossState.Approach)
        {
            // Compared to enemy class spawn position is split off from the movement pattern
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[this.waypoint].position, this.movementSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, this.movementPattern[this.waypoint].position) <= 0.1f)
            {
                // can do a brieft yield coroutine to pause and show off boss in dramatic scene here

                this.waypoint++;
                this.state = EBossState.Engagement;
            }
        }
        else if(this.state == EBossState.Death)
        {
            // Initiate death sequence here

            // Ping death to gameplay manager
        }
        else if(this.state == EBossState.Engagement)
        {
            switch (this.bossID)
            {
                case 1: // First level boss
                    {
                        // Moving Loop Code
                        this.transform.position = Vector2.MoveTowards(this.transform.position, this.movementPattern[this.waypoint].position, this.movementSpeed * Time.deltaTime);

                        if (Vector2.Distance(transform.position, this.movementPattern[this.waypoint].position) <= 0.1f)
                        {
                            if(this.waypoint < this.movementPattern.Length - 1)
                            {
                                this.waypoint++;
                            }
                            else
                            {
                                // Reset back to 0 and keep going in loop
                                // till boss dies
                                this.waypoint = 0;
                            }
                        }

                        // Shooting Loop Code
                        // Gun Turrets are [1] and [2]
                        this.modules[1].GetComponent<CSS_ModuleTurretBase>().Shoot();
                        this.modules[2].GetComponent<CSS_ModuleTurretBase>().Shoot();

                        break;
                    }
                case 2:
                    {
                        // Secret Boss
                        break;
                    }
                default:
                    {
                        Debug.Log("Boss ID is out of range: "+this.bossID);
                        break;
                    }
            }
        }
    }

    // Will be called by Modules during their death sequence to ping this func
    // to check if all other modules are also destroyed to end game.
    public void CheckModules()
    {
        this.isBossDead = true;
        for(int i = 0; i < this.modules.Count; i++)
        {
            if (this.modules[i].GetComponent<CSS_BossModules>().GetIsDestroyed() == false)
            {
                // As long one module is still alive keep game going and stop check
                isBossDead = false;
                break;
            }
        }

        if (isBossDead)
        {
            // Ping Game Manager boss is dead
            CSS_GameManager.Instance.SetIsBossDead(true);
        }
    }

    // Getting total health with all modules combined
    // for UI healthbar
    public int GetTotalBossHealth()
    {
        int tempTotal = 0;
        for (int i = 0; i < this.modules.Count; i++)
        {
            tempTotal += this.modules[i].GetComponent<CSS_BossModules>().GetModHP();
        }
        this.totalHP = tempTotal;

        Debug.Log($"Boss health: {totalHP}");
        return (totalHP);
    }

    public void ApplyLifeMultiplierToModules(float multiplier)
    {
        for(int i = 0; i <= this.modules.Count; i++)
        {
            modules[i].SetModHP((int)(modules[i].GetModHP() * multiplier));
        }
    }

    public void ApplyDamageMultiplierToModules(float multiplier)
    {
        for (int i = 0; i <= this.modules.Count; i++)
        {
            if(modules[i].moduleType == BossModuleType.TURRET)
            {
                CSS_ModuleTurretBase turret = (CSS_ModuleTurretBase)modules[i];
                turret.AdjustDamageMultiplier(multiplier);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// Encapsulators
    ///

    private void SetModules()
    {
        // Modules are stored in index 0 
        for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
        {
            this.modules.Add(this.transform.GetChild(0).transform.GetChild(i).GetComponent<CSS_BossModules>());
            //Debug.Log("Module " + i + ": "+ this.modules[i].GetComponent<CSS_BossModules>().GetModHP());
        }

        // Prints out the number of modules inside the list
        //Debug.Log("Num of Modules: " + this.modules.Count);
    }


        public void SetMovementPattern(int _id, Transform[] _movePat)
    {
        this.bossID = _id;
        this.movementPattern = _movePat;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////
    ///Debug
    ///

    private void DebugIsTakeDamage()
    {
        if (this.isTakingDamage == true)
        {
            this.modules[0].TakeDamage(100);
            this.isTakingDamage = false;
        }
    }

}
