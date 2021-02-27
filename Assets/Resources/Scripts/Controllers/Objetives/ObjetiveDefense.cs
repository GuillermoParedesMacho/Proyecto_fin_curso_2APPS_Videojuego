using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetiveDefense : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    public GameObject[] targets;

    //Values
    [HideInInspector]
    public bool victory;
    [HideInInspector]
    public bool failed;

    public float time;
    private bool useTime;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        victory = false;
        failed = false;
        if (time > 0) { useTime = true; }
        else { useTime = false; }
    }

    void Update() {
        if (!victory && !failed) {
            failed = checkFail();
            if (useTime && !failed) {
                failed = countdown();
            }
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private bool checkFail() {
        //si todos mueren, se pierde
        foreach (GameObject target in targets) {
            if (target.GetComponent<StructuralIntecrityController>().alive) {
                return false;
            }
        }
        return true;
    }

    private bool countdown() {
        time -= Time.deltaTime;
        if (time < 0) { return true; }
        return false;
    }

}
