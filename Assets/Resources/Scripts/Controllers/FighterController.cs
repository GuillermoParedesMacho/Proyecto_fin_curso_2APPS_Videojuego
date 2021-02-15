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
    public float fowardRSpeed;
    [HideInInspector]
    public Vector3 movement;
    [HideInInspector]
    public Vector3 rotation;
    [HideInInspector]
    public bool fire;
    [Header("velocidad a la que aumenta o reduce la velocidad")]
    [Min(0)]
    public float accelerationScale;
    private bool beforeDeadTODO;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    void Start() {
        sMC = gameObject.GetComponent<ShipMovementController>();
        sIC = gameObject.GetComponent<StructuralIntecrityController>();
        cannon = gameObject.GetComponent<CannonController>();
        beforeDeadTODO = true;
    }
    
    void Update(){
        if (sIC.alive) {
            fowardRSpeed = sMC.inFowardBackwards;
            sMC.inFowardBackwards += movement.z * accelerationScale * Time.deltaTime;
            sMC.inLeftRight = movement.x;
            sMC.inUpDown = movement.y;
            sMC.inPitch = rotation.x;
            sMC.inRoll = rotation.z;
            sMC.inYaw = rotation.y;
            if (fire) {  cannon.shoot(); }
        }
        else if (beforeDeadTODO) {
            sMC.inFowardBackwards = 0;
            sMC.inLeftRight = 0;
            sMC.inUpDown = 0;
            sMC.inPitch = 0;
            sMC.inRoll = 0;
            sMC.inYaw = 0;
            beforeDeadTODO = false;
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    public void propIncrease(float increase) {
        if (increase > 1) { increase = 1; }
        else if (increase < -1) { increase = -1; }
        movement.z += increase;
    }
}
