using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishicsMath{
    //math to do phishics and other stuff that requires fhishics calculations on script

    //math to get the newtowns necesary for aceleration
    public static float msToNewtowns(float ms, float mass) {
        return msToNewtowns(ms, mass, 1);
    }
    public static float msToNewtowns(float ms, float mass, float time) {
        return ms * mass * time;
    }

    public static float degreSToNewtows(float degreS, float mass) {
        return degreSToNewtows(degreS, mass, 1);
    }
    public static float degreSToNewtows(float degreS, float mass, float time) {
        return degreS * mass * time;
    }

    public static float distanceTraveled(Vector3 speed, float time) {
        float linearSpeed = Mathf.Sqrt(Mathf.Pow(speed.x,2) + Mathf.Pow(speed.y, 2) + Mathf.Pow(speed.z, 2));
        return linearSpeed * time * 1.2f;
    }
}
