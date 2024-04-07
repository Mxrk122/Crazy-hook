using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public GameObject respawnPoint; // Assign the respawn point GameObject in the Unity Editor
    public GameObject gameOverUI; // Reference to the game over UI GameObject

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Deactivate the player GameObject
            Time.timeScale = 0f;



            FindObjectOfType<GameManager>().EndGame();
            // Show the game over UI
            gameOverUI.SetActive(true);

        }
        else{
            Destroy(gameObject);
        }
    }

}