using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorsSystem : MonoBehaviour {
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants
    private LayerMask ally;
    private LayerMask enemy;

    //Values
    public List<GameObject> allys;
    public List<GameObject> enemys;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    void Start() {
        identifyTeam();
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    public void scan() {
        Collider[] raw = Physics.OverlapSphere(transform.position, 1200);
        allys.Clear();
        enemys.Clear();
        
        foreach(Collider data in raw) {
            if(data.gameObject.layer == ally) {
                allys.Add(data.gameObject);
            }
            else if(data.gameObject.layer == enemy) {
                enemys.Add(data.gameObject);
            }
        }
    }

    private void identifyTeam() {
        if(gameObject.layer == 20) {
            ally = 20;
            enemy = 21;
        }
        else if(gameObject.layer == 21) {
            ally = 21;
            enemy = 20;
        }
        else {
            Debug.LogError("No team dettected, select layer team1(20) or team2(21)");
        }
    }

}
