using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Col : MonoBehaviour {

    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Coin")
        {
            //increase score
            //increase coin collection
            //play audio effect
                Destroy(trig.gameObject);
        }
        
    }

}
