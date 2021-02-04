using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants

    //Values
    [Header("menu de los ajustes")]
    public GameObject menuDeOpciones;
    [Header("el objeto nave que se usara")]
    public GameObject nave;
    [Header("posicion de la camara con respecto al centro del obj")]
    public Vector3 posOffset;

    private FighterController fController;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        getcontroller();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update() {
        moveCamera();
        movementSystem();
        if (Input.GetKeyDown(KeyCode.Escape)) { menuOpciones(); }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void getcontroller() {
        fController = nave.GetComponent<FighterController>();
    }

    private void moveCamera() {
        transform.eulerAngles = nave.transform.eulerAngles;
        Vector3 pos = nave.transform.position;
        pos += transform.forward * posOffset.z;
        pos += transform.up * posOffset.y;
        pos += transform.right * posOffset.x;
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
            Input.GetAxis("Acceleration")
            );
        fController.movement = movement;
    }

    private void fireSystem() {
        if (Input.GetAxis("FireCannon") == 1) { fController.fire = true; }
        else { fController.fire = false; }
    }

    private void menuOpciones() {
        if (menuDeOpciones.active) {
            menuDeOpciones.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1;
        }
        else {
            menuDeOpciones.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 0.2f;
        }
    }
}
