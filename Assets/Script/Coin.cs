using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed = 2f; // Kecepatan koin bergerak ke kiri mengikuti rintangan/pipa

    void Update()
    {
        // Koin bergerak ke kiri
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // Hapus koin jika sudah keluar layar di sebelah kiri
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}