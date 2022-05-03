using UnityEngine;

public class SpawnCoin : MonoBehaviour {
    [SerializeField] GameObject coin;
    [SerializeField] float radius = 0.5f;
    [SerializeField] int minCoinDrop = 2;
    [SerializeField] int maxCoinDrop = 3;

    public void SpawnCoinOnDeath(Transform enemyTransform) {
        System.Random r = new System.Random();
        int spawnNum = r.Next(minCoinDrop, maxCoinDrop);
       
        //Loop until enough coin has spawned
        for (int i = 0; i <= spawnNum; i++) {
            //Get random Vector3 within a radius
            Vector3 pos = new Vector3(enemyTransform.position.x + Random.insideUnitCircle.x,
                enemyTransform.position.y + Random.insideUnitCircle.y, 0);
            //Creates a clone of Coin
            Instantiate(coin, pos, Quaternion.identity);
        }
    }
}
