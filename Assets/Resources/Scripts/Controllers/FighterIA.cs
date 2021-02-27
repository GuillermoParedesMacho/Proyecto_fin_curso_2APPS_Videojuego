using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterIA : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private FighterController fController;
    private SensorsSystem sensor;
    private GyroShipPathFinder pathFinder;
    private StructuralIntecrityController shipHeal;

    public enum estados { idle, battle, objetive };
    public estados estado;

    //Values
    private GameObject target;
    public ObjetivesClass[] objetives;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    void Start(){
        fController = gameObject.GetComponent<FighterController>();
        sensor = gameObject.GetComponent<SensorsSystem>();
        pathFinder = gameObject.GetComponent<GyroShipPathFinder>();
        shipHeal = gameObject.GetComponent<StructuralIntecrityController>();

        estado = estados.idle;
        objetiveStarter();
    }

    // Update is called once per frame
    void Update(){
        if (shipHeal.alive) {
            estadoToIAFunctions();
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //-----estados--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_
    /*estados de la IA para cumplir los diferentes objetivos*/
    private void estadoToIAFunctions() {
        switch (estado) {
            case estados.idle:
                estadoIdle();
                break;
            case estados.battle:
                estadoBattle();
                break;
            case estados.objetive:
                estadoObjetive();
                break;
            default:
                Debug.LogWarning("estado no programado");
                break;
        }
    }

    private void estadoIdle() {
        //TODO esperar en el sistio y buscar objetivos
        if (checkSensors()) {
            estado = estados.battle;
        }
    }

    private void estadoBattle() {
        //TODO buscar, matar, aniquilar (tactica de combate principal: "bum and run")
        if (!checkSensors()) {
            estado = estados.idle;
            fController.stop();
            fController.fire = false;
            return;
        }
        if (target != null) {
            pathFinder.moveToPos(target, 1, 0);
            fireCannons(target.transform.position);

            if (!target.GetComponent<StructuralIntecrityController>().alive) {
                target = null;
            }
        }
        else {
            target = selectNewTarget();
            fController.fire = false;
        }
    }

    private void estadoObjetive() {
        //TODO cumplir objetivos establecidos
        switch (objetives[0].objetivo) {
            case ObjetivesClass.objetivos.Atack:
                break;
            case ObjetivesClass.objetivos.Defense:
                break;
            case ObjetivesClass.objetivos.Move:
                break;
        }
    }

    //-----actions--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_
    /*Acciones y funciones que activan los diferentes sistemas de la nave*/
    //target selection and obj awearnes
    private bool checkSensors() {
        sensor.scan();
        if (sensor.enemys.Count > 0) { return true; }
        else { return false; }
    }
    
    private GameObject selectNewTarget() {
        GameObject target = null;
        foreach (GameObject enemy in sensor.enemys) {
            if(target != null) {
                if(Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, target.transform.position)) {
                    target = enemy;
                }
            }
            else {
                target = enemy;
            }
        }
        if(target != null) {
            return target;
        }
        return null;
    }

    //weapons
    private void fireCannons(Vector3 target) {
        Vector3 offset = new Vector3(0, 1, 0);
        Debug.DrawLine(transform.position + offset, target + offset, Color.red, 0.01f, true);

        Vector2 angles = PhishicsMath.V3relativePosToV2ang(target - transform.position);
        Vector2 shipGlovalAngle = PhishicsMath.V3relativePosToV2ang((transform.position + 10 * transform.forward) - transform.position);
        angles -= shipGlovalAngle;

        if (Vector3.Distance(transform.position, target) < 500 && (angles.x < 5 && angles.x > -5) && (angles.y < 5 && angles.y > -5)) {
            fController.fire = true;
        }
        else {
            fController.fire = false;
        }
    }

    //objetive starter
    private void objetiveStarter() {
        foreach (ObjetivesClass objetive in objetives) {
            switch (objetive.objetivo) {
                case ObjetivesClass.objetivos.Atack:
                    objetive.GetComponent<ObjetiveAtack>().taskedShips.Add(gameObject);
                    break;
                case ObjetivesClass.objetivos.Move:
                    objetive.GetComponent<ObjetiveMove>().taskedShips.Add(gameObject);
                    break;
            }
        }
    }
}
