using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Health), typeof(Arsenal))]
public class PlayerShip : MonoBehaviour, ISaveable
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

    public object SaveState() {
        return new SaveData() {
            coinCount = this.coinCount,
            shootDelay = this.shootDelay,
            fireRate = this.fireRate,
        };
    }

    public void LoadState(object state) {
        var saveData = (SaveData)state;
        coinCount = saveData.coinCount;
        shootDelay = saveData.shootDelay;
        fireRate = saveData.fireRate;
    }

    [Serializable]
    private struct SaveData {
        public int coinCount;
        public float shootDelay;
        public float fireRate;
    }

}
