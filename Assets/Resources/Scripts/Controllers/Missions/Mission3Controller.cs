using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Aliados alrededor de la fragata, si se salen del radio mueren
//Nave y fragata deben sobrevivir hasta llegar al objetivo X
public class Mission3Controller : MonoBehaviour
{
    //Nave que controla el jugador
    private UserController player;
    public ShipController frigate1;
    public ShipController frigate2;
    public ShipController frigate3;

    void Start() {
        player = GameObject.Find("User").GetComponent<UserController>();
    }

    private void Update() {
        if (!frigate1.alive && !frigate2.alive && !frigate3.alive) {
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
    //Torretas alidas
    public GameObject torretaRebelde;
    public GameObject torretaRebelde1;
    public GameObject torretaRebelde2;
    public GameObject torretaRebelde3;
    public GameObject torretaRebelde4;
    public GameObject torretaRebelde5;
    public GameObject torretaRebelde6;
    public GameObject torretaRebelde7;
    public GameObject torretaRebelde8;


    public GameObject torretaAliada;
    public GameObject torretaAliada1;
    public GameObject torretaAliada2;
    public GameObject torretaAliada3;
    public GameObject torretaAliada4;
    public GameObject torretaAliada5;
    public GameObject torretaAliada6;
    public GameObject torretaAliada7;
    public GameObject torretaAliada8;
    public GameObject torretaAliada9;
    public GameObject torretaAliada10;
    public GameObject torretaAliada11;
    // Update is called once per frame
    void Update()
    {

        //Si todas las torretas estan destruidas se gana
        if (torretaRebelde.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde1.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde2.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde3.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde4.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde5.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde6.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde7.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaRebelde8.GetComponent<StructuralIntecrityController>().maxHeal == 0)
        {
            destroyedTurrets = true;
            print("Torretas destruidas");

        }
        if (torretaAliada.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada1.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada2.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada3.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada4.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada5.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada6.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada7.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada8.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada9.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada10.GetComponent<StructuralIntecrityController>().maxHeal == 0
            && torretaAliada11.GetComponent<StructuralIntecrityController>().maxHeal == 0)
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
            print("Derrota");
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
