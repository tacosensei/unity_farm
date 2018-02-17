using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObject : MonoBehaviour {

	public Transform target;

	void Update () {
		transform.position = target.position + new Vector3(0,5,-5);
	}
}
