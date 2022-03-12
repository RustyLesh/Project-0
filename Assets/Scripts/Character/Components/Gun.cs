using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun", order = 10)]
public class Gun : ScriptableObject
{
    [SerializeField] string weaponName;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Texture2D sprite;
    [SerializeField] private int projectileCount;

}
