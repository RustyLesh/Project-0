using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project0
{
    public class DynamicDifficultyDrop : MonoBehaviour
    {
        [SerializeField] private float moneyMultiplyamount;

        [Header("Boss")]
        [SerializeField] private float bossDamageMultiplyAmount;
        [SerializeField] private float bossHealthMultiplyAmount;

        [Header("Mob")]
        [SerializeField] private float mobHealthMultiplyAmount;
        [SerializeField] private float mobDamageMultiplyAmount;

        [Header("Player")]
        [SerializeField] private float playerHealthMultiplyAmount;
        [SerializeField] private float playerDamageMultiplyAmount;

        public delegate void MoneyMultiply(float amount);
        public static event MoneyMultiply onMoneyMultiply;

        //Boss
        public delegate void BossDamageMultiply(float amount);
        public static event BossDamageMultiply onBossDamageMultiply;

        public delegate void BossHealthMultiply(float amount);
        public static event BossHealthMultiply onBossHealthMultiply;

        //Mob
        public delegate void MobDamageMultiply(float amount);
        public static event MobDamageMultiply onMobDamageMultiply;

        public delegate void MobHealthMultiply(float amount);
        public static event MobHealthMultiply onMobHealthMultiply;

        //Player
        public delegate void PlayerHealthMultiplier(float amount);
        public static event PlayerHealthMultiplier onPlayerHealthMultiply;

        public delegate void PlayerDamageMultiplier(float amount);
        public static event PlayerDamageMultiplier onPlayerDamageMultiply;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onMoneyMultiply.Invoke(moneyMultiplyamount);

            //boss
            if(bossDamageMultiplyAmount > 0)
                onBossDamageMultiply.Invoke(bossDamageMultiplyAmount);

            if(bossHealthMultiplyAmount > 0)
            onBossHealthMultiply.Invoke(bossHealthMultiplyAmount);

            //Mob
            if(mobDamageMultiplyAmount > 0)
                onMobDamageMultiply.Invoke(mobDamageMultiplyAmount);

            if (mobHealthMultiplyAmount > 0)
                onMobHealthMultiply.Invoke(mobHealthMultiplyAmount);

            //Player
            if(playerDamageMultiplyAmount > 0)
                onPlayerDamageMultiply.Invoke(playerDamageMultiplyAmount);

            if (playerHealthMultiplyAmount > 0)
                onPlayerHealthMultiply.Invoke(playerHealthMultiplyAmount);

            Destroy(gameObject);
        }
    }
}
