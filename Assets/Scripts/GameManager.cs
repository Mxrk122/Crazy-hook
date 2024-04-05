using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Reference to the game over canvas
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    bool gameOver = false;

    // Update is called once per frame
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if the 'O' key is pressed and the game is over
        if (Input.GetKeyDown(KeyCode.O) && gameOver)
        {
            Restart(); // Restart the game
        }
    }

    // Start is called before the first frame update
    public void EndGame()
    {
        if (gameOver == false)
        {
            gameOver = true;
            ShowGameOverScreen(); // Show game over screen
            PlayGameOverSound(); // Play game over sound
        }
        Invoke("Restart", 10f);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowGameOverScreen()
    {
        gameOverCanvas.SetActive(true); // Set game over canvas active
        // You can add additional logic here, such as displaying scores or buttons
    }

    void PlayGameOverSound()
    {
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound); // Play the game over sound
        }
    }
}