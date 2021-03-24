using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour{
    public Slider generalVolumenSl;
    public Slider musicVolumenSl;
    public Slider soundEffectVolumenSl;

    public Toggle toogleFullscreen;
    public Toggle toogleHints;

    public UserController user;

    private void Awake() {
        checkKeys();
        toogleFullscreen.isOn = Screen.fullScreen;
        generalVolumenSl.value = PlayerPrefs.GetFloat("GeneralVolumen");
        musicVolumenSl.value = PlayerPrefs.GetFloat("MusicVolumen");
        soundEffectVolumenSl.value = PlayerPrefs.GetFloat("SoundEffectsVolumen");
        toogleHints.isOn = PlayerPrefs.GetInt("tips") > 0;
    }

    private void Update() {
        PlayerPrefs.SetFloat("GeneralVolumen", generalVolumenSl.value);
        PlayerPrefs.SetFloat("MusicVolumen", musicVolumenSl.value);
        PlayerPrefs.SetFloat("SoundEffectsVolumen", soundEffectVolumenSl.value);
        setFullscreen(toogleFullscreen.isOn);
        setTooltips(toogleHints.isOn);
    }
    
    public void setFullscreen(bool input) {
        Screen.fullScreen = input;
    }
    
    public void setTooltips(bool input) {
        if (input) {
            PlayerPrefs.SetInt("tips", 1);
        }
        else {
            PlayerPrefs.SetInt("tips", 0);
        }
    }

    public void exit() {
        if(user != null) {
            user.menuOpciones();
            gameObject.SetActive(false);
        }
        else {
            SceneManager.LoadScene("MainMenu");
        }
    }

    static public void checkKeys() {
        if (!PlayerPrefs.HasKey("GeneralVolumen")) {
            PlayerPrefs.SetFloat("GeneralVolumen", 0.3f);
        }
        if (!PlayerPrefs.HasKey("MusicVolumen")) {
            PlayerPrefs.SetFloat("MusicVolumen", 0.3f);
        }
        if (!PlayerPrefs.HasKey("SoundEffectsVolumen")) {
            PlayerPrefs.SetFloat("SoundEffectsVolumen", 0.3f);
        }
        if (!PlayerPrefs.HasKey("tips")) {
            PlayerPrefs.SetInt("tips", 1);
        }
    }
}


