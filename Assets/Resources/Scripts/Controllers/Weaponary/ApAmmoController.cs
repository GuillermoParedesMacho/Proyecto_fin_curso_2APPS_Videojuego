using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApAmmoController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    [Header("tiempo que tardara la vala en desaparecer")]
    public float lifeTime;
    [Header("velocidad de la vala")]
    public float bulletSpeedMs;
    [Space(5)]
    [Header("opciones sobre el da�o que puede hacer la vala")]
    public float damage;
    public float ap;

    [HideInInspector]
    public GameObject launcher;
    private float bulletSpeed;
    private Rigidbody rBody;
    private Rigidbody launcherRgbody;

    //Values
    private float timeOfLife;
    private RaycastHit rayCast;
    private bool hit;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void Awake() {
        rBody = gameObject.GetComponent<Rigidbody>();
        bulletSpeed = PhishicsMath.msToNewtowns(bulletSpeedMs, rBody.mass, 0);
        launcherRgbody = launcher.GetComponent<Rigidbody>();
    }

    private void Start() {
        
    }

    private void OnEnable() {
        rBody.velocity = launcherRgbody.velocity + transform.forward * bulletSpeed;
        //rBody.AddForce();
        timeOfLife = lifeTime;
        hit = false;
    }

    private void Update() {
        timeOfLife -= Time.deltaTime;
        if(timeOfLife < 0 || hit) {
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
        float distance = PhishicsMath.distanceTraveled(rBody.velocity, Time.deltaTime);
        checkHit(transform.forward * -1, distance*3);
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void checkHit (Vector3 direction,float distance) {
        if (Physics.Raycast(transform.position, direction, out rayCast, distance)) {
            GameObject hitted = rayCast.transform.gameObject;
            if (hitted != launcher) {
                StructuralIntecrityController siController = hitted.GetComponent<StructuralIntecrityController>();
                if (siController != null) {
                    siController.damage(damage, ap, launcher);
                    //moveToProperHitpoint(direction, distance);
                    
                }
                hit = true;
            }
        }
    }

    private void moveToProperHitpoint(Vector3 direction, float distance) {
        transform.Translate(direction * distance);
        if (Physics.Raycast(transform.position, direction * -1, out rayCast, distance)) {
            transform.position = rayCast.point;
        }
    }
}
