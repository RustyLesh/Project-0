using UnityEngine;

public class CSS_SpawnParticleSystem : MonoBehaviour {
    [SerializeField] ParticleSystem enemyParticleSystem;

    //Creates a clone of the EnemyParticleSystem
    public void ParticleEffectOnDeath(Transform enemyTransform) {
        Instantiate(enemyParticleSystem, enemyTransform.position, Quaternion.identity);
    }
}
