using NUnit.Framework;
using UnityEngine;

public class CSS_SpawnCoinTest {

    //Tests if coin spawns
    [Test]
    public void spawn_coin() {
        var gameObject = new GameObject();
        var spawnCoin = gameObject.AddComponent<CSS_SpawnCoin>();

        spawnCoin.SpawnCoinOnDeath(gameObject.transform);

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        Assert.AreEqual(true, coins.Length > 0);
    }

    //Tests is coin spawns right amount
    [Test]
    public void spawn_coin_amount() {
        int beforeCount = GameObject.FindGameObjectsWithTag("Coin").Length;

        var gameObject = new GameObject();
        var spawnCoin = gameObject.AddComponent<CSS_SpawnCoin>();

        spawnCoin.SpawnCoinOnDeath(gameObject.transform);

        int afterCount = GameObject.FindGameObjectsWithTag("Coin").Length;

        Assert.AreEqual(true, afterCount <= beforeCount + spawnCoin.maxCoinDrop && 
            afterCount >= beforeCount + spawnCoin.minCoinDrop);
    }
}
