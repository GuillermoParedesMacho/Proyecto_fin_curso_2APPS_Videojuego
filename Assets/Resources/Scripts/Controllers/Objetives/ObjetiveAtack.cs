using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetiveAtack : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    public GameObject[] targets;

    //Values
    [HideInInspector]
    public bool victory;
    [HideInInspector]
    public bool failed;
    [HideInInspector]
    public List<GameObject> taskedShips;

    public float time;
    private bool useTime;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    void Start() {
        victory = false;
        failed = false;
        if(time > 0) { useTime = true; }
        else { useTime = false; }
    }
    
    void Update() {
        if (!victory && !failed) {
            victory = checkVictory();
            if (useTime && !failed) {
                failed = countdown();
            }
            if (taskedShips.Count > 0 && !failed) {
                failed = checkTaskedShips();
            }
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private bool checkVictory() {
        //si todos mueren, victoria
        foreach (GameObject target in targets) {
            if (target.GetComponent<StructuralIntecrityController>().alive) { return false; }
        }
        return true;
    }

    private bool countdown() {
        time -= Time.deltaTime;
        if(time < 0) { return true; }
        return false;
    }

    private bool checkTaskedShips() {
        foreach (GameObject taskedShip in taskedShips) {
            if (taskedShip.GetComponent<StructuralIntecrityController>().alive) { return false; }
        }
        return true;
    }

}
