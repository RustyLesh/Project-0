using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{

    [SerializeField] Slider playerHPBar;
    void Start()
    {
        playerHPBar.value = FindObjectOfType<PlayerShip>().PlayerHealth.GetHealth();
    }

    void Update()
    {
        playerHPBar.value = FindObjectOfType<PlayerShip>().PlayerHealth.GetHealth();
    }
}
