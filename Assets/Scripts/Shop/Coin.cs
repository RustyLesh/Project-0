using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int Value = 1;

    public void GhangeValue(int newValue)
    {
        Value = newValue;
    }

    public int GetValue()
    {
        return Value;
    }
}
