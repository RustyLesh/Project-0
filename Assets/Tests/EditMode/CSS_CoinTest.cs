using NUnit.Framework;
using UnityEngine;
using Project0;

public class CSS_CoinTest {
    
    //Tests if coin moves downwards after spawning
    [Test]
    public void coin_moving_downwards() {
        var gameObject = new GameObject();
        var coin = gameObject.AddComponent<Coin>();
        coin.transform.position = new Vector3(0, 0, 0);

        coin.MoveDownwards();
        Assert.AreNotEqual(new Vector3(0, 0, 0), coin.transform.position);
    }
}
