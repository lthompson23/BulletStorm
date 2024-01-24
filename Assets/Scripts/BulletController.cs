using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5f;

    private int direction;

    // Set the bullet's direction
    public void SetDirection(int dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the specified direction
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Destroy the bullet if it goes off-screen
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}
