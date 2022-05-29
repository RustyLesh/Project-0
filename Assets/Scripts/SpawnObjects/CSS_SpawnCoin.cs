using UnityEngine;

public class CSS_SpawnCoin : MonoBehaviour {
    [SerializeField] GameObject coin;
    [SerializeField] public float radius = 0.5f;
    [SerializeField] public int minCoinDrop = 2;
    [SerializeField] public int maxCoinDrop = 3;

    public void SpawnCoinOnDeath(Transform enemyTransform) {
        System.Random r = new System.Random();
        int spawnNum = r.Next(minCoinDrop, maxCoinDrop);
       
        //Loop until enough coin has spawned
        for (int i = 0; i <= spawnNum; i++) {
            //Get random Vector3 within a radius
            Vector3 pos = enemyTransform.position + (Vector3)Random.insideUnitCircle * radius;
            //Creates a clone of Coin
            Instantiate(coin, pos, Quaternion.identity);
        }
    }
}
