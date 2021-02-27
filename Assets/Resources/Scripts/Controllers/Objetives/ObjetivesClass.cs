using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivesClass : MonoBehaviour { 
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants

    //Values
    [HideInInspector]
    public bool victory;
    [HideInInspector]
    public bool failed;
    [HideInInspector]
    public enum objetivos { Atack, Defense, Move };
    [HideInInspector]
    public objetivos objetivo;

    public bool active;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        victory = false;
        failed = false;

        if (gameObject.GetComponent<ObjetiveAtack>() != null) { objetivo = objetivos.Atack; }
        else if (gameObject.GetComponent<ObjetiveDefense>() != null) { objetivo = objetivos.Defense; }
        else if (gameObject.GetComponent<ObjetiveMove>() != null) { objetivo = objetivos.Move; }
        else { Debug.LogError("Objeto objetivo " + gameObject.name + " no tiene el objetivo correspondiente"); }
    }

    void Update() {
        switch (objetivo) {
            case objetivos.Atack:
                objetiveAtack();
                break;
            case objetivos.Defense:
                objetiveDefense();
                break;
            case objetivos.Move:
                objetiveMove();
                break;
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    private void objetiveAtack() {
        victory = gameObject.GetComponent<ObjetiveAtack>().victory;
        failed = gameObject.GetComponent<ObjetiveAtack>().failed;
    }

    private void objetiveDefense() {
        victory = gameObject.GetComponent<ObjetiveDefense>().victory;
        failed = gameObject.GetComponent<ObjetiveDefense>().failed;
    }

    private void objetiveMove() {
        //victory = gameObject.GetComponent<ObjetiveMove>().victory;
        //failed = gameObject.GetComponent<ObjetiveMove>().failed;
    }

}
