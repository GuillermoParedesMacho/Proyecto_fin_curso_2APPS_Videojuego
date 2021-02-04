using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementController: MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private Rigidbody rigitBody;

    //Values
    
    [HideInInspector]
    public float inPitch;
    [HideInInspector]
    public float inRoll;
    [HideInInspector]
    public float inYaw;
    private float pitch;
    private float roll;
    private float yaw;
    private Vector3 rotationVector;
    [Space(5)]
    [Min(0)]
    [Header("Aceleracion rotacion unidades/s")]
    public float pitchAcel;
    [Min(0)]
    public float rollAcel;
    [Min(0)]
    public float yawAcel;

    [HideInInspector]
    public float inFowardBackwards;
    [HideInInspector]
    public float inLeftRight;
    [HideInInspector]
    public float inUpDown;
    private float fowardBackwards;
    private float leftRight;
    private float upDown;
    private Vector3 movementVector;
    [Space(5)]
    [Min(0)]
    [Header("Aceleracion velocidad m/s")]
    public float fowardAcel;
    [Min(0)]
    public float backwardsAcel;
    [Min(0)]
    public float leftRighAcel;
    [Min(0)]
    public float upDownDownAcel;

    private float time;
    private float mass;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    private void Start() {
        rigitBody = gameObject.GetComponent<Rigidbody>();
        mass = rigitBody.mass; //mass in grams
    }

    private void Update() {
        main();
    }

    public void main() {
        //must be called to make the ship move
        time = Time.deltaTime;
        checkLimits(1);
        rotationVectorBuild();
        movementVectorBuild();
        applyAcelerationVectors();
        Debug.Log(rigitBody.angularVelocity);
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    //TODO añadir un separador de tiempo

    private void checkLimits(float limit) {
        //Limiting rotation
        inPitch = limitValue(inPitch, limit);
        inYaw = limitValue(inYaw, limit);
        inRoll = limitValue(inRoll, limit);

        //Limiting movement
        inFowardBackwards = limitValue(inFowardBackwards, limit);
        inLeftRight = limitValue(inLeftRight, limit);
        inUpDown = limitValue(inUpDown, limit);
    }

    private void rotationVectorBuild() {
        //making of the math and creating the rotation vector
        pitch = multiplyCorrect(inPitch, pitchAcel, pitchAcel);
        yaw = multiplyCorrect(inYaw, yawAcel, yawAcel);
        roll = multiplyCorrect(inRoll, rollAcel, rollAcel);

        //PhishicsMath.degreSToNewtows(roll, mass, time)
        //valorar la opcion de anadir un valor de escalada
        rotationVector = Vector3.zero;
        rotationVector += transform.right * PhishicsMath.degreSToNewtows(pitch, mass, time, rigitBody.angularDrag);
        rotationVector += transform.up * PhishicsMath.degreSToNewtows(yaw, mass, time, rigitBody.angularDrag);
        rotationVector += transform.forward * PhishicsMath.degreSToNewtows(roll, mass, time, rigitBody.angularDrag) * 0.25f;
    }

    private void movementVectorBuild() {
        //making the math and updating the movment vector
        fowardBackwards = multiplyCorrect(inFowardBackwards, fowardAcel, backwardsAcel);
        leftRight = leftRighAcel * inLeftRight;
        upDown = upDownDownAcel * inUpDown;
        
        //input(km/h) * world angle * interaction time * mass
        movementVector = Vector3.zero;
        
        movementVector += transform.right * PhishicsMath.msToNewtowns(leftRight, mass, time, rigitBody.drag);
        movementVector += transform.up * PhishicsMath.msToNewtowns(upDown, mass, time, rigitBody.drag);
        movementVector += transform.forward * PhishicsMath.msToNewtowns(fowardBackwards, mass, time, rigitBody.drag);
    }

    private void applyAcelerationVectors() {
        //make the thing move
        rigitBody.AddForce(movementVector);
        rigitBody.AddTorque(rotationVector);
    }

    private float limitValue(float value, float limit) {
        //making a value to be with in limits
        if (value > limit) { value = limit; }
        else if (value < -1 * limit) { value = -1 * limit; }
        return value;
    }

    private float multiplyCorrect(float value, float upMult, float downMult) {
        //multiply the value to the correct miltyplier
        if (value > 0) { value *= upMult; }
        else if (value < 0) { value *= downMult; }
        return value;
    }
}