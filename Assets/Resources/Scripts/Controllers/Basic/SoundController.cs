using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private AudioSource source;

    public enum SoundType { music, soudEffect };
    public SoundType soundType;

    //Values
    [Range(0,1)]
    public float scale;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void Start() {
        source = gameObject.GetComponent<AudioSource>();
        SettingsMenu.checkKeys();
    }

    private void Update() {
        float general = PlayerPrefs.GetFloat("GeneralVolumen");
        float music = PlayerPrefs.GetFloat("MusicVolumen");
        float soundeEffects = PlayerPrefs.GetFloat("SoundEffectsVolumen");
        if(soundType == SoundType.music) {
            source.volume = scale * music * general;
        }
        else {
            source.volume = scale * soundeEffects * general;
        }
        
    }
}
