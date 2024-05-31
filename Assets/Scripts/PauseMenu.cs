using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                GamePaused = false;
                Resume();
            }
            else
            {
                GamePaused = true;
                Pause();
            }


        }

        if (GamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        AudioListener.volume = 1;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        AudioListener.volume = 0;
    }
}
