using UnityEngine;
using System.Collections;

public class Plateforme : MonoBehaviour {

	public Actionneur actionneur;
	public int pointeurDest;
	public int moveSpeed;

	public bool isSpirit;
	public bool isMobile;
	public int statut;
	public bool isMovingAlone;
	public Animator anim;
	public Vector2[] tabDestination;
	public bool isMoving;
	public bool isActivable;
	public Vector2 nextDestination;

	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

		anim.SetBool ("isSpirit", isSpirit);

		if (isActivable) {
			if (!isMovingAlone) {
				if (actionneur.isActive && !isMoving) {
					isMoving = true;
				}
				if (!actionneur.isActive && isMoving) {
					isMoving = false;
				}
			} else {
				if (actionneur.isActive && isMoving) {
					isMoving = false;
				}
				if (!actionneur.isActive && !isMoving) {
					isMoving = true;
				}
			}
		}

		if (isMoving) {
			makeMove ();
        }

	}

    public void makeMove() {
        
        ((Rigidbody2D)GetComponent<Rigidbody2D>()).MovePosition(Vector2.MoveTowards (transform.position, nextDestination, Time.deltaTime * moveSpeed));

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

		if (collider.gameObject.CompareTag ("Player") && collider.transform.parent.CompareTag("Player")) {
			collider.transform.parent.parent = gameObject.transform;
            gameObject.layer = 8;
        } else if (collider.gameObject.CompareTag("Player"))
        {
            collider.transform.parent = gameObject.transform;
            gameObject.layer = 8;
        }
	}

	void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag ("Player") && collider.transform.parent.parent.gameObject == gameObject) {
			collider.transform.parent.parent = null;
            gameObject.layer = 10;
        } else if (collider.gameObject.CompareTag("Player"))
        {
            collider.transform.parent = null;
        }
	}

}
