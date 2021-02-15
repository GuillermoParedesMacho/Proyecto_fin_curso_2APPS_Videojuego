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

    //Values
    private GameObject target;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    void Start(){
        fController = gameObject.GetComponent<FighterController>();
        sensor = gameObject.GetComponent<SensorsSystem>();
        pathFinder = gameObject.GetComponent<GyroShipPathFinder>();
        shipHeal = gameObject.GetComponent<StructuralIntecrityController>();
    }

    // Update is called once per frame
    void Update(){
        if (shipHeal.alive) {
            target = selectNewTarget();
            if (target != null) {
                Vector3 offset = new Vector3(0, 1, 0);
                Debug.DrawLine(transform.position + offset, target.transform.position + offset, Color.red, 0.01f, true);
                pathFinder.moveToPos(target, 1);

                Vector2 angles =PhishicsMath.V3relativePosToV2ang(target.transform.position - transform.position);
                Vector2 shipGlovalAngle = PhishicsMath.V3relativePosToV2ang((transform.position + 10 * transform.forward) - transform.position);
                angles -= shipGlovalAngle;

                if (Vector3.Distance(transform.position, target.transform.position)<500 && (angles.x < 5 && angles.x > -5) && (angles.y < 5 && angles.y > -5)) {
                    fController.fire = true;
                }
                else {
                    fController.fire = false;
                }

                if (!target.GetComponent<StructuralIntecrityController>().alive) {
                    target = null;
                }
            }
            else {
                sensor.scan();
                selectNewTarget();
                fController.fire = false;
            }
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private GameObject selectNewTarget() {
        if(sensor.enemys.Count > 0) {
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
            return target;
        }
        return null;
    }
}
