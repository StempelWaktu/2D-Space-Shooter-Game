using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    private float speed, timer = 0f;
    private int poin;
    private EnemySpawnManager spawnManager;
    private PoinManager poinManager; // Referensi ke PoinManager
    private SettingsController settingsController; // Perbaikan nama kelas
    private PlayerHealth playerHealth;
    

    // Setup untuk enemy
    public void SetupEnemy(float _speed, int _poin, EnemySpawnManager _spawnManager, PoinManager _poinManager)
    {
        this.speed = _speed;
        this.poin = _poin;
        this.spawnManager = _spawnManager;
        this.poinManager = _poinManager; // Setup PoinManager
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        settingsController = FindObjectOfType<SettingsController>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    bool IsVisible(Camera camera)
    {
        if (camera != null)
        {
            Vector3 viewportPoint = camera.WorldToViewportPoint(transform.position);
            return viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
        }
        return false;
    }

    void Update()
    {
        if ((settingsController != null && settingsController.IsResetPressed) || 
            (playerHealth != null && playerHealth.IsResetPressed))
        {
            Destroy(gameObject); // Hancurkan musuh jika reset ditekan
            return;
        }

        if (!IsVisible(Camera.main))
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                Destroy(gameObject);
                spawnManager.OnEnemyDestroyed();
            }
        }
        else
        {
            timer = 0f;
        }

        transform.position -= Vector3.right * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && IsVisible(Camera.main))
        {
            Destroy(other.gameObject); // Hancurkan peluru
            Destroy(gameObject);         // Hancurkan musuh

            if (poinManager != null)
            {
                poinManager.AddScore(poin); // Tambahkan skor
            }

            spawnManager.OnEnemyDestroyed(); // Kurangi musuh
        }
    }
}
