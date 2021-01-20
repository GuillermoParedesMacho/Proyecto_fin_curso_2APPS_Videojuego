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

}
