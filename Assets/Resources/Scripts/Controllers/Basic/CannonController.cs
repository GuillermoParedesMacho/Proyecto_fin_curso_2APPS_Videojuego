using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    public GameObject ammoSample;
    public float fireRateRPM;
    public float bulletSpeedMs;
    public Vector3 location;
    private float forceInNewtowns;

    //Values
    private GameObject[] ammo;
    private int nextToShoot;
    private float timeBtwShoots;
    private float timeForNextShoot;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void Start(){
        createAmmo();
        timeBtwShoots = 60 / fireRateRPM;
        forceInNewtowns = PhishicsMath.msToNewtowns(bulletSpeedMs, ammoSample.GetComponent<Rigidbody>().mass);
    }

    private void Update() {
        if(timeForNextShoot > 0) {
            timeForNextShoot -= Time.deltaTime;
        }
    }
    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void createAmmo() {
        int ammoNeeded = Mathf.CeilToInt(ammoSample.GetComponent<ApAmmoController>().lifeTime / (60 / fireRateRPM));
        ammo = new GameObject[ammoNeeded];
        for (int x = 0; x < ammoNeeded; x++) {
            ammo[x] = Instantiate(ammoSample);
            ammo[x].SetActive(false);
        }
        Debug.Log(gameObject.name + " generated " + ammoNeeded + " bullets of the sample: " + ammoSample.name);
    }

    public void shoot() {
        if(timeForNextShoot <= 0) {
            timeForNextShoot = timeBtwShoots;
            launchBullet();
            nextToShoot++;
            Debug.Log("fired " + nextToShoot);
            if (nextToShoot >= ammo.Length) {
                nextToShoot = 0;
            }
        }
    }

    private void launchBullet() {
        GameObject fire = ammo[nextToShoot];
        fire.transform.position = gameObject.transform.position;
        fire.transform.Rotate(gameObject.transform.eulerAngles);
        fire.SetActive(true);
    }
}
