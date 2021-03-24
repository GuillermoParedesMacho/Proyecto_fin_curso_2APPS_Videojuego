using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private GameObject gyro;
    private SensorsSystem sensor;
    private Vector2 rotationSpeed;

    public List<GameObject> checkPoints;
    public float Speed;
    public float distanceOfComplete;
    public Vector2 rotationSpeedSec;
    public TurretController[] Turrets;
    public StructuralIntecrityController[] ShipTrusters;

    //Values
    public bool alive;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        gyro = new GameObject();
        gyro.name = "gyroscope";
        gyro = Object.Instantiate(gyro, transform);
        sensor = gameObject.GetComponent<SensorsSystem>();
        alive = true;
    }
    
    void Update() {
        if (alive) {
            rotationSpeed = rotationSpeedSec * Time.deltaTime;
            updateTurrets();
            if (checkPoints.Count > 0) {
                if (checkPoints[0].activeSelf) {
                    Vector3 position = checkPoints[0].transform.position;
                    Debug.DrawLine(transform.position, position, Color.white, 0.01f, true);
                    movementSystem(position);
                    if (Vector3.Distance(transform.position, position) < distanceOfComplete) { checkPoints.RemoveAt(0); }
                }
            }
        }
        checkalive();
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    private void updateTurrets() {
        sensor.scan();
        foreach (TurretController turret in Turrets) {
            turret.targets = sensor.enemys;
        }
    }

    private void movementSystem(Vector3 movPoint) {
        if (!checkTrustAlive()) {
            return;
        }

        gyro.transform.LookAt(movPoint);
        Vector2 turningAngles = PhishicsMath.transV2AngleTo180(new Vector2(gyro.transform.localEulerAngles.x, gyro.transform.localEulerAngles.y));
        
        Vector3 angles = new Vector3(angleLimiters(rotationSpeed.x, turningAngles.x), angleLimiters(rotationSpeed.y, turningAngles.y), 0);
        transform.eulerAngles += angles;

        if(turningAngles.x < 0.1 && turningAngles.y < 0.1 && turningAngles.x > -0.1 && turningAngles.y > -0.1) {
            transform.position += (Speed * Time.deltaTime) * transform.forward;
        }
    }

    private float angleLimiters(float limiter, float ammount) {
        if (ammount > limiter) { return limiter; }
        else if (ammount < -limiter) { return -limiter; }
        return ammount;
    }

    private bool checkTrustAlive() {
        foreach(StructuralIntecrityController ShipTruster in ShipTrusters) {
            if (ShipTruster.alive) {
                return true;
            }
        }
        return false;
    }

    private void checkalive() {
        if (checkTrustAlive()) { return; }
        foreach(TurretController turret in Turrets) {
            StructuralIntecrityController tSIC = turret.GetComponent<StructuralIntecrityController>();
            if (tSIC != null && tSIC.alive) {
                return;
            }
        }
        alive = false;
    }
}
