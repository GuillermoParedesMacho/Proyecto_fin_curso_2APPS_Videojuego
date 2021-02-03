using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private ShipMovementController sMC;
    private StructuralIntecrityController sIC;
    private CannonController cannon;

    //Values
    public Vector3 movement;
    public Vector3 rotation;
    public bool fire;
    public float accelerationScale;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    void Start() {
        sMC = gameObject.GetComponent<ShipMovementController>();
        sIC = gameObject.GetComponent<StructuralIntecrityController>();
        cannon = gameObject.GetComponent<CannonController>();
    }
    
    void Update(){
        if (sIC.alive) {
            sMC.inFowardBackwards = movement.z;
            sMC.inLeftRight = movement.x;
            sMC.inUpDown = movement.y;
            sMC.inPitch = rotation.x;
            sMC.inRoll = rotation.z;
            sMC.inYaw = rotation.y;
            if (fire) {  cannon.shoot(); }
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    public void propIncrease(float increase) {
        if (increase > 1) { increase = 1; }
        else if (increase < -1) { increase = -1; }
        movement.z += increase * accelerationScale * Time.deltaTime;
    }
}
