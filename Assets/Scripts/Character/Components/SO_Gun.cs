using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Gun", order = 10)]
public class SO_Gun : Item
{
    [SerializeField] private SO_Bullet bullet;
    [SerializeField] private Texture2D sprite;
    [SerializeField] private int projectileCount;
    public float fireRate;

#region Public Getters

    public SO_Bullet Bullet
    {
        private set => bullet = value;
        get => bullet;
    }
    public Texture2D Sprite
    {
        private set => sprite = value;
        get => sprite;
    }
    public int ProjectileCount
    {
        private set => projectileCount = value;
        get => projectileCount;
    }
    public float FireRate
    {
        private set => fireRate = value;
        get => fireRate;
    }
#endregion

}
