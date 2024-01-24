using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot1 : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    public float fireRate;

    float fireCooldown;

    AudioSource m_shootingSound;

    private AudioSource source;

    

    void Start()
    {
        fireCooldown = fireRate;

        source = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        fireCooldown += Time.deltaTime;
        // Check for player input to shoot
        if (Input.GetKeyDown(KeyCode.Mouse0) && fireCooldown >= fireRate)
        {
            Shoot();

            source.Play();

            fireCooldown = 0;
        }
    }

    void Shoot()
    {
        // Determine the direction the player is facing
        float horizontalInput = Input.GetAxis("Horizontal");
        int direction = (int)Mathf.Sign(horizontalInput);

        // Create a bullet at the fire point position
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set the bullet's direction based on player's facing direction
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetDirection(direction);
        }
    }
}
