using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet", order = 20)]
public class Bullet : ScriptableObject
{
    [SerializeField] private float baseDamage;
    [SerializeField] private Texture2D sprite;
}
