using NUnit.Framework;
using UnityEngine;
using Project0;

public class CoinTest {
    
    [Test]
    public void coin_moving_downwards() {
        var gameObject = new GameObject();
        var coin = gameObject.AddComponent<Coin>();
        coin.transform.position = new Vector3(0, 0, 0);

        coin.MoveDownwards();
        Assert.AreNotEqual(new Vector3(0, 0, 0), coin.transform.position);
    }

    [Test]
    public void coin_out_of_view() {
        var gameObject = new GameObject();
        var spawnCoin = gameObject.AddComponent<SpawnCoin>();
        spawnCoin.SpawnCoinOnDeath(gameObject.transform);

        Coin coin = GameObject.FindObjectOfType<Coin>();

        coin.transform.position = new Vector3(1000, 1000, 0);

        Assert.AreEqual(false, coin.IsInView());
    }
}
