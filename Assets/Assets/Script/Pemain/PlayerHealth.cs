using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Text livesText;
    public Button btnHome, btnReset;
    public int lives = 3; // Jumlah nyawa pemain
    public Transform spawnPoint; // Titik respawn pemain
    public GameObject GOverPanel;  // Panel Settings (Popup)
    public float respawnDelay = 0.5f; // Waktu delay sebelum respawn
    private bool isInvulnerable, isRespawning = false;
    public bool IsResetPressed { get; private set; }

    private Renderer playerRenderer;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        if (btnHome != null)
        {
            btnHome.onClick.AddListener(() => {
                IsResetPressed = true;
                SceneManager.LoadScene("Main");
            });
        }

        if (btnReset != null)
        {
            btnReset.onClick.AddListener(() => {
                IsResetPressed = true;
                SceneManager.LoadScene("Play");
            });
        }
    }

    public void TakeDamage(int damage)
    {
        if (isRespawning || isInvulnerable) return;

        lives -= damage;
        Debug.Log("Player took damage! Lives left: " + lives);
        livesText.text = lives.ToString();

        if (lives > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator Respawn()
    {
        isRespawning = true;
        isInvulnerable = true;

        // Menonaktifkan kontrol pemain sementara respawn
        // Tambahkan animasi atau efek kematian di sini
        yield return new WaitForSeconds(respawnDelay);

        // Respawn pemain ke spawn point
        transform.position = spawnPoint.position;

        // Reset kondisi pemain (jika diperlukan)
        isRespawning = false;
        StartCoroutine(InvulnerabilityCountdown(3)); // Memulai imunisasi selama 3 detik
    }

    IEnumerator InvulnerabilityCountdown(float duration)
    {
        isInvulnerable = true;
        Color color = playerRenderer.material.color;

        // Set opacity menjadi 50% saat imun
        color.a = 0.2f;  
        playerRenderer.material.color = color;

        yield return new WaitForSeconds(duration);

        // Kembalikan opacity ke 100%
        color.a = 1f;
        playerRenderer.material.color = color;

        isInvulnerable = false;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        if (GOverPanel != null)
        {
            GOverPanel.SetActive(true);  // Toggle panel
        }
        Time.timeScale = GOverPanel.activeSelf ? 0 : 1;

        // Destroy(gameObject); // Menghapus pemain (opsional)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Deteksi jika tabrakan dengan pemain
        {
            TakeDamage(1); // Mengurangi nyawa pemain
            Debug.Log("Enemy collided with Player!");
        }
    }
}
