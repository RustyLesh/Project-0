using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Battlements))]
public class PlayerShip : MonoBehaviour
{
    public Health PlayerHealth { get; private set; }
    public Battlements PlayerBattlements { get; private set; }
    private void Awake()
    {
        PlayerHealth = GetComponent<Health>();
        PlayerBattlements = GetComponent<Battlements>();
    }
}
