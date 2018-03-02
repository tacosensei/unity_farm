using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seed : inventoryItem {

	protected int[] crops;
	
	public seed(int i, string n, int stack, bool sell, float sellB, float f, int[] c)
		: base(i, n, stack, sell, sellB, f) {
			
		crops = c;
	}
	
	public int[] GetCrops() {
		return crops;
	}
}
