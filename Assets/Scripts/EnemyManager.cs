using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies; // Array to store all enemy GameObjects
    public GameObject winUI; // Reference to the win UI GameObject

    private List<GameObject> aliveEnemies = new List<GameObject>(); // List to store alive enemies
    private bool win = false;

    void Start()
    {
        // Initialize the list of alive enemies with all enemies at the start
        aliveEnemies.AddRange(enemies);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.O) && win){
            Restart();
        }
    }

    // Call this method when an enemy dies
    public void EnemyDied(GameObject enemy)
    {
        // Remove the dead enemy from the list of alive enemies
        aliveEnemies.Remove(enemy);
        Debug.Log(":)");

        // Check if all enemies are dead
        if (aliveEnemies.Count == 0)
        {
            // If all enemies are dead, trigger the win condition
            WinGame();
        }
    }

    void WinGame()
    {
        // Show the win UI
        Time.timeScale = 0f;
        win = true;
        winUI.SetActive(true);
        // Add any additional win logic here
    }


    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}