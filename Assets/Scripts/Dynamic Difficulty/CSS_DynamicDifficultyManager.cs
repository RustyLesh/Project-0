using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_DynamicDifficultyManager : MonoBehaviour
{
    private CSS_PlayerShip playerShip;
    private CSS_GameManager gameManager;
    private CSS_MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GetComponent<CSS_PlayerShip>();
        gameManager = CSS_GameManager.Instance;
        moneyManager = CSS_MoneyManager.Instance;
    }
}
