using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundChecker : MonoBehaviour {

	private HashSet<Collider2D> triggers = new HashSet<Collider2D>();
	private HashSet<Collider2D> actionners = new HashSet<Collider2D>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public HashSet<Collider2D> getTriggers(){
		return triggers;
	}

	public bool isTriggered{
		get { return triggers.Count > 0; }
	}

	public bool isActivated{
		get { return actionners.Count > 0; }
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Plateforme")) {
			triggers.Add (collider);
		}

		if (collider.gameObject.CompareTag("Actionneur")) {
			actionners.Add (collider);
		}
	}

    void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Plateforme")) {
			triggers.Remove (collider);
		}

		if (collider.gameObject.CompareTag("Actionneur")) {
			actionners.Remove (collider);
		}
	}


}
