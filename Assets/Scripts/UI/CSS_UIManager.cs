/*
 * Author: Peter An
 * 
 * This will manage which canvas will be displayed to the user.
 * each screen will be stored here and will only show the screen needed for
 * the scene.
 * 
 * This uses the singleton pattern so that only
 * one UIManager exists in the scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSS_UIManager : MonoBehaviour
{
    public static CSS_UIManager instance;

    // All the screens
    [SerializeField] GameObject startScreen, gameScreen, endScreen;

    void Awake()
    {
        // Initialize when no UIManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // Destory any other duplicates
        {
            Destroy(gameObject);
        }
    }
}
