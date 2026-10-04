using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab; // Prefab musuh yang akan di-spawn
    public float speed = 3f, spawnInterval = 2f; // Kecepatan gerak musuh
    public int poin = 10, maxEnemies = 10; // Damage yang diberikan ke player

    [Header("Shooting Settings")]
    public GameObject bulletPrefab; // Prefab peluru
    public float bulletSpeed = 10f, shootingInterval = 2f; // Kecepatan peluru

    [Header("Spawn Points")]
    public Transform[] spawnPoints; // Daftar spawn points

    private int currentEnemyCount = 0; // Jumlah musuh saat ini

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
        
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Quaternion spawnRotation = Quaternion.Euler(0, 0, -90);
        GameObject enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, spawnRotation);

        EnemyBehavior enemyBehavior = enemy.AddComponent<EnemyBehavior>();
        enemyBehavior.SetupEnemy(speed, poin, this, GameObject.FindObjectOfType<PoinManager>());

        StartCoroutine(EnemyShoot(enemy));

        currentEnemyCount++;
    }

    IEnumerator EnemyShoot(GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval);

            if (enemy == null)
                yield break;

            Transform firePoint = enemy.transform.Find("EnemyFirePoint");

            if (firePoint == null)
            {
                Debug.LogWarning("FirePoint is missing on " + enemy.name);
                yield break;
            }

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );

            bullet.transform.rotation = Quaternion.Euler(0, 0, 90);

            Rigidbody2D rbEnemy = bullet.GetComponent<Rigidbody2D>();

            if (rbEnemy != null)
                rbEnemy.velocity = Vector2.left * bulletSpeed;
        }
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
