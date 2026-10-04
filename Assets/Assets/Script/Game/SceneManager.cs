using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    // List of buttons
    public Button ScenePlay;
    public Button SceneMain;
    public Button button3;

    void Start()
    {
        // Menambahkan listener untuk masing-masing tombol
        if (ScenePlay != null)
        {
            ScenePlay.onClick.AddListener(() => LoadScene("Play"));
        }
        
        if (SceneMain != null)
        {
            SceneMain.onClick.AddListener(() => LoadScene("Main"));
        }
    }
        

    // Fungsi untuk memuat scene berdasarkan nama
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}