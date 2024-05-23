using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public GameObject respawnPoint; // Assign the respawn point GameObject in the Unity Editor
    public GameObject gameOverUI; // Reference to the game over UI GameObject
    public float reflectionForce = 10f; // Force applied to the reflected bullet

    private static bool canParry = false; // Flag to indicate if the player can parry

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Shield"))
        {
            Debug.Log(other.gameObject.tag);
            // If the player can parry, reflect the bullet back
            if (canParry)
            {
                // Calculate the reflection direction
                Vector3 reflectionDirection = Vector3.Reflect(transform.position - respawnPoint.transform.position, transform.forward).normalized;

                // Apply force to reflect the bullet
                GetComponent<Rigidbody>().velocity = reflectionDirection * reflectionForce * 5;
            }
            else
            {
                if (other.gameObject.CompareTag("Shield"))
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    GameObject shieldUI = GameObject.Find("ShieldUI");
                    shieldUI.SetActive(false);
                }
                else
                {
                    // Deactivate the player GameObject
                    Time.timeScale = 0f;
                    FindObjectOfType<GameManager>().EndGame();
                    // Show the game over UI
                    gameOverUI.SetActive(true);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to enable parry
    public void EnableParry()
    {
        canParry = true;
        Debug.Log("(:");
    }

    // Method to disable parry

    public void DisableParry()
    {
        canParry = false;
    }

}