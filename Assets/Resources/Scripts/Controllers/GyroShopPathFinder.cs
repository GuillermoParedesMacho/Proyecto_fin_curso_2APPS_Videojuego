using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroShopPathFinder: MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private GameObject gyro;
    private FighterController fControler;

    //Values

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        gyro = new GameObject();
        gyro.name = "gyroscope";
        gyro = Object.Instantiate(gyro, transform);
        fControler = gameObject.GetComponent<FighterController>();
    }

    void Update() {
        moveToPos(new Vector3(100, 50, 50));
        makelines();
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    public void moveToPos(Vector3 pos) {
        gyro.transform.LookAt(pos);
        adjustTrayectory(gyro.transform.localEulerAngles);
    }

    private void adjustTrayectory(Vector3 angles) {
        Vector3 rotate = Vector3.zero;
        Vector3 rAngles = adjustAngles(angles);

        if ((angles.y > 20 && angles.y < 160) || (angles.y > 200 && angles.y < 340)) {
            rotate.z = rAngles.y * -0.2f;
            if (angles.x > 15 && angles.x < 345) { rotate.x = rAngles.x * 0.3f; }
        }
        else if (angles.x > 15 && angles.x < 345) { rotate.x = rAngles.x * 0.3f; }
        else {
            rotate.y = rAngles.y * 0.2f;
            rotate.x = rAngles.x * 0.3f;
        }

        fControler.rotation = rotate;

        //temp
        fControler.movement = new Vector3(0, 0, 1);
    }

    private Vector3 adjustAngles(Vector3 angles) {
        angles = new Vector3(
            adjustAngle(angles.x),
            adjustAngle(angles.y),
            adjustAngle(angles.z)
        );
        return angles;
    }

    private float adjustAngle(float angle) {
        if (angle > 180) { angle -= 360; }
        return angle;
    }

    private void makelines() {
        Debug.DrawLine(transform.position, new Vector3(100, 50, 50), Color.white, 0.01f, true);
        Debug.DrawLine(transform.position, transform.position + 10 * transform.forward, Color.white, 0.01f, true);
    }
}