using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    public GameObject ammoSample;
    public float fireRateRPM;
    public Vector3[] location;
    private float forceInNewtowns;

    //Values
    private GameObject[] ammo;
    private int nextToShoot;
    private int nextLocation;
    private float timeBtwShoots;
    private float timeForNextShoot;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void Start(){
        createAmmo();
        timeBtwShoots = 60 / fireRateRPM;
        nextLocation = 0;
        if(location.Length == 0) {
            location = new Vector3[] { Vector3.zero };
        }
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
    }

    public void shoot() {
        if(timeForNextShoot <= 0) {
            timeForNextShoot = timeBtwShoots;
            launchBullet();
            nextToShoot++;
            if (nextToShoot >= ammo.Length) {
                nextToShoot = 0;
            }
        }
    }

    private void launchBullet() {
        GameObject fire = ammo[nextToShoot];
        setBulletPosition(fire);
        fire.SetActive(true);
    }

    private void setBulletPosition(GameObject fire) {
        fire.transform.position = gameObject.transform.position;
        fire.transform.eulerAngles = gameObject.transform.eulerAngles;
        fire.transform.Translate(location[nextLocation]);
        nextLocation++;
        if(nextLocation >= location.Length) {
            nextLocation = 0;
        }
    }
}
