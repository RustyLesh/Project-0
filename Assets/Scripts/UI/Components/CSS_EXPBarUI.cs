/*
 * Author: Peter An
 * 
 * UI Class to update the EXPBar on the UI
 * The information is from the 
 * experienceManager.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CSS_EXPBarUI : MonoBehaviour
{
    CSS_ExperienceManager experienceManager = CSS_ExperienceManager.Instance;

    [SerializeField] TMP_Text levelText;
    [SerializeField] Slider expBar;

    void Start()
    {
        experienceManager = CSS_ExperienceManager.Instance;

        RefreshUI();
    }

    // Subscribe to listners (observers)
    void OnEnable()
    {
        CSS_ExperienceManager.onExperienceChanged += RefreshUI;
    }

    void OnDisable()
    {
        CSS_ExperienceManager.onExperienceChanged -= RefreshUI;
    }

    //void Update()
    //{
    //    RefreshUI();
    //}

    void RefreshUI()
    {
        expBar.maxValue = experienceManager.xpToNextLevel;
        expBar.value = experienceManager.xp;

        levelText.text = $"Level: {experienceManager.currentLevel}";
    }
}
