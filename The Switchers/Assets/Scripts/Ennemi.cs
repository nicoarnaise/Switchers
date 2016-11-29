using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {

	public int statut;
	public Plateforme support;
	public Vector2 destination1;
	public Vector2 destination2;
	public bool isSpirit;
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
		anim.SetBool ("isSpirit", isSpirit);
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
