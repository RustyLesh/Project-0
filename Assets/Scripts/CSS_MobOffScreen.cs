using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_MobOffScreen : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Simple Collider
        if (collision.gameObject.tag == "Enemy")
        {
            // Simple deletion because it survived
            collision.gameObject.GetComponent<CSS_Enemy>().DeleteItSelf();
            Debug.Log("Enemy Survived");
        }
    }
}
