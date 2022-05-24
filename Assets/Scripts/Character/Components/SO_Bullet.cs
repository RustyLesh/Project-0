using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet", order = 20)]
public class SO_Bullet : ScriptableObject
{
    public int baseDamage;
    public Sprite sprite;
    public float speed;
    public int fireRate;
}
