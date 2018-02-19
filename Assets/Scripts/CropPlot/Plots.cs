using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plots : MonoBehaviour {
	
	public GameObject selection;
	public GameObject plot;
	public bool adjacentMode;
	private GameObject pointer;
	private List<PlotGroup> plotGroups = new List<PlotGroup>();
	
	void Start () {
		pointer = Instantiate(selection);
	}
	
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
			if (hit.transform.gameObject.tag == "valid") {
				if(hit.transform.gameObject.name == "Adjacent") {
					
					GameObject adj = hit.transform.gameObject;
					int groupID = adj.transform.GetComponentInParent<Plot>().groupID;
					string orientation = adj.transform.GetComponent<AdjacentPlot>().position;
					GameObject parent = adj.transform.parent.gameObject;

					pointer.transform.position = hit.transform.gameObject.transform.position;
					if (Input.GetMouseButtonDown(0)) {
						GameObject newPlot = Instantiate(plot);
						newPlot.transform.position = pointer.transform.position;

						AddPlotToGroup(groupID, newPlot, parent, orientation);
					}
				}
				else
                {
                    pointer.SetActive(true);
                    pointer.transform.position = hit.point;
					
				    if (Input.GetMouseButtonDown(0)) {
						PlotGroup pg = CreatePlotGroup(pointer.transform.position);
						GameObject newPlot = Instantiate(plot);
						newPlot.transform.position = pointer.transform.position;
						pg.Initialize(newPlot);
						plotGroups.Add(pg);
					}
                }
            }
			else {
				//pointer.SetActive(false);
			}
        }
	}

	private PlotGroup CreatePlotGroup(Vector3 position) {
		return new PlotGroup(position, plotGroups.Count);
	}

	private void AddPlotToGroup(int id, GameObject plot, GameObject parent, string orientation) {
		PlotGroup pg = plotGroups[id];
		pg.AddPlot(plot, parent, orientation);
	}
}
