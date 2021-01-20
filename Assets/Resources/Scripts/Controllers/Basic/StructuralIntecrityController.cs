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

    public void damage(float damageHitpoints, float armourPiercing) {
        //Math the armour piercing vs armour and remove heal properly
        if (damageHitpoints < 0) { damageHitpoints *= -1; }
        var armourPiercingFactor = armourPiercing / armour;
        if(armourPiercingFactor > maxArmourPiercingFactor) { armourPiercingFactor = maxArmourPiercingFactor; }
        heal -= damageHitpoints * armourPiercingFactor;

        //update heal data
        healUpdate();
    }

    public void healUpdate() {
        //update values dependent of heal
        if(heal <= 0) { alive = false; }
    }

}
