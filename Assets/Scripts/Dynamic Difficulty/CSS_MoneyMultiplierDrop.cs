using UnityEngine.Events;
using UnityEngine;

namespace Project0
{
    public class CSS_MoneyMultiplierDrop : MonoBehaviour
    {
        [SerializeField] private float amount;

        public delegate void MoneyMultiply(float amount);
        public static event MoneyMultiply onMoneyMultiply;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("pp");
            onMoneyMultiply.Invoke(amount);
            Destroy(gameObject);
        }
    }
}
