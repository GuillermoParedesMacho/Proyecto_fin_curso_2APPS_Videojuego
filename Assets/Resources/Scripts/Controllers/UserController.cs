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

        //temporal
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update() {
        moveCamera();
        movementSystem();

        //temporal
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
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

    private void movementSystem() {
        Vector3 rotation = new Vector3(
            Input.GetAxis("Pitch") - Input.GetAxis("MouseY"),
            Input.GetAxis("Yaw") + Input.GetAxis("MouseX"),
            Input.GetAxis("Roll")
            );
        fController.rotation = rotation;
        Vector3 movement = new Vector3(
            Input.GetAxis("strafeX"),
            Input.GetAxis("strafeY"),
            fController.movement.z
            );
        fController.movement = movement;
        fController.propIncrease(Input.GetAxis("Acceleration"));

        if(Input.GetAxis("FireCannon") == 1) { fController.fire = true; }
        else { fController.fire = false; }
        
    }
}
