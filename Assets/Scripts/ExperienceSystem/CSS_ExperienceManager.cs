using System;
using UnityEngine;

public class CSS_ExperienceManager : MonoBehaviour, CSS_ISaveable {
    #region Singleton

    public static CSS_ExperienceManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one inventory instance found!");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [field: SerializeField] public int currentLevel { get; private set; } = 1;
    [field: SerializeField] public int maxLevel { get; private set; } = 100;
    [field: SerializeField] public int xp { get; private set; } = 0;
    [field: SerializeField] public int xpToNextLevel { get; private set; } = 100;

    public delegate void OnExperienceChanged();
    public static event OnExperienceChanged onExperienceChanged;

    private void OnEnable() {
        CSS_Enemy.OnAddXp += AddXp;
    }

    private void OnDisable() {
        CSS_Enemy.OnAddXp -= AddXp;
    }

    public void AddXp(int amount) {
        xp += amount;
        if (xp >= xpToNextLevel && currentLevel <= maxLevel) {
            currentLevel++;
            xp -= xpToNextLevel;
            xpToNextLevel = Convert.ToInt32(xpToNextLevel * 1.10);
        }
        Debug.Log("XP: " + xp);
        onExperienceChanged.Invoke();
    }

    public object SaveState() {
        return new SaveData() {
            currentLevel = this.currentLevel,
            maxLevel = this.maxLevel,
            xp = this.xp,
            xpToNextLevel = this.xpToNextLevel,
        };
    }

    public void LoadState(object state) {
        var saveData = (SaveData)state;
        currentLevel = saveData.currentLevel;
        maxLevel = saveData.maxLevel;
        xp = saveData.xp;
        xpToNextLevel = saveData.xpToNextLevel;

    }

    [Serializable]
    private struct SaveData {
        public int currentLevel;
        public int maxLevel;
        public int xp;
        public int xpToNextLevel;
    }
}

