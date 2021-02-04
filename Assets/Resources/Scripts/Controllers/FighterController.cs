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
    [HideInInspector]
    public Vector3 movement;
    [HideInInspector]
    public Vector3 rotation;
    [HideInInspector]
    public bool fire;
    [Header("velocidad a la que aumenta o reduce la velocidad")]
    [Min(0)]
    public float accelerationScale;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    void Start() {
        sMC = gameObject.GetComponent<ShipMovementController>();
        sIC = gameObject.GetComponent<StructuralIntecrityController>();
        cannon = gameObject.GetComponent<CannonController>();
    }
    
    void Update(){
        if (sIC.alive) {
            sMC.inFowardBackwards += movement.z * accelerationScale * Time.deltaTime;
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
        movement.z += increase;
    }
}
