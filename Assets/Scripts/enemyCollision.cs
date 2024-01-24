using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollision : MonoBehaviour
{
    public GameObject enemy; 


    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        if (collision.tag == "Player")
        {
            healthBar.health -= 25;
        }

        if (collision.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
