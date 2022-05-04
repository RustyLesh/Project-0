using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_GameManager : MonoBehaviour
{
    // Singleton
    public static CSS_GameManager Instance { get; private set; }

    [Header("References")]
    public GameObject playerShip;
    public GameObject bossShip;

    [Space]
    [Header("Game Points")]
    [SerializeField] private int playerCoins; // eg Get save or start from 0

    // level 1 Infomation can be seperated into another class
    [Space]
    [Header("Level 1 info")]
    public bool isBossSpawned = false;
    public bool isBossDead = false;
    public int spawnPattern = 1;
    public int spawnNumbers = 5;
    public float gameTimer = 0.0f;
    public float spawnTimer = 0.0f;
    public float spawnRate = 0.5f;
    public float cannonFodderTimer = 0.0f;
    public float lancerTimer = 0.0f;


    // event
    // to trigger to UI when boss is spawned
    public delegate void OnBossUpdate();
    public static event OnBossUpdate onBossUpdate;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameTimer += Time.deltaTime;
        this.Level01Update(this.gameTimer);

    }

    // level 1 Infomation can be seperated into another class
    // Current logic makes gameplay very repititive 
    public void Level01Update(float deltaTime)
    {
        // Total game length aim 2 - 5 mins
        // deltaTime avg 120 - 300 ticks

        // Spawning Mobs till boss arrives
        if(this.gameTimer <= 140.0f)
        {
            // Constantly spawn cannon fodders till boss
            // Spawn Basic Chargers aka cannon fodder
            this.cannonFodderTimer += Time.deltaTime;
            if (this.cannonFodderTimer >= 2.0f)
            {
                CSS_Spawn.Instance.SpawnChargerMob();
                this.cannonFodderTimer = 0.0f;
            }

            // Hit and Run Lancers that are beefy cannon fodders
            this.lancerTimer += Time.deltaTime;
            if(this.lancerTimer >= 30.0f)
            {
                CSS_Spawn.Instance.SpawnHitAndRunMob();
                this.lancerTimer = 0.0f;
            }

            // Set Pattern
            this.spawnTimer += Time.deltaTime;
            if(this.spawnTimer >= 20.0f)
            {
                if(this.spawnPattern == 1)
                {
                    CSS_Spawn.Instance.SpawnCurveMob(false);
                }
                else if (this.spawnPattern == 2)
                {
                    CSS_Spawn.Instance.SpawnCutterMob(true);
                }
                else if (this.spawnPattern == 3)
                {
                    CSS_Spawn.Instance.SpawnCurveMob(true);
                }
                else if (this.spawnPattern == 4)
                {
                    CSS_Spawn.Instance.SpawnCutterMob(false);
                }
                else if (this.spawnPattern == 5)
                {
                    CSS_Spawn.Instance.SpawnCurveMob();
                }
                else if (this.spawnPattern == 6)
                {
                    CSS_Spawn.Instance.SpawnCutterMob();
                }

                if(spawnNumbers != 0)
                {
                    this.spawnTimer = 20.0f - this.spawnRate;
                    this.spawnNumbers--;
                }
                else
                {
                    this.spawnTimer = 0.0f;
                    this.spawnNumbers = 5;
                    this.spawnPattern++;
                }
            }
        }
        else
        {
            if (!this.isBossSpawned)
            {
                bossShip = CSS_Spawn.Instance.SpawnBoss01();
                this.isBossSpawned = true;

                // send message that boss spawned
                onBossUpdate.Invoke();
            }
        }

    }
}
