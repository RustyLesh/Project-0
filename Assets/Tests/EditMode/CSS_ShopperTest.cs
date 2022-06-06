using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
/*
 * 
 * Author: Peter An
 * 
 * This is a Unit Test for the Shopper and Shop class
 * to test if the shop actually attaches to the shopper
 * so the UI can display the shop
 * 
 */

using UnityEngine;
using UnityEngine.TestTools;

using Project0.Shops;
using Project0.Inventories;

public class CSS_ShopperTest
{

    [Test]
    public void TestShopAttachmentToShopper()
    {
        // create a shopper
        var shopperObject = new GameObject();
        var shopper = shopperObject.AddComponent<CSS_Shopper>();

        // create a shop
        var shopObject = new GameObject();
        var shop = shopObject.AddComponent<CSS_Shop>();

        // set the shopper a shop
        shopper.SetActiveShop(shop);

        // expected the same shop to be set
        var expected = shop;

        // actual shop from the getter
        var actual = shopper.GetActiveShop();

        // Test if true
        Assert.AreEqual(expected, actual);
    }
}
