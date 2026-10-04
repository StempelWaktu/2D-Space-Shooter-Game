using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyBullet : MonoBehaviour
{
    void OnBecameInvisible()
    {
        Destroy(gameObject); // Menghancurkan peluru saat keluar dari layar
    }
}
