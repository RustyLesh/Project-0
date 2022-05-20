using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_DynamicDifficultyManager : MonoBehaviour
{
    private CSS_PlayerShip playerShip;
    private CSS_GameManager gameManager;
    private float moneyMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        moneyMultiplier = CSS_MoneyManager.Instance.moneyMultiplier;
    }

}
