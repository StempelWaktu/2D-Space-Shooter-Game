using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public Button btnSetting, btnPlay, btnHome, btnReset;   // Tombol Settings
    public GameObject settingsPanel;  // Panel Settings (Popup)
    public Slider volumeSlider;      // Slider untuk mengatur volume
    private AudioSource audioSource;  // AudioSource untuk mengatur volume

    private bool SettingActiveState = true;  // Default nilai awal adalah false

    public bool IsResetPressed { get; private set; }

    void Start()
    {
        Time.timeScale = settingsPanel.activeSelf ? 0 : 1;
        audioSource = GetComponent<AudioSource>();

        if (btnSetting != null)
        {
            btnSetting.onClick.AddListener(OpenSettings);
        }

        if (btnPlay != null)
        {
            btnPlay.onClick.AddListener(CloseSettings);
        }

        if (btnHome != null)
        {
            btnHome.onClick.AddListener(() => {
                IsResetPressed = true;
                SceneManager.LoadScene("Main");
            });  // Tambahkan fungsi ResetGame jika diperlukan
        }

        if (btnReset != null)
        {
            btnReset.onClick.AddListener(() => {
                IsResetPressed = true;
                SceneManager.LoadScene("Play");
            });  // Tambahkan fungsi ResetGame jika diperlukan
        }

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            volumeSlider.value = audioSource.volume;
        }
    }


    // Fungsi untuk membuka pengaturan (popup) dan mempause game
    void OpenSettings()
    {
        
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(SettingActiveState);  // Toggle panel
        }

        // Mempause atau melanjutkan permainan
        Time.timeScale = settingsPanel.activeSelf ? 0 : 1;
    }
        void CloseSettings()
    {
        
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!SettingActiveState);  // Toggle panel
        }
        Time.timeScale = settingsPanel.activeSelf ? 0 : 1;
    }

    void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

}
