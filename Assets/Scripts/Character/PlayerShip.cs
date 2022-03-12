using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Battlements))]
public class PlayerShip : MonoBehaviour
{
    public Health PlayerHealth { get; private set; }
    public Battlements PlayerBattlements { get; private set; }

    private PlayerControls playerControls;

    private float bulletSpeed = 5;

    [SerializeField]private GameObject bulletPrefrab;

    [SerializeField] private Transform firePosition;
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
        PlayerBattlements = GetComponent<Battlements>();
    }

    public void Shoot()
    {
            Debug.Log("OI"); //#TODO replace with proper code to use weapon and bullet variables
            GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        //foreach (Gun gun in PlayerBattlements.guns)
        //{
        //    GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
        //    newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        //}
        //#TODO This is temp for testing. the bullet itself should change the speed as to not call get component everyframe for every bullet.
    }

    private void Update()
    {
        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {
            Shoot();
        }
    }
}
