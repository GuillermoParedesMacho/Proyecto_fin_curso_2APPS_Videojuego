using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroShipPathFinder: MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private GameObject gyro;
    private FighterController fControler;
    [Min(0)]
    public float rangeOfComplete;

    //Values
    private List<Vector3> path;
    private GameObject hitIgnore;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        gyro = new GameObject();
        gyro.name = "gyroscope";
        gyro = Object.Instantiate(gyro, transform);
        fControler = gameObject.GetComponent<FighterController>();
        path = new List<Vector3>();
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    public void moveToPos(GameObject pos, float rSpeed) {
        hitIgnore = pos;
        moveToPos(pos.transform.position, rSpeed);
    }
    public void moveToPos(Vector3 pos, float rSpeed, GameObject ignore) {
        hitIgnore = ignore;
        moveToPos(pos, rSpeed);
    }
    public void moveToPos(Vector3 pos, float rSpeed) {
        gyro.transform.LookAt(pos);
        checkPath(pos);
        
        Vector3 nextPoint = pos;
        if(path.Count != 0) { nextPoint = path[0]; }
        gyro.transform.LookAt(nextPoint);
        checkPath(pos);

        showPath(pos);

        if(Vector3.Distance(gameObject.transform.position, nextPoint) >= rangeOfComplete) {
            adjustTrayectory(gyro.transform.localEulerAngles);
            adjustSpeed(rSpeed);
        }
        else {
            adjustSpeed(0);
            fControler.rotation = Vector3.zero;
            if (path.Count != 0){ path.RemoveAt(0); }
        }
        
    }

    //movement system
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
    }

    private void adjustSpeed(float rSpeed) {
        Vector3 movement = Vector3.zero;
        movement.z = rSpeed - fControler.fowardRSpeed;
        fControler.movement = movement;
    }

    //path finder system
    private void checkPath(Vector3 endPoint) {
        RaycastHit rayCast;
        if (Physics.Linecast(transform.position, endPoint, out rayCast)) {
            if(rayCast.transform.gameObject == hitIgnore) {
                if (path.Count != 0) {
                    path.Clear();
                }
            }
            else if (path.Count == 0) {
                makePath(endPoint, rayCast);
            }
        }
        else {
            if(path.Count != 0) {
                path.Clear();
            }
        }

    }

    private void makePath(Vector3 endPoint, RaycastHit rayCast) {
        //la ruta debe evitar obstaculos y terminar en un punto en el que se pueda llegar al destino
        Vector3 prevPoint = transform.position;
        Vector3 offset = 10 * gyro.transform.up;
        Vector3 hitPoint = rayCast.point;
        GameObject hit = rayCast.transform.gameObject;
        Vector3 tempPoint = hitPoint;
        RaycastHit rayCastPath;
        bool flag = false;
        int x = 0;

        do {
            x++;
            tempPoint += offset;
            flag = false;

            //hit from prev point to new one
            if (Physics.Linecast(prevPoint, tempPoint, out rayCastPath)) {
                //Debug.DrawLine(prevPoint, tempPoint, Color.red, 5, true);
                if (rayCast.transform.gameObject != hitIgnore) {
                    if (rayCastPath.transform.gameObject != rayCastPath.transform.gameObject) {
                        tempPoint = rayCast.point;
                        rayCast = rayCastPath;
                    }
                    flag = true;
                }
            }

            //hit from new point to end pos
            if (Physics.Linecast(tempPoint - 1 * gyro.transform.forward, endPoint, out rayCastPath)) {
                //Debug.DrawLine(tempPoint, endPoint, Color.red, 5, true);
                if (rayCastPath.transform.gameObject != hitIgnore) {
                    if (rayCast.transform.gameObject != rayCastPath.transform.gameObject) {
                        path.Add(tempPoint);
                        prevPoint = tempPoint;
                        tempPoint = rayCastPath.point;
                        rayCast = rayCastPath;
                        offset *= -1;
                    }
                        flag = true;
                }
            }

            if (!flag) {
                //add last tempoint
                path.Add(tempPoint);
            }

            if(x > 100) {
                Debug.LogWarning("Failed to create path");
                break;
            }

        } while (flag);
    }

    //other math stuff
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

    private void showPath(Vector3 endpoint) {
        Debug.DrawLine(transform.position, transform.position + 10 * transform.forward, Color.white, 0.01f, true);
        Vector3 prevPoint = transform.position;
        for(int x = 0; x < path.Count; x++) {
            Debug.DrawLine(prevPoint, path[x], Color.white, 0.01f, true);
            prevPoint = path[x];
        }
        Debug.DrawLine(prevPoint, endpoint, Color.white, 0.01f, true);
    }
}