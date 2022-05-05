using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Arsenal))]
public class PlayerShip : MonoBehaviour
{
    

    public bool PlayerShoot;
    public Health PlayerHealth { get; private set; }
    public Arsenal PlayerBattlements { get; private set; }
    public int coinCount { get; private set; }
    

    private PlayerControls playerControls;

    private float timer;

    [SerializeField]private float shootDelay = 0.7f;

    [SerializeField] private GameObject bulletPrefrab;

    [SerializeField] private Transform firePosition;

    

    float time = 0;

    private float nextShootTime = 0;

    [SerializeField] private float fireRate = 0.1f;


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


    void Update()
    {
        time += Time.deltaTime;

        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {

            timer += Time.deltaTime;
            if (timer > fireRate) {
                
               GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
               newBullet.transform.Rotate(new Vector3(0, 0, +90));
               newBullet.GetComponent<BulletTest>().SetPlayerFired(true);

                timer = 0;
                    }

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Coin") {
            coinCount++;
            Debug.Log("Player has: " + coinCount + " coins");
        }
    }

}
