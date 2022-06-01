using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Project0;

[RequireComponent(typeof(CSS_Health))]
public class CSS_PlayerShip : MonoBehaviour, CSS_ISaveable
{
        //float bulletFireRate = bulletData.fireRate;
    public int coinCount { get; private set; }
    public bool playerShoot;
    public CSS_Health playerHealth { get; private set; }

    private PlayerControls playerControls;
    private float timer;
    [SerializeField] private SO_Bullet bulletData;

    [SerializeField] private float shootDelay = 0.7f;
    [SerializeField] private GameObject bulletPrefrab;
    [SerializeField] private Transform firePosition;
    [SerializeField] private float fireRate = 0.1f;

    //Dynamic difficulty multipliers
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float healthMultplier;

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

    public void AdjustMaxHealthMultiplier(float value)
    {
        playerHealth.SetMaxHealth(Mathf.Clamp(playerHealth.GetMaxHealth() * value, 1, float.MaxValue));
    }

    public void AdjustDamageMultiplier(float value)
    {
        bulletData.baseDamage = (Mathf.Clamp((int)(bulletData.baseDamage * value), 1, int.MaxValue));
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
