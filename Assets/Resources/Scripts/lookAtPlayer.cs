using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtPlayer : MonoBehaviour
{
    GameObject player;
    
    void Start() {
        player = GameObject.Find("User");
    }
    
    void Update() {
        if (player != null) {
            gameObject.transform.LookAt(player.transform);
        }
    }
}
