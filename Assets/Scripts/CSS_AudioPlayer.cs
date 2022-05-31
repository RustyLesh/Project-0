using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CSS_AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip playerShootingClip;
    [SerializeField] AudioClip enemyShootingClip;
    [SerializeField] AudioClip bossShootingClip;
    [SerializeField] AudioClip collisionClip;

    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    public void PlayShootingClip()
    {
        if(playerShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(playerShootingClip, Camera.main.transform.position, shootingVolume);
        }
    }
    public void PlayEnemyShootingClip()
    {
        if (enemyShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(enemyShootingClip, Camera.main.transform.position, shootingVolume);
        }
    }
    public void PlayBossShootingClip()
    {
        if (bossShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(bossShootingClip, Camera.main.transform.position, shootingVolume);
        }
    }

    public void PlayCollisionClip()
    {
        if (collisionClip != null)
        {
            AudioSource.PlayClipAtPoint(collisionClip, Camera.main.transform.position, shootingVolume);
        }
    }
}