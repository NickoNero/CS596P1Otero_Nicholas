using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        adjustFuelBar();

	}

    void adjustFuelBar()
    {
        if (Player_Controls.jetPackFuel > 0.001)
        {
            gameObject.transform.localScale = new Vector3(Player_Controls.jetPackFuel, 1, 1);
        }
    }
}
