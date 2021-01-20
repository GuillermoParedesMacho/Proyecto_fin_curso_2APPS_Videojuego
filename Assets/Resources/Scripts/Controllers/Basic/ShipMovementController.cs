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
    [Header("Aceleracion m/s")]
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
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    //TODO añadir un separador de tiempo

    private void checkLimits(float limit) {
        //Limiting rotation
        pitch = limitValue(inPitch, limit);
        yaw = limitValue(inYaw, limit);
        roll = limitValue(inRoll, limit);

        //Limiting movement
        fowardBackwards = limitValue(inFowardBackwards, limit);
        leftRight = limitValue(inLeftRight, limit);
        upDown = limitValue(inUpDown, limit);
    }

    private void rotationVectorBuild() {
        //making of the math and creating the rotation vector
        pitch = multiplyCorrect(pitch, pitchAcel, pitchAcel);
        yaw = multiplyCorrect(yaw, yawAcel, yawAcel);
        roll = multiplyCorrect(roll, rollAcel, rollAcel);

        //PhishicsMath.degreSToNewtows(roll, mass, time)
        rotationVector = Vector3.zero;
        rotationVector += transform.right * PhishicsMath.degreSToNewtows(pitch, mass, time);
        rotationVector += transform.up * PhishicsMath.degreSToNewtows(yaw, mass, time);
        rotationVector += transform.forward * PhishicsMath.degreSToNewtows(roll, mass, time);
    }

    private void movementVectorBuild() {
        //making the math and updating the movment vector
        fowardBackwards = multiplyCorrect(fowardBackwards, fowardAcel, backwardsAcel);
        leftRight *= leftRighAcel;
        upDown *= upDownDownAcel;
        
        //input(km/h) * world angle * interaction time * mass
        movementVector = Vector3.zero;
        
        movementVector += transform.right * PhishicsMath.msToNewtowns(leftRight, time, mass);
        movementVector += transform.up * PhishicsMath.msToNewtowns(upDown, time, mass);
        movementVector += transform.forward * PhishicsMath.msToNewtowns(fowardBackwards, time, mass);
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