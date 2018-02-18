using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class definePlot : MonoBehaviour {
	
	public GameObject sphere;
	
	void Update () {
		if(Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray)) {
                Instantiate(sphere, transform.position, transform.rotation);
			}
        }
	}
}
