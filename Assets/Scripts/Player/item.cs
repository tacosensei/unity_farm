using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item {
	
	/*
		ItemID			ItemName			Stackable
		0				null				false
		1				Potato				true
	*/
	
	public int id;
	public int amount;
	
	public item(int i, int a) {
		id = i;
		amount = a;
	}
	
	public bool isStackable() {
		switch(id) {
			case 1:
				return true;
			default:
				return false;
		}
	}
	
	public string name() {
		switch(id) {
			case 1:
				return "Potato";
			default:
				return "null";
		}
	}
}
