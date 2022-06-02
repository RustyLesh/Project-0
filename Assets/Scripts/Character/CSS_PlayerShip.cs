/*
 * Author: Luke Jordens, Les McIlroy
 * This class mainly deals with the playership spawning, dying, saving and shooting.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Project0;

[RequireComponent(typeof(CSS_Health))]
public class CSS_PlayerShip : MonoBehaviour, CSS_ISaveable
{
    // Collectsb 
    public int coinCount { get; private set; } 

    // Variable referencing the player health
    public CSS_Health playerHealth { get; private set; }

    // Variable referencing the player controls 
    private PlayerControls playerControls;
    
    // Variables with information regarding bullet instantiation 
    [SerializeField] private float shootDelay = 0.7f;
    [SerializeField] private GameObject bulletPrefrab;
    [SerializeField] private Transform firePosition;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private SO_Bullet bulletData;
    private float timer;

    // Variable referencing audio clips for firing
    CSS_AudioPlayer audioPlayer;

    //Dynamic difficulty multipliers
    [SerializeField] private float damageMultiplier;
    [SerializeField] private float healthMultplier;

    private void OnEnable()
    {
        playerControls.Enable(); // Enables player controls
    }

    private void OnDisable()
    {
        playerControls.Disable(); // Disables player controls
    }

    // Instantiates variables on awake
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerHealth = GetComponent<CSS_Health>();
        audioPlayer = FindObjectOfType<CSS_AudioPlayer>();
        coinCount = 0;

    }

    void Update()
    {
        InstantiateBullet(); // Calls instantiate bullet every frame
    }

    // Instantiates a bullet at a fire position for both burst and normal fire bullets
    private void InstantiateBullet()
    {
        if (playerControls.PlayerShipControls.Shoot.ReadValue<float>() > 0)
        {
            timer += Time.deltaTime;
            if (timer > bulletData.fireRate)
            {

                if (bulletData.burst == true)
                {
                    StartCoroutine(FireBurst()); // Calls the coroutine that handles the burst fire
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

    // Coroutine that handles the fire burst
    private IEnumerator FireBurst()
    { 
        for (int i = 0; i < bulletData.timesToShoot; i++)
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

    // Disables player movement 
    public void DisablePlayer()
    {
        gameObject.SetActive(false);
        audioPlayer.PlayExplosionClip();
    }

    // Enables player movement 
    public void EnablePlayer()
    {
        gameObject.SetActive(true);
    }
}
