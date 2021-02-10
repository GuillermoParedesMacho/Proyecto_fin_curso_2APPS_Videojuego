using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPathFinder : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private FighterController fControler;

    //Values
    [Min(0)]
    public float yawAngle;

    public Vector2 shipGlovalAngle;
    public Vector2 endPointGlovalAngle;
    public Vector2 relativeAngles;
    public Vector3 endPoint;
    public Vector3 relativeDistance;
    public Vector3 relativeFoward;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        fControler = gameObject.GetComponent<FighterController>();
    }
    
    void Update() {
        relativeDistance = endPoint - transform.position;
        endPointGlovalAngle = PhishicsMath.V3relativePosToV2ang(relativeDistance);

        relativeFoward = transform.position + 10 * transform.forward;
        Vector3 relativeFowardtemp = relativeFoward - transform.position;

        shipGlovalAngle = PhishicsMath.V3relativePosToV2ang(relativeFowardtemp);
        relativeAngles = endPointGlovalAngle - shipGlovalAngle;

        Vector2 rolledRelativeAngles = rotateAngles(relativeAngles);

        angleAdjustments(rolledRelativeAngles);
        Debug.Log(rolledRelativeAngles + " - " + transform.eulerAngles.z);
        Debug.Log(relativeAngles + " - " + transform.eulerAngles);
        //fControler.movement = new Vector3(0, 0, 1);
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    void OnDrawGizmosSelected() {
        //mostrar movimiento
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, endPoint);
        Gizmos.DrawLine(transform.position, relativeFoward);
    }

    //obtener el valor de rotacion local para calcularlo con la rotacion del obj
    void angleAdjustments(Vector2 angles) {
        Vector3 rotate = Vector3.zero;
        if(angles.x > yawAngle) { rotate.z = -0.5f; }
        else if(angles.x < -yawAngle) { rotate.z = 0.5f; }
        else {
            if (angles.y > 5) { rotate.x = -0.5f; }
            else if (angles.y < -5) { rotate.x = 0.5f; }
        }
        fControler.rotation = rotate;
        Debug.Log(rotate);
    }

    //rotar los angulos con respecto al roll (eje z)
    Vector2 rotateAngles(Vector2 angles) {
        Vector2 rolledAngles = Vector2.zero;
        float normalRoll = transform.eulerAngles.z / 90;
        if (normalRoll < 1) {
            rolledAngles = new Vector2(
                angles.x * (1 - normalRoll) + angles.y * normalRoll,
                angles.y * (1 - normalRoll) + angles.x * normalRoll * -1
            );
        }
        else if (normalRoll < 2) {
            normalRoll -= 1;
            rolledAngles = new Vector2(
                angles.y * (1 - normalRoll) + angles.x * normalRoll * -1,
                -angles.x * (1 - normalRoll) + angles.y * normalRoll * -1
            );
        }
        else if (normalRoll < 3) {
            normalRoll -= 2;
            rolledAngles = new Vector2(
                -angles.x * (1 - normalRoll) + angles.y * normalRoll * -1,
                -angles.y * (1 - normalRoll) + angles.x * normalRoll
            );
        }
        else if (normalRoll < 4) {
            normalRoll -= 3;
            rolledAngles = new Vector2(
                -angles.y * (1 - normalRoll) + angles.x * normalRoll,
                angles.x * (1 - normalRoll) + angles.y * normalRoll
            );
        }
        return rolledAngles;
    }
}
