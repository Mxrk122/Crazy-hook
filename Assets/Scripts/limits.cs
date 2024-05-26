using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class limits : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the game over UI GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivate the player GameObject
            Time.timeScale = 0f;
            FindObjectOfType<GameManager>().EndGame();
            // Show the game over UI
            gameOverUI.SetActive(true);
        }
    }
}
