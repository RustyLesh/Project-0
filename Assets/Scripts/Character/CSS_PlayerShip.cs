using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CSS_Health))]


public class CSS_PlayerShip : MonoBehaviour
{
    public bool playerShoot;
    public CSS_Health playerHealth { get; private set; }

    [SerializeField] private SO_Bullet bulletData;
    public int coinCount { get; private set; }

        //float bulletFireRate = bulletData.fireRate;

        private PlayerControls playerControls;
    private float timer;

    [SerializeField] private float shootDelay = 0.7f;
    [SerializeField] private GameObject bulletPrefrab;
    [SerializeField] private Transform firePosition;
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
        playerHealth = GetComponent<CSS_Health>();

        coinCount = 0;

    }

    void Update()
    {
        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {

            timer += Time.deltaTime;
            if (timer > bulletData.fireRate) {

                if (bulletData.burst == true)
                {
                    for (int TimesShot = 1; TimesShot <= bulletData.timesToShoot; TimesShot++)
                    {
                        GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
                        newBullet.GetComponent<CSS_Bullet>().SetPlayerFired(true);
                        new WaitForSeconds(100f / bulletData.fireRate);
                    }
                }

                else
                {
                    GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
                    newBullet.GetComponent<CSS_Bullet>().SetPlayerFired(true);
                }

                timer = 0;
            }
        }
    }

    public object SaveState() {
        return new SaveData() {
            shootDelay = this.shootDelay,
            fireRate = this.fireRate,
        };
    }

    public void LoadState(object state) {
        var saveData = (SaveData)state;
        shootDelay = saveData.shootDelay;
        fireRate = saveData.fireRate;
    }

    [Serializable]
    private struct SaveData {
        public float shootDelay;
        public float fireRate;
    }

    public void DisablePlayer()
    {
        gameObject.SetActive(false);
    }

    public void EnablePlayer()
    {
        gameObject.SetActive(true);
    }
}
