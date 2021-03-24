using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Aliados alrededor de la fragata, si se salen del radio mueren
//Nave y fragata deben sobrevivir hasta llegar al objetivo X
public class Mission1Controller : MonoBehaviour {
    public GameObject Center;
    public float maxDistance;

    public enum Stages { wawe1, wawe2, wawe3};
    public Stages stage;

    public ObjetivesClass[] objetives;
    public GameObject[] Stage2Ships;
    public GameObject[] Stage3Ships;
    public ShipController frigate;

    private UserController player;
    
    private void Start() {
        player = GameObject.Find("User").GetComponent<UserController>();
    }

    private void Update() {
        if (stage == Stages.wawe1) {
            stage1();
        }
        else if (stage == Stages.wawe2) {
            stage2();
        }
        else if (stage == Stages.wawe3) {
            stage3();
        }

        if (player.playerDead) {
            player.defeatMenu();
            gameObject.SetActive(false);
        }

        player.turnBackIndicator(Vector3.Distance(player.transform.position, Center.transform.position) > maxDistance * 0.95);
        if (Vector3.Distance(player.transform.position, Center.transform.position) > maxDistance) {
            player.nave.GetComponent<StructuralIntecrityController>().instakill();
        }

        if (!frigate.alive) {
            player.defeatMenu();
            gameObject.SetActive(false);
        }
    }

    private void stage1() {
        if (objetives[0].failed) {
            foreach(GameObject nave in Stage2Ships) {
                nave.SetActive(true);
            }
            stage = Stages.wawe2;
        }
    }

    private void stage2() {
        if (objetives[1].failed) {
            foreach (GameObject nave in Stage3Ships) {
                nave.SetActive(true);
            }
            stage = Stages.wawe3;
        }
    }

    private void stage3() {
        if (objetives[2].failed) {
            player.victoryMenu();
            gameObject.SetActive(false);
        }
    }

}
/*
public GameObject NavePlayer;
public GameObject ObjetivoX;
public GameObject menuDeOpciones;
private float FragataHealth;
public float distanceShipToFragata;
public float distanceShipToObjetive;
// Update is called once per frame
void Update() {
    distanceShipToFragata = Vector3.Distance(Fragata.transform.position, NavePlayer.transform.position);
    distanceShipToObjetive = Vector3.Distance(Fragata.transform.position, ObjetivoX.transform.position);

    // print("Distance from frigate to player: " + distanceShipToFragata);
    print("Distance from frigate to objetive: " + distanceShipToObjetive);

    //Si la distancia al objetivo es menor a X se gana
    if (distanceShipToObjetive < 50) {
        //VICTORIA
        print("Has ganado");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MissionsMenu");

    }
    //Si la distancia de la nave a la fragata es mayor que X muere el jugador
    if (distanceShipToFragata > maxDistance) {
        //MUERTE
        print("Has muerto");
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        // SceneManager.LoadScene("MainMenu");
        NavePlayer.GetComponent<StructuralIntecrityController>().heal = 0;
        print(NavePlayer.GetComponent<StructuralIntecrityController>().heal);

    }
    //Si no quedan mas naves alidas en el GameObject se pierde el juego
    if (GameObject.FindGameObjectsWithTag("Aliados").Length == 0) {
        // Do something
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");

        //DERROTA
        print("Has perdido");
    }

}
*/