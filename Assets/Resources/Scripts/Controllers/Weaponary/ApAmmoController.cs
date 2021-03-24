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
    [Header("opciones sobre el daño que puede hacer la vala")]
    public float damage;
    public float ap;
    [Header("Hit indicator sample")]
    public GameObject hitIndicatorSample;

    [HideInInspector]
    public GameObject launcher;
    private float bulletSpeed;
    private Rigidbody rBody;
    private Rigidbody launcherRgbody;

    //Values
    private float timeOfLife;
    private bool collision;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void Awake() {
        rBody = gameObject.GetComponent<Rigidbody>();
        bulletSpeed = PhishicsMath.msToNewtowns(bulletSpeedMs, rBody.mass, 0);
        launcherRgbody = launcher.GetComponent<Rigidbody>();
        hitIndicatorSample.SetActive(false);
    }

    private void OnEnable() {
        rBody.velocity = transform.forward * bulletSpeed;
        if(launcherRgbody != null) {
            rBody.velocity += launcherRgbody.velocity;
        }
        //rBody.AddForce();
        timeOfLife = lifeTime;
    }

    private void Update() {
        timeOfLife -= Time.deltaTime;
        if(timeOfLife < 0) {
            end();
        }
        float distance = PhishicsMath.distanceTraveled(rBody.velocity, Time.deltaTime);
        //checkHit(transform.forward * -1, distance * 3);
        checkHit(transform.forward, distance * 3);
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void checkHit (Vector3 direction,float distance) {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, direction, distance);
        
        foreach(RaycastHit hit in hits) {
            GameObject hitted = hit.transform.gameObject;
            if (hitted != launcher) {
                StructuralIntecrityController siController = hitted.GetComponent<StructuralIntecrityController>();
                if (siController != null) {
                    siController.damage(damage, ap, launcher);
                    endWithPasion(hit);
                    return;
                }
            }
        }
        if(hits.Length > 0) {
            endWithPasion(hits[0]);
        }
    }

    private void endWithPasion(RaycastHit hit) {
        GameObject instantiate = Instantiate(hitIndicatorSample);
        instantiate.transform.position = hit.point;
        instantiate.transform.eulerAngles = gameObject.transform.eulerAngles;
        instantiate.SetActive(true);
        end();
    }

    private void end() {
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
