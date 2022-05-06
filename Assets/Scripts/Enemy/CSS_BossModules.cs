using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CSS_BossModules : MonoBehaviour
{
    [Header("Module Stat")]
    [SerializeField] private bool isDestroyed;
    [SerializeField] private int modHP;

    public void Init(int _modHP)
    {
        this.modHP = _modHP;
    }

    public void TakeDamage(int _dmg)
    {
        // Double parent due to gameobject structure
        // Modules only take damge when boss is in engagement mode
        if(this.transform.parent.parent.gameObject.GetComponent<CSS_Boss>().state == CSS_Boss.EBossState.Engagement)
        {
            this.modHP -= _dmg;

            if (this.modHP <= 0)
            {
                this.ModuleDestroyed();
            }
        }       
    }

    // Changing object component sprite to destory sprite
    // and activate any other VFX from here without deleting.
    // Most likely to be turned into virtual
    public void ModuleDestroyed()
    {
        this.isDestroyed = true;
        // Ping to Boss script to check other modules for death and endgame
        this.transform.parent.parent.gameObject.GetComponent<CSS_Boss>().CheckModules();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// Encapsulators
    /// 
    public void SetIsDestroyed(bool _isDestroyed)
    {
        this.isDestroyed = _isDestroyed;
    }

    public void SetModHP(int _hp)
    {
        this.modHP = _hp;
    }

    public bool GetIsDestroyed() { return (this.isDestroyed); }
    public int GetModHP() { return (this.modHP); }
}
