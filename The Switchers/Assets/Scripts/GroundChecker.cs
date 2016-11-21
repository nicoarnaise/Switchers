using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundChecker : MonoBehaviour {

	private HashSet<Collider2D> triggers = new HashSet<Collider2D>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isTriggered{
		get { return triggers.Count > 0; }
	}

	void OnTriggerEnter2D(Collider2D collider){
		triggers.Add (collider);
	}

	void OnTriggerExit2D(Collider2D collider){
		triggers.Remove (collider);
	}


}
