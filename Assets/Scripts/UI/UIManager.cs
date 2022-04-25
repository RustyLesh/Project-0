using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject startScreen, gameScreen, endScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
