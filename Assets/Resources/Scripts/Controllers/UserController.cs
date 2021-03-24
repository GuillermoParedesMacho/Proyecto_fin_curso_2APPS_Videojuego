using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserController : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    public Image healBar;
    public Image speedBar;
    public GameObject fowardImage;
    public GameObject bacwardImage;
    public GameObject victory;
    public GameObject defeat;
    public GameObject turnback;

    //Values
    [Header("menu de los ajustes")]
    public GameObject menuDeOpciones;
    [Header("el objeto nave que se usara")]
    public GameObject nave;
    [Header("posicion de la camara con respecto al centro del obj")]
    public Vector3 posOffset;

    public bool playerDead;

    private FighterController fController;
    private float timeToRespawn;
    private float movingToPos;
    private Vector3 startPoint;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        timeToRespawn = 1;
        movingToPos = 1;
        startPoint = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        playerDead = false;
        healBar.fillAmount = 0;
        speedBar.fillAmount = 0;
        fowardImage.SetActive(false);
        bacwardImage.SetActive(false);
    }

    
    void Update() {
        if (nave.GetComponent<StructuralIntecrityController>().alive && movingToPos <= 0) {
            moveCamera();
            movementSystem();
            fireSystem();
            updateUHD();
        }
        else if(movingToPos > 0) {
            if(timeToRespawn <= 0) {
                respawn();
            }
            else {
                timeToRespawn -= Time.deltaTime;
            }
        }
        else {
            searchNewHost();
            healBar.fillAmount = 0;
            speedBar.fillAmount = 0;
            fowardImage.SetActive(false);
            bacwardImage.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { menuOpciones(); }
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    private void searchNewHost() {
        SensorsSystem sensor = nave.GetComponent<SensorsSystem>();
        sensor.scan();
        if(sensor.allys.Count > 0) {
            foreach(GameObject possibleNave in sensor.allys) {
                if (possibleNave.GetComponent<StructuralIntecrityController>().alive) {
                    nave = possibleNave;
                    timeToRespawn = 3;
                    movingToPos = 1;
                    startPoint = transform.position;
                }
            }
            playerDead = false;
        }
        else {
            playerDead = true;
        }
    }

    private void respawn() {
        gameObject.transform.LookAt(nave.transform.position);
        Vector3 v3Distance = nave.transform.position - startPoint;
        transform.position = startPoint + v3Distance * (1 - movingToPos);
        movingToPos -= (Time.deltaTime / 3);
        if(movingToPos <= 0) {
            getcontroller();
        }
    }

    private void getcontroller() {
        fController = nave.GetComponent<FighterController>();
        nave.GetComponent<StructuralIntecrityController>().deactiveIndicators();
        nave.GetComponent<FighterIA>().enabled = false;
        nave.GetComponent<GyroShipPathFinder>().enabled = false;
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

    public void menuOpciones() {
        if(menuDeOpciones != null) {
            if (menuDeOpciones.activeSelf) {
                menuDeOpciones.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
            else {
                menuDeOpciones.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void updateUHD() {
        StructuralIntecrityController sIC = fController.GetComponent<StructuralIntecrityController>();
        healBar.fillAmount = sIC.heal / sIC.maxHeal;

        float speed = fController.GetComponent<ShipMovementController>().inFowardBackwards;
        fowardImage.SetActive(speed > 0);
        bacwardImage.SetActive(speed < 0);
        if (speed < 0) { speed = speed * -1; }
        speedBar.fillAmount = speed;
    }

    public void victoryMenu() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        victory.SetActive(true);
    }

    public void defeatMenu() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        defeat.SetActive(true);
    }

    public void turnBackIndicator(bool active) {
        defeat.SetActive(active);
    }
}
