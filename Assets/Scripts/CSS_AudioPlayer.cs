/*
  *Author: Luke Jordens
  *This class is is designed to handle all methods related to sound effects in the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CSS_AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    
    // The different audio clips used for the different sound effects within the game
    [SerializeField] private AudioClip playerShootingClip;
    [SerializeField] private AudioClip enemyShootingClip;
    [SerializeField] private AudioClip bossShootingClip;
    [SerializeField] private AudioClip collisionClip;
    [SerializeField] private AudioClip explosionClip;

    // Variable to hold volume level 
    [SerializeField] private float audioVolume = 0.5f;

    // Method that plays audio clip when player shoots
    public void PlayShootingClip()
    {
        if(playerShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(playerShootingClip, Camera.main.transform.position, audioVolume);
        }
    }
    // Method that plays audio clip when enemy shoots
    public void PlayEnemyShootingClip()
    {
        if (enemyShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(enemyShootingClip, Camera.main.transform.position, audioVolume);
        }
    }

    // Method that plays audio clip when boss shoots
    public void PlayBossShootingClip()
    {
        if (bossShootingClip != null)
        {
            AudioSource.PlayClipAtPoint(bossShootingClip, Camera.main.transform.position, audioVolume);
        }
    }

    // Method that plays audio clip when bullet collides with player or enemy
    public void PlayCollisionClip()
    {
        if (collisionClip != null)
        {
            AudioSource.PlayClipAtPoint(collisionClip, Camera.main.transform.position, audioVolume);
        }
    }

    // Method that plays audio clip when player or enemy dies (explodes)
    public void PlayExplosionClip()
    {
        if (explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, audioVolume);
        }
    }
}