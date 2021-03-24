using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private GameObject gyro;
    private CannonController cannonController;
    private StructuralIntecrityController sIC;
    private float bulletspeed;

    [Header("Y es horizontal e X es vertical")]
    [Min(0)]
    public Vector2 turningSpeedSec;
    [Header("Y es horizontal e X es vertical")]
    [Range(-180, 180)]
    public float minHorizontal;
    [Range(-180, 180)]
    public float maxHorizontal;
    [Header("Y es horizontal e X es vertical")]
    [Range(-90, 90)]
    public float minVertical;
    [Range(-90, 90)]
    public float maxVertical;
    public float range;
    public GameObject body;
    public GameObject cannon;

    //Values
    public List<GameObject> targets;
    private GameObject target;
    private Vector2 turningSpeed;
    private float checkAgain;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        gyro = new GameObject();
        gyro.name = "gyroscope";
        gyro = Object.Instantiate(gyro, transform);
        cannonController = cannon.GetComponent<CannonController>();
        checkAgain = 1;
        sIC = gameObject.GetComponent<StructuralIntecrityController>();
        bulletspeed = cannonController.bulletspeed;
    }

    void Update() {
        if (sIC.alive) {
            turningSpeed = turningSpeedSec * Time.deltaTime;
            checkTargets();
            if (target != null) {
                Vector3 aimPoint = PhishicsMath.properAimPos(target, Vector3.Distance(transform.position, target.transform.position), bulletspeed);
                gyro.transform.LookAt(aimPoint);
                if (!checkviability(target, gyro.transform.localEulerAngles)) {
                    target = null;
                    return;
                }
                Vector2 gyroAngles = PhishicsMath.transV2AngleTo180(new Vector2(gyro.transform.localEulerAngles.x, gyro.transform.localEulerAngles.y));
                Vector2 cannonAngles = PhishicsMath.transV2AngleTo180(new Vector2(cannon.transform.localEulerAngles.x, body.transform.localEulerAngles.y));
                Vector2 angles = gyroAngles - cannonAngles;
                aimBody(angles);
                aimCannon(angles);
                fireCannon(angles);
            }
        }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void checkTargets() {
        checkAgain -= Time.deltaTime;
        if (checkAgain > 0) { return; }
        else { checkAgain = 1; }
        foreach (GameObject posTarget in targets) {
            gyro.transform.LookAt(posTarget.transform.position);
            if (checkviability(posTarget, gyro.transform.localEulerAngles)) {
                if(target != null) {
                    float distance = Vector3.Distance(transform.position, posTarget.transform.position);
                    if (Vector3.Distance(transform.position, target.transform.position) > distance && distance < range) {
                        target = posTarget;
                    }
                }
                else {
                    target = posTarget;
                }
            }
        }
    }

    private bool checkviability(GameObject target, Vector2 angles) {
        StructuralIntecrityController targetheal = target.GetComponent<StructuralIntecrityController>();
        if(targetheal == null) {
            return false;
        }
        else if (checkAngles(target, angles) && Vector3.Distance(transform.position, target.transform.position) <= range && target.GetComponent<StructuralIntecrityController>().alive) {
            return true;
        }
            return false;
    }

    private bool checkAngles(GameObject target, Vector2 angles) {
        angles = PhishicsMath.transV2AngleTo180(angles);    
        if ((angles.y < maxHorizontal && angles.y > minHorizontal) && (angles.x < -minVertical && angles.x > -maxVertical)) {
            return true;
        }
        return false;
    }

    private void aimBody(Vector2 turnAngles){
        if(turnAngles.y > turningSpeed.y) {
            body.transform.localEulerAngles += new Vector3(0, turningSpeed.y, 0);
        }
        else if (turnAngles.y > turningSpeed.y) {
            body.transform.localEulerAngles += new Vector3(0, -turningSpeed.y, 0);
        }
        else {
            body.transform.localEulerAngles += new Vector3(0, turnAngles.y, 0);
        }
    }

    private void aimCannon(Vector2 turnAngles) {
        if (turnAngles.x > turningSpeed.x) {
            cannon.transform.localEulerAngles += new Vector3(turningSpeed.x, 0, 0);
        }
        else if (turnAngles.x > turningSpeed.x) {
            cannon.transform.localEulerAngles += new Vector3(-turningSpeed.x, 0, 0);
        }
        else {
            cannon.transform.localEulerAngles += new Vector3(turnAngles.x, 0, 0);
        }
    }

    private void fireCannon(Vector2 angles){
        if((angles.x < 1 && angles.x > -1) && (angles.y < 1 && angles.y > -1)) {
            cannonController.shoot();
        }
    }

}
