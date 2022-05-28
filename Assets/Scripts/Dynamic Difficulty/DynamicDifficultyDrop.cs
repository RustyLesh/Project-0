using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project0
{
    public class DynamicDifficultyDrop : MonoBehaviour
    {
        [SerializeField] private float moneyMultiplyamount;
        [SerializeField] private float bossDamageMultiplyAmount;
        [SerializeField] private float bossHealthMultiplyAmount;
        [SerializeField] private float mobHealthMultiplyAmount;
        [SerializeField] private float mobDamageMultiplyAmount;

        public delegate void MoneyMultiply(float amount);
        public static event MoneyMultiply onMoneyMultiply;

        public delegate void BossDamageMultiply(float amount);
        public static event BossDamageMultiply onBossDamageMultiply;

        public delegate void BossHealthMultiply(float amount);
        public static event BossHealthMultiply onBossHealthMultiply;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            onMoneyMultiply.Invoke(moneyMultiplyamount);
            onBossDamageMultiply.Invoke(bossDamageMultiplyAmount);
            onBossHealthMultiply.Invoke(bossHealthMultiplyAmount);

            Destroy(gameObject);
        }
    }
}
