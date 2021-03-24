using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuralIntecrityController : MonoBehaviour{
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    [HideInInspector]
    public float maxHeal;
    private float maxArmour;
    private float maxArmourPiercingFactor;

    public GameObject[] indicators;
    public GameObject[] objToDesactive;
    public GameObject[] objToActivate;
    public AudioSource dethSound;

    //Values
    private float nextObjTimer;

    [HideInInspector]
    public bool alive;
    [Space(5)]
    [Min(0)]
    public float heal;
    [Min(0)]
    public float armour;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    private void Start(){
        maxHeal = heal;
        maxArmour = armour;
        maxArmourPiercingFactor = 1;
        alive = true;
        nextObjTimer = 1;
    }

    private void Update() {
        if (!alive) {
            //TODO temporizar salida del siguiente elemento
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    public void damage(float damageHitpoints, float armourPiercing, GameObject hitter) {
        if (!alive) { return; }

        //Math the armour piercing vs armour and remove heal properly
        if (damageHitpoints < 0) { damageHitpoints *= -1; }
        var armourPiercingFactor = armourPiercing / armour;
        if(armourPiercingFactor > maxArmourPiercingFactor) { armourPiercingFactor = maxArmourPiercingFactor; }
        float damage = damageHitpoints * armourPiercingFactor;
        heal -= damage;

        //update heal data
        healUpdate();

        //report
        Debug.Log(hitter.name + " hitted " + gameObject.name + " dealing " + damage + " damage");
    }

    public void instakill() {
        heal = -1;
        healUpdate();
    }

    public void healUpdate() {
        //update values dependent of heal
        if(heal <= 0) {
            deactiveIndicators();
            foreach (GameObject desactive in objToDesactive) {
                if(desactive != null) {
                    desactive.SetActive(false);
                }
            }
            foreach (GameObject active in objToActivate) {
                if (active != null) {
                    active.SetActive(true);
                }
            }
            if(dethSound != null) {
                dethSound.Play();
            }
            alive = false;
        }
    }

    public void deactiveIndicators() {
        foreach (GameObject desactive in indicators) {
            if (desactive != null) {
                desactive.SetActive(false);
            }
        }
    }
}
