using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class playermove : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Animator animator; 
    public TextMeshProUGUI scoreText; 
    public GameObject gameOverPanel;

    [Header("Game Over Score UI")]
    public TextMeshProUGUI gameOverPointText;          
    public TextMeshProUGUI gameOverHighScorePointText; 
    public TextMeshProUGUI gameOverCoinText;           
    public TextMeshProUGUI gameOverHighScoreCoinText;  

    [Header("Floating Text FX")]
    public GameObject floatingTextPrefab; 
    public Transform canvasTransform;      

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip jumpSound;  
    public AudioClip pointSound; 
    public AudioClip coinSound; 
    public AudioClip deadSound;  

    public Color deadColor = Color.red;

    private int scorePoint = 0; 
    private int coinCount = 0;  
    private bool isDead = false;
    private bool isGameStarted = false; 

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // --- PAUSE GAME DI AWAL ---
        Time.timeScale = 0f; // Semua pipa dan spawner akan diam di tempat

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateScoreUI();
    }

    void Update()
    {
        if (isDead) return;

        // Gunakan unscaledDeltaTime/InputSystem agar tetap membaca klik meski Time.timeScale = 0
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Klik pertama: Mulai game dan pipa mulai jalan
            if (!isGameStarted)
            {
                isGameStarted = true;
                Time.timeScale = 1f; // Jalankan kembali seluruh pergerakan game (pipa, timer, dll)
            }

            // Eksekusi lompatan
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * 4.6f, ForceMode2D.Impulse);

            PlaySound(jumpSound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("point"))
        {
            scorePoint += 1;
            UpdateScoreUI();
            PlaySound(pointSound);

            // Langsung matikan pemicu poin begitu tersentuh pertama kali
            collision.enabled = false; 
        }
        
        else if (collision.CompareTag("coin"))
        {
            scorePoint += 2; 
            coinCount += 1;  
            
            UpdateScoreUI();
            PlaySound(coinSound);

            ShowFloatingText(collision.transform.position);

            Destroy(collision.gameObject); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            GameOver();
        }
    }

    void ShowFloatingText(Vector3 position)
    {
        if (floatingTextPrefab != null && canvasTransform != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(position);
            Instantiate(floatingTextPrefab, screenPosition, Quaternion.identity, canvasTransform);
        }
    }

    void GameOver()
    {
        isDead = true;

        PlaySound(deadSound);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = deadColor;
        }

        // Simpan & Cek High Score
        int highScorePoint = PlayerPrefs.GetInt("HighScorePoint", 0);
        if (scorePoint > highScorePoint)
        {
            highScorePoint = scorePoint;
            PlayerPrefs.SetInt("HighScorePoint", highScorePoint);
        }

        int highScoreCoin = PlayerPrefs.GetInt("HighScoreCoin", 0);
        if (coinCount > highScoreCoin)
        {
            highScoreCoin = coinCount;
            PlayerPrefs.SetInt("HighScoreCoin", highScoreCoin);
        }

        PlayerPrefs.Save(); 

        // Tampilkan ke UI Game Over
        if (gameOverPointText != null) 
            gameOverPointText.text = "Point: " + scorePoint;

        if (gameOverHighScorePointText != null) 
            gameOverHighScorePointText.text = "Best Point: " + highScorePoint;

        if (gameOverCoinText != null) 
            gameOverCoinText.text = "Coin: " + coinCount;

        if (gameOverHighScoreCoinText != null) 
            gameOverHighScoreCoinText.text = "Best Coin: " + highScoreCoin;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f; // Pause game saat game over
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = scorePoint.ToString();
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}