using UnityEngine;
using System.Collections;

public class Actionneur : MonoBehaviour {

	public int statut;
	public bool isSpirit;
	public bool isActive;

	Animator anim;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		anim.SetBool ("isSpirit", isSpirit);
	
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

	// Plaque Enter
	/*void OnTriggerEnter2D(Collider2D collider){
		
		if (collider.gameObject.CompareTag ("Player") && gameObject.CompareTag ("Plaque")) {
			isActive = true;
			anim.SetBool ("Activated", isActive);
		}
	}

	// Plaque Exit
	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag ("Player") && gameObject.CompareTag ("Plaque")) {
			isActive = false;
			anim.SetBool ("Activated", isActive);
		}
	}*/

	// Plaque 
	void OnTriggerStay2D(Collider2D collider){

		if (gameObject.CompareTag ("Plaque")) {
			// Player
			if (collider.gameObject.CompareTag ("Player")) {
				isActive = true;
			} else {
				// Cadavre
				if (collider.gameObject.CompareTag ("Cadavre")) {
					Cadavre cadavre = collider.gameObject.GetComponent<Cadavre> ();
					if (!cadavre.isFree) {
						isActive = true;
					} else {  
						isActive = false;
					} 
				} else {
					isActive = false;
				}
			}
			anim.SetBool ("Activated", isActive);
		}
	}

}
