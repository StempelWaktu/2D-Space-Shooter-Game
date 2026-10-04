using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keluar : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton.onClick.AddListener(KeluarGame);
    }

public void KeluarGame()
{
    Debug.Log("Keluar dari game!");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Untuk keluar dari Play Mode di Editor
    #else
        Application.Quit(); // Untuk keluar aplikasi di build
    #endif
}

}
