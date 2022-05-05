using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Arsenal))]
public class PlayerShip : MonoBehaviour
{
    

    public bool PlayerShoot;
    public Health PlayerHealth { get; private set; }
    public Arsenal PlayerBattlements { get; private set; }

    private PlayerControls playerControls;

    private float timer;

    [SerializeField]private float shootDelay = 0.7f;

    [SerializeField] private GameObject bulletPrefrab;

    [SerializeField] private Transform firePosition;

    

    float time = 0;

    private float nextShootTime = 0;

    [SerializeField] private float fireRate = 0.1f;

    //private float bulletSpeed = 20f;

    //[SerializeField]private GameObject bulletPrefrab;

    //[SerializeField] private Transform firePosition;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        PlayerHealth = GetComponent<Health>();
        PlayerBattlements = GetComponent<Arsenal>();
    }

    /*public void Shoot()
    {
        if (time > nextShootTime)
        {
            nextShootTime = (time + shootDelay);
            //TODO: replace with proper code to use weapons
            GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

        }
    }

       
    }*/


    void Update()
    {
        time += Time.deltaTime;

        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {

            timer += Time.deltaTime;
            if (timer > fireRate) {
                
               GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
                //PlayerBattlements.Shoot();
                //PlayerShoot = true;
                //newBullet.GetComponent<BulletTest>().SetPlayerFired();

                timer = 0;
                    }

        }
        
    }

    

}
