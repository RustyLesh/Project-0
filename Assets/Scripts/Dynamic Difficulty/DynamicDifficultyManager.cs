using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficultyManager : MonoBehaviour
{
    private PlayerShip playerShip;
    private CSS_GameManager gameManager;
    private MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        playerShip = GetComponent<PlayerShip>();
        gameManager = CSS_GameManager.Instance;
        moneyManager = MoneyManager.Instance;
    }
}
