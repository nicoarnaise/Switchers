using UnityEngine;
using System.Collections;

public class Plateforme : MonoBehaviour {

	public Actionneur actionneur;
	public int pointeurDest;
	public int moveSpeed;
	Animator anim;
	public Vector2[] tabDestination;
	public bool isMoving;
	public Vector2 nextDestination;

	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (actionneur.isActive && !isMoving) {
			isMoving = true;
		}

		if (!actionneur.isActive && isMoving) {
			isMoving = false;
		}

		if (isMoving) {
			makeMove ();
		}
	}

	public void makeMove(){




		transform.position = Vector2.MoveTowards (transform.position, nextDestination, Time.deltaTime * moveSpeed);

		if ((Vector2)transform.position == nextDestination) {
			if (pointeurDest == tabDestination.Length) {
				pointeurDest = 0;
			} else {
				pointeurDest++;
			}
			nextDestination = tabDestination [pointeurDest];
		}
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (collider.gameObject.CompareTag ("Player")) {
			collider.transform.parent.parent = gameObject.transform;
		} else {
			collider.transform.parent = gameObject.transform;

		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag ("Player")) {
			collider.transform.parent.parent = null;
		} else {
			collider.transform.parent = null;
		}
	}

}
