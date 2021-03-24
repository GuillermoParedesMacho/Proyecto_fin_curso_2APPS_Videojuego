using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Aliados alrededor de la fragata, si se salen del radio mueren
//Nave y fragata deben sobrevivir hasta llegar al objetivo X
public class Mission2Controller : MonoBehaviour
{
    
    //Nave que controla el jugador
    private UserController player;
    public ShipController frigate1;
    public ShipController frigate2;

    void Start(){
        player = GameObject.Find("User").GetComponent<UserController>();
    }

    private void Update() {
        if(!frigate1.alive && !frigate2.alive) {
            player.victoryMenu();
            gameObject.SetActive(false);
        }

        if (player.playerDead) {
            player.defeatMenu();
            gameObject.SetActive(false);
        }
    }
}
/*
    public GameObject torreta;
    public GameObject torreta1;
    public GameObject torreta2;
    public GameObject torreta3;
    public GameObject torreta4;
    public GameObject torreta5;

    public GameObject torreta6;
    public GameObject torreta7;
    public GameObject torreta8;
    public GameObject torreta9;


    private bool destroyedTurrets;
    private bool destroyedTurretsAllies;
    private bool noAllies;
    // Update is called once per frame
    void Update()
    {
    
        //Si todas las torretas estan destruidas
        if (torreta.GetComponent<StructuralIntecrityController>().maxHeal == 0 
            && torreta1.GetComponent<StructuralIntecrityController>().maxHeal == 0 
            && torreta2.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torreta3.GetComponent<StructuralIntecrityController>().maxHeal == 0 
            && torreta4.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torreta5.GetComponent<StructuralIntecrityController>().maxHeal == 0)
        {
            destroyedTurrets = true;
            print("Torretas destruidas");

        }
        if ( torreta6.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torreta7.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torreta8.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torreta9.GetComponent<StructuralIntecrityController>().maxHeal == 0
           )
        {
            destroyedTurretsAllies = true;
            print("Torretas aliadas destruidas");

        }
        //Si no quedan mas naves rebelde en el GameObject se gana el juego
        if (GameObject.FindGameObjectsWithTag("Rebeldes").Length == 0)
        {
            noAllies = true;
            print("No quedan rebeldes");

        }
        if (GameObject.FindGameObjectsWithTag("Aliados").Length == 0 || destroyedTurretsAllies == true)
        {
            print("No quedan aliados");
            SceneManager.LoadScene("MainMenu");

        }
        if (destroyedTurrets == true && noAllies == true)
        {
            print("Has ganado");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("MissionsMenu");
        }
       

    }
    */
