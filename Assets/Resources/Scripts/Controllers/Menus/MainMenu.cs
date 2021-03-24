using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MissionsMenu");

    }
    public void LeaderbordsMenu()
    {
        SceneManager.LoadScene("LeaderboardsMenu");

    }
    public void SettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");

    }
     public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

