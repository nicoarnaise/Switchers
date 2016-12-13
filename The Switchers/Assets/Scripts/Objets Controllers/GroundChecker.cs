using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Ici le joueur est assimile a ses pieds.
public class GroundChecker : MonoBehaviour {

	// Variables stockant les objets avec lesquels le joueur rentre en collision
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

	// Retourne si le joueur est sur une plateforme
	public bool isTriggered{
		get { return triggers.Count > 0; }
	}

	// Retourne si le joueur est a cote d'un actionneur
	public bool isActivated{
		get { return actionners.Count > 0; }
	}

	// Recupere les objets en collisions avec le joueur
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Plateforme")) {
			triggers.Add (collider);
		}

		if (collider.gameObject.CompareTag("Actionneur")) {
			actionners.Add (collider);
		}
	}

	// Enleve les objets qui sortent de collisions avec le joueur
    void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Plateforme")) {
			triggers.Remove (collider);
		}

		if (collider.gameObject.CompareTag("Actionneur")) {
			actionners.Remove (collider);
		}
	}


}
