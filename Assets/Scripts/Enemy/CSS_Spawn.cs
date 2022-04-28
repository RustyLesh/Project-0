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
    public GameObject obj_enemy01;

    [Space]
    [Header("Spawn Settings")]
    public float spawnTimer = 0.0f;
    public float spawnTime = 1.0f;

    [Space]
    //public List<Transform> defSpawnPos = new List<Transform>();
    public Transform[] defSpawnPos;
    public Transform[] atkRunMovePos;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.spawnTimer = this.spawnTime;
        this.SetDefaultSpawnPosition();
        this.SetAtkRun01();
    }

    // Update is called once per frame
    void Update()
    {
        this.spawnTimer -= Time.deltaTime;
        if(this.spawnTimer <= 0.0f)
        {
            // Spawn Simple Enemy
            GameObject tempEnemy = Instantiate(obj_enemy01, this.RandomSpawnPosition(), Quaternion.identity);
            tempEnemy.GetComponent<CSS_Enemy>().SetMovementID(0);

            // Spawn AtkRun Enemy
            GameObject tempEnemy02 = Instantiate(obj_enemy01, this.atkRunMovePos[0].position, Quaternion.identity);
            tempEnemy02.GetComponent<CSS_Enemy>().SetMovementID(1);

            if (tempEnemy02.GetComponent<CSS_Enemy>())
            {
                Debug.Log("Script is shooting");
            }

            // Reset Timer
            this.spawnTimer = this.spawnTime;
        }

    }

    void SetDefaultSpawnPosition()
    {
        //int listIndex = 0;
        //while (this.spawnPoints.transform.GetChild(0).GetChild(listIndex).transform)
        //{
        //    this.defSpawnPos.Add(this.spawnPoints.transform.GetChild(0).GetChild(listIndex).transform);
        //    listIndex++;
        //}

        int arraySize = this.spawnPoints.transform.GetChild(0).childCount;
        this.defSpawnPos = new Transform[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            this.defSpawnPos[i] = this.spawnPoints.transform.GetChild(0).GetChild(i).transform;
        }
    }

    Vector3 RandomSpawnPosition()
    {
        Vector3 tempVec;
        int randIndex = Random.Range(0, this.defSpawnPos.Length);
        tempVec = this.defSpawnPos[randIndex].position;

        return (tempVec);
    }

    void SetAtkRun01()
    {
        int arraySize = this.movementPoints.transform.GetChild(0).childCount;
        this.atkRunMovePos = new Transform[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
            this.atkRunMovePos[i] = this.movementPoints.transform.GetChild(0).GetChild(i).transform;
        }
    }



    /// Getters
    public Transform[] GetAtkRun01() { return (this.atkRunMovePos); }
}
