using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CSS_Health))]


public class CSS_PlayerShip : MonoBehaviour, CSS_ISaveable
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
    CSS_AudioPlayer audioPlayer;


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
        audioPlayer = FindObjectOfType<CSS_AudioPlayer>();

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
                    StartCoroutine(FireBurst());


                }

                else
                {
                    GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
                    newBullet.GetComponent<CSS_Bullet>().SetPlayerFired(true);
                    audioPlayer.PlayShootingClip();
                    

                }

                timer = 0;
            }
        }
    }

    public IEnumerator FireBurst()
    {
        
        for (int i = 0; i < 3; i++)
        {   
                
            GameObject newBullet = Instantiate(bulletPrefrab, firePosition.position, firePosition.rotation);
            newBullet.GetComponent<CSS_Bullet>().SetPlayerFired(true);
            audioPlayer.PlayShootingClip();
           
            yield return new WaitForSeconds(0.1f);
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
