using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project0;


public class MoneyManager : MonoBehaviour
{
    [SerializeField]Wallet wallet;

    void Start()
    {
        wallet = new Wallet();
    }

    public Wallet PlayerWallet
    {
        private set => wallet = value;
        get => wallet;
    }
}
