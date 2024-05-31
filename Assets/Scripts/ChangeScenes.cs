using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToTutorial()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToRoofTop()
    {
        SceneManager.LoadScene("Climb");
    }

    public void GoToSkyScraper()
    {
        SceneManager.LoadScene("DayCity");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
