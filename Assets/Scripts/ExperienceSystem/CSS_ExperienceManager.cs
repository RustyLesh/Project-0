using System;
using UnityEngine;

public class CSS_ExperienceManager : MonoBehaviour {

    [field: SerializeField] public int currenLevel { get; private set; } = 0;
    [field: SerializeField] public int maxLevel { get; private set; } = 100;
    [field: SerializeField] public int xp { get; private set; } = 0;
    [field: SerializeField] public int xpToNextLevel { get; private set; } = 100;
    [field: SerializeField] public int statPoint { get; private set; } = 0;

    private void OnEnable() {
        CSS_Enemy.OnAddXp += AddXp;
    }

    private void OnDisable() {
        CSS_Enemy.OnAddXp -= AddXp;
    }

    public void AddXp(int amount) {
        xp += amount;
        if (xp >= xpToNextLevel && currenLevel <= maxLevel) {
            currenLevel++;
            xp -= xpToNextLevel;
            xpToNextLevel = Convert.ToInt32(xpToNextLevel * 1.10);
            statPoint++;
        }
        Debug.Log("XP: " + xp);
    }
}

