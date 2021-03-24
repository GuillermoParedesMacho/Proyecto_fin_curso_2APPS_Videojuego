using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetiveMove: MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants

    //Values
    //[HideInInspector]
    public bool victory;
    [HideInInspector]
    public bool failed;
    [HideInInspector]
    public List<GameObject> taskedShips;

    [Min(0)]
    public float distanceToComplete;
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
        foreach (GameObject taskedShip in taskedShips) {
            if (Vector3.Distance(transform.position, taskedShip.transform.position) > distanceToComplete && taskedShip.GetComponent<StructuralIntecrityController>().alive) { return false; }
        }
        if(taskedShips.Count == 0) { return false; }
        foreach (GameObject taskedShip in taskedShips) {
            if (taskedShip.GetComponent<StructuralIntecrityController>().alive) { return true; }
        }
        return false;
    }

    private bool countdown() {
        time -= Time.deltaTime;
        if (time < 0) { return true; }
        return false;
    }

    private bool checkTaskedShips() {
        foreach (GameObject taskedShip in taskedShips) {
            if (taskedShip.GetComponent<StructuralIntecrityController>().alive) { return false; }
        }
        return true;
    }
}
