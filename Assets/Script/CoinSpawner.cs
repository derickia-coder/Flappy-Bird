using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Slot prefab koin
    public float spawnRate = 3f;  // Jeda waktu muncul koin (detik)
    public float minY = -2f;      // Batas posisi Y paling bawah
    public float maxY = 3f;       // Batas posisi Y paling atas

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnCoin();
            timer = 0f;
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            // Menentukan posisi Y secara acak
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

            // Memunculkan koin
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}