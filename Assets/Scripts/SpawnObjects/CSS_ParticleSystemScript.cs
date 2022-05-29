using UnityEngine;

public class CSS_ParticleSystemScript : MonoBehaviour {
    [SerializeField] public ParticleSystem thisParticleSystem;

    public void Awake() {
        thisParticleSystem.Play();
    }

    //Once particle effect is no longer playing, destroy the particle system
    void Update() {
        if (!thisParticleSystem.isPlaying) {
            Destroy(gameObject);
        }
    }
}
