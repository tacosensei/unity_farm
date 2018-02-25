using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui : MonoBehaviour {

	public GameObject inventory;
	public unityChanMovement playerMoveScript;
	private bool inventoryOpen;

	void Start () {
		
	}
	

	void Update () {
		if(Input.GetButtonDown("Menu")) {
			inventory.SetActive(!inventory.activeSelf);
			inventoryOpen = !inventoryOpen;
			playerMoveScript.Move(!inventoryOpen);
		}
	}
	
	public bool InvIsOpen() {
		return inventoryOpen;
	}
}
