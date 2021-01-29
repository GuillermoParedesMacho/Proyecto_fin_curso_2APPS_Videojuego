using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants

    //Values
    public GameObject ship;
    private FighterController fController;
    public Vector3 offset;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        getcontroller();
    }

    
    void Update() {
        moveCamera();
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void getcontroller() {
        fController = ship.GetComponent<FighterController>();
    }

    private void moveCamera() {
        transform.eulerAngles = ship.transform.eulerAngles;
        Vector3 pos = ship.transform.position;
        pos += transform.forward * offset.z;
        pos += transform.up * offset.y;
        pos += transform.right * offset.x;
        transform.position = pos;
    }
}
