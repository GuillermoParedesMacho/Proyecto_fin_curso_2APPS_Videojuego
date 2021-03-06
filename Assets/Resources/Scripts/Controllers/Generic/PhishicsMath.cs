using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishicsMath{
    //math to do phishics and other stuff that requires fhishics calculations on script

    //math to get the newtowns necesary for aceleration
    public static float msToNewtowns(float ms, float mass, float drag) {
        return msToNewtowns(ms, mass, Time.fixedDeltaTime, drag);
    }
    public static float msToNewtowns(float ms, float mass, float time, float drag) {
        if(drag > 0) { return ((ms * mass) / Time.fixedDeltaTime) * time; }
        return ((ms * mass) / Time.fixedDeltaTime) * time;
    }

    //math to get force neceary for rotation
    public static float degreSToNewtows(float degreS, float mass, float drag) {
        return degreSToNewtows(degreS, mass, Time.fixedDeltaTime, drag);
    }
    public static float degreSToNewtows(float degreS, float mass, float time, float drag) {
        if (drag > 0) { return ((degreS * mass) / Time.fixedDeltaTime) * time; }
        return ((degreS * mass) / Time.fixedDeltaTime) * time;
    }

    //Indica la distancia recorrida en la cantidad dada de tiempo
    public static float distanceTraveled(Vector3 speed, float time) {
        float linearSpeed = Mathf.Sqrt(Mathf.Pow(speed.x,2) + Mathf.Pow(speed.y, 2) + Mathf.Pow(speed.z, 2));
        return linearSpeed * time * 1.2f;
    }

    //Calcular posicion en la que estara
    public static Vector3 properAimPos(GameObject target, float distance, float bulletspeed) {
        Vector3 output = target.transform.position;
        Rigidbody rBody = target.GetComponent<Rigidbody>();
        if (rBody != null) { output += rBody.velocity * (distance / bulletspeed); }
        return output;
    }

    //Obtiene dos angulos a traves de un Vector3
    public static Vector2 V3relativePosToV2ang(Vector3 relativePos) {
        float longituzXZ = Mathf.Sqrt((Mathf.Pow(relativePos.x, 2) + Mathf.Pow(relativePos.z, 2)));
        if(relativePos.z < 0) { longituzXZ *= -1; }
        Vector2 angle = new Vector2(
            getAngleByTG(relativePos.x , relativePos.z),
            getAngleByTG(relativePos.y , longituzXZ)
        );
        if (relativePos.z < 0) {
            angle.x *= -1;
            if (angle.x >= 0) { angle.x = 180 - angle.x; }
            else { angle.x = -180 - angle.x; }
            angle.y *= -1;
            if (angle.y >= 0) { angle.y = 180 - angle.y; }
            else { angle.y = -180 - angle.y; }
        }
        return angle;
    }
    public static float getAngleByTG(float co, float cc) {
        return Mathf.Rad2Deg * Mathf.Atan(co / cc);
    }

    //Transformar anguos to -180 180
    public static Vector2 transV2AngleTo180(Vector2 angles) {
        angles.x = transAngleTo180(angles.x);
        angles.y = transAngleTo180(angles.y);
        return angles;
    }
    public static float transAngleTo180(float angle) {
        if (angle > 180) {
            while (angle > 180){
                angle -= 360;
            }
        }
        else if (angle < -180) {
            while (angle > 180) {
                angle += 360;
            }
        }
        return angle;
    }
}
