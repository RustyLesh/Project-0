using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_ModuleTurretBase : CSS_BossModules
{
    [Header("Module Stat")]
    [SerializeField] private int defaultModHP;
    [SerializeField] private int ammo;
    [SerializeField] private int ammoCount;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float fireReload;
    [SerializeField] private float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        this.defaultModHP = 100;
        this.ammo = 6;
        this.ammoCount = this.ammo;
        this.fireSpeed = 3.0f;
        this.fireReload = this.fireSpeed;
        this.fireRate = 0.3f;
        this.Init(this.defaultModHP);
    }

    // Depending on how many differnt attacks there would be 
    // This might become a virtual func with this becoming a base class
    // for different derived turret weapon classes
    public void Shoot()
    {
        if (!this.GetIsDestroyed())
        {
            if(this.fireReload > 0.0f)
            {
                this.fireReload -= Time.deltaTime;
            }
            else
            {
                if(this.ammoCount > 0)
                {
                    //Debug.Log("Boss is Firing a bullet");
                    Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
                    GameObject newBullet = Instantiate(CSS_GameManager.Instance.bullet, spawnPosition, this.transform.rotation);
                    newBullet.GetComponent<BulletTest>().SetPlayerFired(false);

                    this.ammoCount--;
                    this.fireReload = this.fireRate;
                }
                else
                {
                    //Debug.Log("Boss is Reloading");
                    this.ammoCount = this.ammo;
                    this.fireReload = this.fireSpeed;
                }
            }

            //StartCoroutine(ShootingCoroutine());
        }
    }

    //private IEnumerator ShootingCoroutine()
    //{
    //    //for(int i = 0; i < this.ammo; i++)
    //    //{
    //    //    Debug.Log("Boss is Firing a bullet");
    //    //    yield return new WaitForSeconds(this.fireRate);
    //    //}
    //    Debug.Log("Boss is Firing a bullet");
    //    yield return new WaitForSeconds(this.fireSpeed);
    //}

}
