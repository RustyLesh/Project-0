/*
 * Author: Luke Jordens
 * This class holds all the data related to the different types of bullets available in the game
*/
using Project0.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet", order = 20)]
public class SO_Bullet : SO_InventoryItem
{
    public int baseDamage;
    public Sprite sprite;
    public float speed;
    public float fireRate;
    public bool burst = false;
    public int timesToShoot = 3; 
}
