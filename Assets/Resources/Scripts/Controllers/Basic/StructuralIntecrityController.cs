using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuralIntecrityController : MonoBehaviour{
    //---data--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    //Constants

    private float maxHeal;
    private float maxArmour;
    private float maxArmourPiercingFactor;

    //Values

    [HideInInspector]
    public bool alive;
    [Space(5)]
    [Min(0)]
    public float heal;
    [Min(0)]
    public float armour;

    //---main scripr--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--
    
    private void Start(){
        maxHeal = heal;
        maxArmour = armour;
        maxArmourPiercingFactor = 1;
        alive = true;
    }

    //---functions--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--/--

    public void damage(float damageHitpoints, float armourPiercing, GameObject hitter) {
        if (!alive) { return; }

        //Math the armour piercing vs armour and remove heal properly
        if (damageHitpoints < 0) { damageHitpoints *= -1; }
        var armourPiercingFactor = armourPiercing / armour;
        if(armourPiercingFactor > maxArmourPiercingFactor) { armourPiercingFactor = maxArmourPiercingFactor; }
        float damage = damageHitpoints * armourPiercingFactor;
        heal -= damage;

        //update heal data
        healUpdate();

        //report
        Debug.Log(hitter.name + " hitted " + gameObject.name + " dealing " + damage + " damage");
    }

    public void healUpdate() {
        //update values dependent of heal
        if(heal <= 0) { alive = false; }
    }

}
