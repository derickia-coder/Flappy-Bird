using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 2f;    // Kecepatan teks melayang ke atas
    public float destroyTime = 0.8f; // Waktu sebelum teks hilang (detik)

    void Start()
    {
        // Hapus teks otomatis setelah beberapa detik
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // Teks melayang perlahan ke atas
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}