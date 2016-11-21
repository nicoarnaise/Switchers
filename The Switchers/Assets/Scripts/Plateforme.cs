using UnityEngine;
using System.Collections;

public class Plateforme : MonoBehaviour {

	public Actionneur actionneur;
	public int pointeurDest;
	public int moveSpeed;
	public int statut;
	public Vector2[] tabDestination;
	public bool isMoving;
	public Vector2 nextDestination;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			makeMove ();
		}
	}

	public void makeMove(){
		transform.position = Vector2.MoveTowards (transform.position, nextDestination, Time.deltaTime * moveSpeed);
		if (pointeurDest == tabDestination.Length) {
			pointeurDest = 0;
		} else {
			pointeurDest++;
		}
		nextDestination = tabDestination [pointeurDest];
	}
}
