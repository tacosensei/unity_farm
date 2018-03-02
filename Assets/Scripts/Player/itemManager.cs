using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour {
	
	protected int itemID;
	protected string name;
	protected int stackSize;
	protected bool isSellable;
	protected float baseSell;
	protected float fluxSize;
	protected int[] crops;
	
	private Dictionary<int, inventoryItem> library = new Dictionary<int, inventoryItem>(200);
	private List<inventoryItem> inventory = new List<inventoryItem>(20);
	
	private inventoryItem newItem;
	
	
	void Start() {
		ItemLoader();
	}
	
	public void ItemLoader() {
		string[] text = System.IO.File.ReadAllLines(@"Assets/Resources/ItemList.lib");
		int itemType = -1;
		foreach(string line in text) {
			//23, Turnip, 30, y, 100, 20
			string[] words = line.Split(',');
			words[0] = words[0].Trim();
			if(words[0] == "") {
				continue;
			}
			char[] charLine = line.ToCharArray();
			if(charLine[0] == '#') {
				itemType++;
				//itemType 0: inventoryItem
				//itemType 1: seed
				print("New Item Type: " + itemType);
				continue;
			}
			for(int i = 0; i < words.Length; i++) {
				words[i] = words[i].Trim();
				switch(i) {
					case 0:
						itemID = Int32.Parse(words[0]);
						//print("ItemID: " + itemID + " ");
						break;
					case 1:
						name = words[1];
						//print("Name: " + name + " ");
						break;
					case 2:
						stackSize = Int32.Parse(words[2]);
						//print("Stack Size: " + stackSize + " ");
						break;
					case 3:
						if(words[3] == "y") {
							isSellable = true;
							//print("Sellable: true ");
							break;
						}
						else {
							isSellable = false;
							//print("Sellable: false ");
							break;
						}
						break;
					case 4:
						baseSell = Single.Parse(words[4]);
						//print("Base Value: " + baseSell + " ");
						break;
					case 5:
						fluxSize = Single.Parse(words[5]);
						//print("Flux Range: +/- " + fluxSize + " ");
						break;
					case 6:
						switch(itemType) {
							case 1://Seed
								string[] newCrops = words[6].Split('/');
								crops = newCrops.Select(crop => Int32.Parse(crop)).ToArray();
								//print("Crops: ");
								for(int n = 0; n < crops.Length; n++) {
									//print(crops[n] + " ");
								}
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
					print("Adding Item " + itemID);
					library.Add(itemID, new inventoryItem(itemID, name, stackSize, isSellable, baseSell, fluxSize));
					print("New InventoryItem: " + library[itemID].GetName());
					break;
				case 1:
					print("Adding Item " + itemID);
					library.Add(itemID, new seed(itemID, name, stackSize, isSellable, baseSell, fluxSize, crops));
					print("New Seed: " + library[itemID].GetName());
					print("Crop Output Default: " + ((seed)library[itemID]).GetCrops()[0]);
					break;
				default: 
					break;
			}
		}
	}
	
	public inventoryItem FindItem(int id) {
		return library[id];
	}
}
