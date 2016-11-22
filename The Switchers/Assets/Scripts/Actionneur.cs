using UnityEngine;
using System.Collections;

public class Actionneur : MonoBehaviour {


	public bool isActive;
	public Plateforme support;

	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	//	transform.position = support.transform.position;
	
	}

	// Activate the actionneur
	public void activate(){

		if (isActive) {
			isActive = false;
		} else {
			isActive = true;

		}

		Debug.Log (isActive);
		anim.SetBool("Activated", isActive);


	}


}
