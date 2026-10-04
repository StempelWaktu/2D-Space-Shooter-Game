using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rbPlayer;
    private Vector2 movement;

    // Batas posisi Player
    private float xMin = -8f, xMax = 8f, yMin = -4f, yMax = 2.9f;

    public GameObject bulletPrefab;   // Prefab peluru
    public Transform firePoint;       // Titik tempat peluru ditembakkan
    public float bulletSpeed = 10f;   // Kecepatan peluru

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        rbPlayer.drag = 1f;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Deteksi tombol spasi untuk menembak
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        rbPlayer.velocity = movement.normalized * moveSpeed;

        // Batasi posisi Player di dalam area
        rbPlayer.position = new Vector2(
            Mathf.Clamp(rbPlayer.position.x, xMin, xMax),
            Mathf.Clamp(rbPlayer.position.y, yMin, yMax)
        );
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.rotation = firePoint.rotation;

        Rigidbody2D rbPlayer = bullet.GetComponent<Rigidbody2D>();
        if (rbPlayer != null)
        {
            rbPlayer.velocity = firePoint.up * bulletSpeed;
        }
    }
}
