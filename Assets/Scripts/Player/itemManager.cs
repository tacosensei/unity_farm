using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour {
	
	public int maxInventorySize;
	
	protected int itemID;
	protected string name;
	protected int stackSize;
	protected bool isSellable;
	protected float baseSell;
	protected float fluxSize;
	protected int[] crops;
	
	private Dictionary<int, inventoryItem> library;
	private itemReference[] inventory;
	
	private inventoryItem newItem;
	
	protected struct itemReference {
		public int id, quantity;
		
		public itemReference(int itemNum, int q) {
			id = itemNum;
			quantity = q;
		}
	}
	
	
	void Start() {
		inventory = new itemReference[maxInventorySize];
		ItemLoader();
	}
	
	public void ItemLoader() {
		string[] text = System.IO.File.ReadAllLines(@"Assets/Resources/ItemList.lib");
		library = new Dictionary<int, inventoryItem>(text.Length);
		int itemType = -1;
		foreach(string line in text) {
			string[] words = line.Split(',');
			words[0] = words[0].Trim();
			if(words[0] == "") {
				continue;
			}
			char[] charLine = line.ToCharArray();
			if(charLine[0] == '#') {
				itemType++;
				Debug.Log("Loading type " + itemType + " items...");
				continue;
			}
			for(int i = 0; i < words.Length; i++) {
				words[i] = words[i].Trim();
				switch(i) {
					case 0://ItemID
						itemID = Int32.Parse(words[0]);
						break;
					case 1://Name
						name = words[1];
						break;
					case 2://StackSize
						stackSize = Int32.Parse(words[2]);
						break;
					case 3://IsSellable
						if(words[3] == "y") {
							isSellable = true;
							break;
						}
						else {
							isSellable = false;
							break;
						}
						break;
					case 4://BaseSellValue
						baseSell = Single.Parse(words[4]);
						break;
					case 5://FluctuationSize 
						fluxSize = Single.Parse(words[5]);
						break;
					case 6:
						switch(itemType) {
							case 1://Seed - OutputCrops
								string[] newCrops = words[6].Split('/');
								crops = newCrops.Select(crop => Int32.Parse(crop)).ToArray();
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
			switch(itemType) {
				case 0:
					library.Add(itemID, new inventoryItem(itemID, name, stackSize, isSellable, baseSell, fluxSize));
					break;
				case 1:
					library.Add(itemID, new seed(itemID, name, stackSize, isSellable, baseSell, fluxSize, crops));
					break;
				default: 
					break;
			}
		}
		Debug.Log("All items loaded into library!");
	}
	
	public inventoryItem FindItem(int id) {
		return library[id];
	}
	
	public void InsertItem(int id, int q, int slot) {
		inventory[slot] = new itemReference(id, q);
	}
	
	//attempts to add item(s); returns leftovers
	public int PickUpItem(int id, int q) {
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i].id == id && inventory[i].quantity > 0) {
				int freeSpace = library[id].GetStackSize() - inventory[i].quantity;
				if(freeSpace >= q) {//when a slot has enough space
					inventory[i].quantity += q;
					return 0;//leftover
				}
				if(freeSpace < q && freeSpace > 0) {//when a slot has some space, but not enough
					int leftover = q - freeSpace;
					inventory[i].quantity += freeSpace;
					return PickUpItem(id, leftover);//repeat using leftovers in hopes of finding a new slot
				}
			}
			if(inventory[i].quantity <= 0) {
				inventory[i] = new itemReference(id, q);
				return 0;
			}
		}
		return q;//when all valid slots are full
	}
	
	//attempts to remove item(s) from inventory; returns whether successful or not
	public bool SiphonItem(int id, int q) {
		int itemCount = 0;
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i].id == id && inventory[i].quantity > 0) {
				itemCount += inventory[i].quantity;
			}
		}
		if(itemCount < q) {//check that there is enough before attempting removal
			return false;
		}
		else {
			itemCount = q;
			for(int i = 0; i < inventory.Length; i++) {
				if(inventory[i].id == id && inventory[i].quantity > 0) {
					if(inventory[i].quantity >= itemCount) {
						inventory[i].quantity -= itemCount;
						return true;//all items have been siphoned
					}
					if(inventory[i].quantity < itemCount) {
						inventory[i].quantity = 0;
					}
				}
			}
		}
		return false;//if I messed up somewhere
	}
	
	public bool InventoryFull() {
		for(int i = 0; i < inventory.Length; i++) {
			if(inventory[i].quantity <= 0) {
				return false;
			}
		}
		return true;
	}

}
