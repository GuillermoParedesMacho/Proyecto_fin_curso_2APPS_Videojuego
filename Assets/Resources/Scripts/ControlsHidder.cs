using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHidder : MonoBehaviour {

    public GameObject controls;

    private void Start() {
        SettingsMenu.checkKeys();
    }

    void Update() {
        controls.SetActive(PlayerPrefs.GetInt("tips") > 0);
    }
}
