using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour {
    public UserController user;
    public GameObject options;

    public void SettingsMenu() {
        try {
            options.SetActive(true);
            gameObject.SetActive(false);
        }
        catch{
            SceneManager.LoadScene("SettingsMenu");
        }
    }

    public void QuitGame(){
        SceneManager.LoadScene("MainMenu");
    }

    public void missionsMenu() {
        SceneManager.LoadScene("MissionsMenu");
    }

    public void resume() {
        user.menuOpciones();
    }
}

