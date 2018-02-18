using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour {

	private item[] items = new item[20];
	
	/*Item IDs:
		0 - Empty
		1 -
		2 -
		3 -
	*/
	
	/*Functions:
		EmptyAll()
		Add(itemID, amount)
		AddSlot(slot, itemID, amount)
		Remove(itemID, amount)
		RemoveSlot(slot, amount)
		EmptySlot(slot)
	*/
	
	//public void ChangeItem(int slot, int itemID) {
	//	items[slot] = itemID;
	//	return;
	//}
	
	
	//Delet this
	public void EmptyAll() {
		for(int i = 0; i < items.Length; i++) {
			items[i] = new item(0, 0);
			return;
		}
	}
	
	//When player automatically adds an item to their inventory.
	public void Add(int itemID, int amount) {
		
		//First, check if player already owns the stackable item
		for(int i = 0; i < items.Length; i++) {
			if(items[i].id == itemID && items[i].isStackable()) {
				items[i].amount += amount;
				return;
			}
		}
		
		//If not, check for empty space.
		for(int i = 0; i < items.Length; i++) {
			if(items[i].id == 0) {
				items[i] = new item(itemID, amount);
				return;
			}
			else {
				if(i == items.Length - 1) {
					Debug.Log("<color=red>Inventory Full</color>");
					return;
				}
			}
		}
	}
	
	//When player can choose where to put items.
	public void AddSlot(int slot, int itemID, int amount) {
		items[slot] = new item(itemID, amount);
		return;
	}
	
	//When player automatically removes an item from their inventory.
	public void Remove(int itemID, int amount) {
		
		//Searches inventory for item and removes amount.
		for(int i = 0; i < items.Length; i++) {
			if(items[i].id == itemID) {
				items[i].amount -= 1;
				
				//Checks if this would deplete the slot.
				if(items[i].amount <= 0) {
					items[i] = new item(0, 0);
				}
				return;
			}
			else {
				if(i == items.Length - 1) {
					Debug.Log("<color=red>Item Not Found</color>");
					return;	
				}
			}
		}
	}
	
	public void RemoveFromSlot(int slot, int amount) {
		items[slot].amount -= amount;
		return;
	}
	
	public void EmptySlot(int slot) {
		items[slot] = new item(0, 0);
		return;
	}
}
