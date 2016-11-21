using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {

	public Plateforme support;
	public Vector2 destination1;
	public Vector2 destination2;
	public bool isMoving;
	public int moveSpeed;

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
		if ((Vector2) transform.position == destination1) {
			nextDestination = destination2;
		} else {
			nextDestination = destination1;
		}
	}
}
