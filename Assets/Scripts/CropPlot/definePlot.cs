using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class definePlot : MonoBehaviour {
	
	public GameObject selection;
	public GameObject plot;
	private GameObject pointer;
	
	void Start () {
		pointer = Instantiate(selection);
	}
	
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
			if (hit.transform.gameObject.tag == "valid") {
				pointer.SetActive(true);
				pointer.transform.position = hit.point;
				if(Input.GetMouseButtonDown(0)) {
					GameObject newPlot = Instantiate(plot);
					newPlot.transform.position = pointer.transform.position;
				}
			}
			else {
				pointer.SetActive(false);
			}
        }
	}
}
