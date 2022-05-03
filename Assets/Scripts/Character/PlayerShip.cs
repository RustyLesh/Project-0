using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Arsenal))]
public class PlayerShip : MonoBehaviour
{
    public Health PlayerHealth { get; private set; }
    public Arsenal PlayerBattlements { get; private set; }
    public int coinCount { get; private set; }
    

    private PlayerControls playerControls;

    private float bulletSpeed = 5;

    [SerializeField]private float shootDelay = 0.7f;

    [SerializeField] private GameObject bulletPrefrab;

    [SerializeField] private Transform firePosition;

    float time = 0;

    private float nextShootTime = 0;
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
        coinCount = 0;
    }

    public void Shoot()
    {
        if (time > nextShootTime)
        {
            nextShootTime = (time + shootDelay);
            //TODO: replace with proper code to use weapons
            GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {
            Shoot(); //TODO: Use shoot from arsenal
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Coin") {
            coinCount++;
            Debug.Log("Player has: " + coinCount + " coins");
        }
    }
}
