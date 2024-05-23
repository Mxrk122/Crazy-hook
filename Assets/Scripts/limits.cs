using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limits : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverUI; // Reference to the game over UI GameObject
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }
}
