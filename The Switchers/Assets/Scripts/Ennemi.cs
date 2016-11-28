using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {

	public Plateforme support;
	public Vector2 destination1;
	public Vector2 destination2;
	public bool isMoving;
	public int moveSpeed;
	Animator anim;

	bool facingRight = false;

	public Vector2 nextDestination;

	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			makeMove ();
		}
	
	}


	public void makeMove(){


		Vector2 velocity = rb.velocity;

		if (facingRight) {
			velocity.x = moveSpeed;
		} else {
			velocity.x = -moveSpeed;
		}
		rb.velocity = velocity;

		// transform.position = Vector2.MoveTowards (transform.position, nextDestination, Time.deltaTime * moveSpeed);
		/*if ((Vector2) transform.position == nextDestination) {
			if (nextDestination == destination1) {
				nextDestination = destination2;
			} else {
				nextDestination = destination1;
			}
			*/


	}

	void Flip(){
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D collider){

		if (collider.gameObject.CompareTag ("Bords")) {
			Flip ();
		}
	}
}
