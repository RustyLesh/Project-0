using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_Spawn : MonoBehaviour
{
    // Singleton
    public static CSS_Spawn Instance { get; private set; }

    [Header("References")]
    public GameObject spawnPoints;
    public GameObject movementPoints;
    public GameObject bossWayPoints;
    public GameObject obj_enemy01;
    public GameObject obj_enemy02;
    public GameObject obj_enemy03;
    public GameObject obj_enemy04;
    public GameObject obj_enemy05;
    public GameObject obj_boss01;

    [Space]
    [Header("Spawn Settings")]
    public float spawnTimer = 0.0f;
    public float spawnTime = 1.0f;

    [Space]
    [Header("Spawn Position Info")]
    public Transform[] defSpawnPos;
    private Transform bossSpawnPos;
    public List<List<Transform>> bossMovementPattern = new List<List<Transform>>();
    public List<List<Transform>> atkRunMovePos = new List<List<Transform>>();

    [Space]
    [Header("Debug Functions")]
    [SerializeField] private bool isSpawnMob01 = false; // Charger
    [SerializeField] private bool isSpawnMob02 = false; // Sneaky curve
    [SerializeField] private bool isSpawnMob03 = false; // Bulk fire and run
    [SerializeField] private bool isSpawnMob04 = false; // Cut 
    [SerializeField] private bool isSpawnBoss01 = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.spawnTimer = this.spawnTime;
        this.SetDefaultSpawnPosition();
        this.SetBossSpawnPoint();
        this.SetBossMovementPattern();
        this.SetAtkRunPos();
    }

    // Update is called once per frame
    void Update()
    {
        //this.spawnTimer -= Time.deltaTime;
        //if(this.spawnTimer <= 0.0f)
        //{

        //    //this.SpawnCurveMob(true);
        //    //this.SpawnHitAndRunMob(true);
        //    //this.SpawnCutterMob(true);

        //    // Reset Timer
        //    this.spawnTimer = this.spawnTime;
        //}

        // Debug Functions
        this.DebugIsSpawnMob01();
        this.DebugIsSpawnMob02();
        this.DebugIsSpawnMob03();
        this.DebugIsSpawnMob04();
        this.DebugIsSpawnBoss01();
    }

    public void SpawnChargerMob()
    {
        // Spawn Map Charging Enemies 
        GameObject tempEnemy = Instantiate(obj_enemy01, this.RandomSpawnPosition(), Quaternion.identity);
        tempEnemy.GetComponent<CSS_Enemy>().SetMovementID(0);
    }

    // Spawn both left and right
    public void SpawnCurveMob()
    {
        GameObject tempEnemy03 = Instantiate(obj_enemy02, this.atkRunMovePos[0][this.atkRunMovePos[0].Count - 1].position, Quaternion.identity);
        tempEnemy03.GetComponent<CSS_Enemy>().SetIsRightSide(true);
        tempEnemy03.GetComponent<CSS_Enemy>().SetMovementID(1);
        tempEnemy03.GetComponent<CSS_Enemy>().SetWaypointPos(this.atkRunMovePos[0].Count - 2);

        GameObject tempEnemy02 = Instantiate(obj_enemy02, this.atkRunMovePos[0][0].position, Quaternion.identity);
        tempEnemy02.GetComponent<CSS_Enemy>().SetMovementID(1);
    }

    public void SpawnCurveMob(bool isRightSide)
    {
        // Spawn AtkRun01 Enemy
        if (isRightSide)
        {
            GameObject tempEnemy03 = Instantiate(obj_enemy02, this.atkRunMovePos[0][this.atkRunMovePos[0].Count - 1].position, Quaternion.identity);
            tempEnemy03.GetComponent<CSS_Enemy>().SetIsRightSide(isRightSide);
            tempEnemy03.GetComponent<CSS_Enemy>().SetMovementID(1);
            tempEnemy03.GetComponent<CSS_Enemy>().SetWaypointPos(this.atkRunMovePos[0].Count - 2);
        }
        else
        {
            GameObject tempEnemy02 = Instantiate(obj_enemy02, this.atkRunMovePos[0][0].position, Quaternion.identity);
            tempEnemy02.GetComponent<CSS_Enemy>().SetMovementID(1);
        }
    }

    // Spawn both left and right 
    public void SpawnHitAndRunMob()
    {
        // Spawn Right
        GameObject tempEnemy06 = Instantiate(obj_enemy03, this.atkRunMovePos[1][2].position, Quaternion.identity);
        tempEnemy06.GetComponent<CSS_Enemy>().SetIsRightSide(true);
        tempEnemy06.GetComponent<CSS_Enemy>().SetMovementID(2);

        // Spawn Left
        GameObject tempEnemy07 = Instantiate(obj_enemy03, this.atkRunMovePos[1][0].position, Quaternion.identity);
        tempEnemy07.GetComponent<CSS_Enemy>().SetMovementID(2);
    }

    public void SpawnHitAndRunMob(bool isRightSide)
    {
        // Spawn AtkRun02 Enemy
        if (isRightSide)
        {
            GameObject tempEnemy06 = Instantiate(obj_enemy03, this.atkRunMovePos[1][2].position, Quaternion.identity);
            tempEnemy06.GetComponent<CSS_Enemy>().SetIsRightSide(isRightSide);
            tempEnemy06.GetComponent<CSS_Enemy>().SetMovementID(2);
            //tempEnemy06.GetComponent<CSS_Enemy>().SetWaypointPos(3);
        }
        else
        {         
            GameObject tempEnemy06 = Instantiate(obj_enemy03, this.atkRunMovePos[1][0].position, Quaternion.identity);
            tempEnemy06.GetComponent<CSS_Enemy>().SetMovementID(2);
        }
    }

    public void SpawnCutterMob()
    {
        GameObject tempEnemy05 = Instantiate(obj_enemy05, this.atkRunMovePos[2][2].position, Quaternion.identity);
        tempEnemy05.GetComponent<CSS_Enemy>().SetIsRightSide(true);
        tempEnemy05.GetComponent<CSS_Enemy>().SetMovementID(3);

        GameObject tempEnemy04 = Instantiate(obj_enemy05, this.atkRunMovePos[2][0].position, Quaternion.identity);
        tempEnemy04.GetComponent<CSS_Enemy>().SetMovementID(3);
    }

    public void SpawnCutterMob(bool isRightSide)
    {
        // Spawn AtkRun03 Enemy
        if (isRightSide)
        {
            GameObject tempEnemy05 = Instantiate(obj_enemy05, this.atkRunMovePos[2][2].position, Quaternion.identity);
            tempEnemy05.GetComponent<CSS_Enemy>().SetIsRightSide(isRightSide);
            tempEnemy05.GetComponent<CSS_Enemy>().SetMovementID(3);
        }
        else
        {
            GameObject tempEnemy04 = Instantiate(obj_enemy05, this.atkRunMovePos[2][0].position, Quaternion.identity);
            tempEnemy04.GetComponent<CSS_Enemy>().SetMovementID(3);
        }
    }

    public GameObject SpawnBoss01()
    {
        GameObject tempBoss01 = Instantiate(obj_boss01, this.bossSpawnPos.position, Quaternion.identity);
        tempBoss01.GetComponent<CSS_Boss>().SetMovementPattern(1, this.GetBossMovementPattern(0));

        return tempBoss01;
    }

    private void SetDefaultSpawnPosition()
    {
        int arraySize = this.spawnPoints.transform.GetChild(0).childCount;
        this.defSpawnPos = new Transform[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            this.defSpawnPos[i] = this.spawnPoints.transform.GetChild(0).GetChild(i).transform;
        }
    }

    private void SetBossSpawnPoint()
    {
        this.bossSpawnPos = this.spawnPoints.transform.GetChild(1).transform;
    }

    private void SetBossMovementPattern()
    {
        for (int i = 0; i < this.bossWayPoints.transform.childCount; i++)
        {
            int arraySize = this.bossWayPoints.transform.GetChild(i).childCount;

            this.bossMovementPattern.Add(new List<Transform>());
            for (int j = 0; j < arraySize; j++)
            {
                this.bossMovementPattern[i].Add(this.bossWayPoints.transform.GetChild(i).GetChild(j).transform);
            }
        }
    }

    private void SetAtkRunPos()
    {
        for (int i = 0; i < this.movementPoints.transform.childCount; i++)
        {
            int arraySize = this.movementPoints.transform.GetChild(i).childCount;

            this.atkRunMovePos.Add(new List<Transform>());
            for(int j = 0; j < arraySize; j++)
            {
                this.atkRunMovePos[i].Add(this.movementPoints.transform.GetChild(i).GetChild(j).transform);
            }

            // Prints out the number of patterns inside the list
            // Debug.Log("Movement PAttern: " + this.atkRunMovePos[i]);
        }
    }

    Vector3 RandomSpawnPosition()
    {
        Vector3 tempVec;
        int randIndex = Random.Range(0, this.defSpawnPos.Length);
        tempVec = this.defSpawnPos[randIndex].position;

        return (tempVec);
    }

    /// Getters
    public Transform[] GetAtkRun(int index) {

        Transform[] temp = new Transform[this.atkRunMovePos[index].Count];

        for(int i = 0; i < temp.Length; i++)
        {
            temp[i] = this.atkRunMovePos[index][i];
        }
        
        return (temp); 
    }

    public Transform[] GetBossMovementPattern(int index) {

        Transform[] temp = new Transform[this.bossMovementPattern[index].Count];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = this.bossMovementPattern[index][i];
        }

        return (temp);
    }

    /// Debug Functions
    private void DebugIsSpawnMob01()
    {
        if(this.isSpawnMob01 == true)
        {
            // Spawn Simple Enemy
            GameObject tempEnemy = Instantiate(obj_enemy01, this.RandomSpawnPosition(), Quaternion.identity);
            tempEnemy.GetComponent<CSS_Enemy>().SetMovementID(0);
            this.isSpawnMob01 = false;
        }
    }

    private void DebugIsSpawnMob02()
    {
        if (this.isSpawnMob02 == true)
        {
            // Spawn AtkRun01 Enemy
            GameObject tempEnemy02 = Instantiate(obj_enemy02, this.atkRunMovePos[0][0].position, Quaternion.identity);
            tempEnemy02.GetComponent<CSS_Enemy>().SetMovementID(1);
            this.isSpawnMob02 = false;
        }
    }

    private void DebugIsSpawnMob03()
    {
        if (this.isSpawnMob03 == true)
        {
            // Spawn AtkRun02 Enemy
            GameObject tempEnemy06 = Instantiate(obj_enemy03, this.atkRunMovePos[1][0].position, Quaternion.identity);
            tempEnemy06.GetComponent<CSS_Enemy>().SetMovementID(2);
            this.isSpawnMob03 = false;
        }
    }

    private void DebugIsSpawnMob04()
    {
        if (this.isSpawnMob04 == true)
        {
            // Spawn AtkRun03 Enemy
            GameObject tempEnemy04 = Instantiate(obj_enemy05, this.atkRunMovePos[2][0].position, Quaternion.identity);
            tempEnemy04.GetComponent<CSS_Enemy>().SetMovementID(3);
            this.isSpawnMob04 = false;
        }
    }

    private void DebugIsSpawnBoss01()
    {
        if (this.isSpawnBoss01 == true)
        {
            GameObject tempBoss01 = Instantiate(obj_boss01, this.bossSpawnPos.position, Quaternion.identity);
            tempBoss01.GetComponent<CSS_Boss>().SetMovementPattern(1,this.GetBossMovementPattern(0));

            this.isSpawnBoss01 = false;
        }
    }

}
