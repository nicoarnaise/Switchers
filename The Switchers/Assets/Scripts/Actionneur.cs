using UnityEngine;
using System.Collections;

public class Actionneur : MonoBehaviour {


	public bool isActive;

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
		anim.SetBool("Activated", isActive);


	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("coucou");
		if (collider.gameObject.CompareTag ("Player") && gameObject.CompareTag ("Plaque")) {
			//Debug.Log ("coucou");
			isActive = true;
			anim.SetBool ("Activated", isActive);
		}
	}


}
