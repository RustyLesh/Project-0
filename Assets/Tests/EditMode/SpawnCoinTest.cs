using NUnit.Framework;
using UnityEngine;
using Project0;

public class SpawnCoinTest {

    [Test]
    public void spawn_coin() {
        var gameObject = new GameObject();
        var spawnCoin = gameObject.AddComponent<SpawnCoin>();

        spawnCoin.SpawnCoinOnDeath(gameObject.transform);

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        Assert.AreEqual(true, coins.Length > 0);
    }

    [Test]
    public void spawn_coin_amount() {
        int beforeCount = GameObject.FindGameObjectsWithTag("Coin").Length;

        var gameObject = new GameObject();
        var spawnCoin = gameObject.AddComponent<SpawnCoin>();

        spawnCoin.SpawnCoinOnDeath(gameObject.transform);

        int afterCount = GameObject.FindGameObjectsWithTag("Coin").Length;

        Assert.AreEqual(true, afterCount <= beforeCount + spawnCoin.maxCoinDrop && 
            afterCount >= beforeCount + spawnCoin.minCoinDrop);
    }
}
