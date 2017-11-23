﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	private float h = 0.0f;
	private float v = 0.0f;

	private Transform tr;
	
	public float moveSpeed = 10.0f;
	
	void Start () {
		
		tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		
        h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		Debug.Log("H=" + h.ToString());
		Debug.Log("V=" + v.ToString());

		Vector3 moveDirection = (Vector3.forward * v) + (Vector3.right * h);
		tr.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
	}
}
