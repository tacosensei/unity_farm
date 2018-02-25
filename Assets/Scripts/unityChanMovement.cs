using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unityChanMovement : MonoBehaviour {

	private Animator anim;
	private Rigidbody rb;
	private float moveX, moveY;
	private bool canMove;
	
	void Start () {
		canMove = true;
		anim = transform.GetComponent<Animator>();
		rb = transform.GetComponent<Rigidbody>();
	}
	
	
	void Update () {
		moveX = Input.GetAxisRaw("Horizontal");
		moveY = Input.GetAxisRaw("Vertical");
		if(!canMove) {
			moveX = 0;
			moveY = 0;
		}
		
		anim.SetFloat("Speed", rb.velocity.magnitude);
		
		moveX *= 20;
		moveY *= 20;
		if(rb.velocity.magnitude < 5) {
			rb.AddForce(new Vector3(moveX, 0, moveY));
		}
		if(rb.velocity != Vector3.zero) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z)), Time.deltaTime * 100);
		}
		if(Input.GetKeyDown("r")) {
			transform.position = new Vector3(0,0,0);
		}
	}
	
	public void Move(bool onOff) {
		canMove = onOff;
	}
}
