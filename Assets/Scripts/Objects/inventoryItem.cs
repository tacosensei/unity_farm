using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryItem {

	protected int itemID;
	protected string name;
	protected int stackSize;
	protected bool isSellable;
	protected float baseSell;
	protected float fluxSize;
	

	public inventoryItem(int i, string n, int stack, bool sell, float sellB, float f) {
		itemID = i;
		name = n;
		stackSize = stack;
		isSellable = sell;
		baseSell = sellB;
		fluxSize = f;
	}
	
	public int GetID() {
		return itemID;
	}
	
	public string GetName() {
		return name;
	}
	
	public int GetStackSize() {
		return stackSize;
	}
	
	public bool GetIsSellable() {
		return isSellable;
	}
	
	public float GetBaseSell() {
		return baseSell;
	}
	
	public float GetFluxSize() {
		return fluxSize;
	}
}
