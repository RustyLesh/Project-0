using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Arsenal))]
public class PlayerShip : MonoBehaviour
{
    public Health PlayerHealth { get; private set; }
    public Arsenal PlayerBattlements { get; private set; }

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
        PlayerBattlements = GetComponent<Arsenal>();
    }

    public void Shoot()
    {
            //#TODO replace with proper code to use weapons
            GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
       
    }

    private void Update()
    {
        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {
            Shoot(); //#TODO Use shoot from arsenal
        }
    }
}
