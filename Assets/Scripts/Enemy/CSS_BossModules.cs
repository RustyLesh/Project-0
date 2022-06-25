using System;
using UnityEngine;

public abstract class CSS_BossModules : MonoBehaviour
{
    [Header("Module Stat")]
    [SerializeField] private bool isDestroyed;
    [SerializeField] private int modHP;

    [SerializeField] private SpriteRenderer sprRen;
    [SerializeField] private ParticleSystem smokePart;
    [SerializeField] private Sprite sprDestroyedVer;

    public BossModuleType moduleType { get; protected set; }

    public void Init(int _modHP)
    {
        this.modHP = _modHP;
        this.sprRen = this.GetComponent<SpriteRenderer>();
        this.smokePart = this.transform.GetChild(0).GetComponent<ParticleSystem>(); // 0 for smoke particles
    }

    public void TakeDamage(int _dmg)
    {
        // Modules only take damge when boss is in engagement mode
        if(this.transform.parent.parent.gameObject.GetComponent<CSS_Boss>().state == CSS_Boss.EBossState.Engagement &&
            this.modHP > 0)
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

        // change visuals
        if(this.sprDestroyedVer != null)
        {
            this.sprRen.sprite = this.sprDestroyedVer;
        }
        else
        {
            // Debug log isnt realy needed here as error msg should also 
            // say the same thing. Can be commented out as theres a null 
            // check anyway.
            Debug.Log("Destoryed Sprite version not set");
        }
        this.smokePart.Play();

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
        //this.modHP = Mathf.Clamp(_hp, 0, int.MaxValue);
    }

    public bool GetIsDestroyed() { return (this.isDestroyed); }
    public int GetModHP() { return (this.modHP); }
}
public enum BossModuleType
{
    CORE,
    TURRET,
}

