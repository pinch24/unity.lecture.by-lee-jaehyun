using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationClipList {

	public AnimationClip idle;
	public AnimationClip runForward;
	public AnimationClip runBackward;
	public AnimationClip runRight;
	public AnimationClip runLeft;
}

public class PlayerControl : MonoBehaviour {

	private float h = 0.0f;
	private float v = 0.0f;
	
	public float moveSpeed = 10.0f;
	public float rotateSpeed = 100.0f;

	public AnimationClipList animationClipList;
	public Animation animationControl;
	
	private Transform tr;

	void Start () {
		
		tr = GetComponent<Transform>();

		animationControl = GetComponentInChildren<Animation>();

		animationControl.clip = animationClipList.idle;
		animationControl.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		Vector3 moveDirection = (Vector3.forward * v) + (Vector3.right * h);
		tr.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.Self);
		tr.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));

		if (v >= 0.1f) {

			animationControl.CrossFade(animationClipList.runForward.name, 0.3f);
		}
		else if (v <= -0.1f) {

			animationControl.CrossFade(animationClipList.runBackward.name, 0.3f);
		}
		else if (h >= 0.1f) {

			animationControl.CrossFade(animationClipList.runRight.name, 0.3f);
		}
		else if (h <= -0.1f) {

			animationControl.CrossFade(animationClipList.runLeft.name, 0.3f);
		}
		else {

			animationControl.CrossFade(animationClipList.idle.name, 0.3f);
		}
	}
}
