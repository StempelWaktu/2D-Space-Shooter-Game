using UnityEngine;
using UnityEngine.UI;

public class PoinManager : MonoBehaviour
{
    public Text scoreText; // Referensi ke UI Text untuk menampilkan skor
    public Text highScoreText; // Referensi ke UI Text untuk menampilkan high score
    private int highScore, score = 0; // Skor saat ini


    private void Start()
    {
        // PlayerPrefs.DeleteKey("HighScore");
        // highScoreText.text = "High Score: 0";
        score = 0;
        LoadHighScore();
        UpdateUI();
    }
    public void AddScore(int points)
    {
        // Tambahkan poin ke skor
        score += points;

        // Perbarui High Score jika skor lebih tinggi
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }

        // Perbarui tampilan UI
        UpdateUI();
    }

    private void SaveHighScore()
    {
        // Simpan High Score ke PlayerPrefs
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        // Muat High Score dari PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void UpdateUI()
    {
        // Perbarui teks skor di UI
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        // Perbarui teks High Score di UI
        if (highScoreText != null)
        {
            highScoreText.text = highScore.ToString();
        }
    }
}
