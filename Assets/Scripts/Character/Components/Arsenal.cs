using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour { 

    private float bulletSpeed = 20f;

    [SerializeField]private GameObject bulletPrefrab;

    [SerializeField] private Transform firePosition;
    GameObject newBullet;
    

    public Gun[] Guns { get; private set; }

    
    
}
