using UnityEngine;
using System.Collections;

public class Plateforme : MonoBehaviour {

	// Actionneur de la plateforme pour la faire bouger ou non
	public Actionneur actionneur;

	// Pointeur vers la case du tableau contenant la position suivante vers laquelle se diriger
	public int pointeurDest;

	// Vitesse de la plateforme
	public int moveSpeed;

	// // statut =
	// 0 : Mixte
	// 1 : Physique
	// 2 : Spirituel
	public int statut;

	// isSpirit, correspondant au mode de vue du joueur
	//sert pour l'animation du mode physique ou spirituel
	public bool isSpirit;

	// Si la plateforme est mobile
	public bool isMobile;

	// Si la plateforme bouge alors que l'activateur est desactive
	public bool isMovingAlone;

	// Animator
	public Animator anim;

	// Tableau contenant les positions des differentes destination de la plateforme
	public Vector2[] tabDestination;
	// Si la plateforme bouge
	public bool isMoving;
	// Si la plateforme est activable
	public bool isActivable;
	// Position de la destination de la plateforme
	public Vector2 nextDestination;

	// RigidBody
	private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {

		anim.SetBool ("isSpirit", isSpirit);

		// Si la plateforme est activable :
		// Elle bouge ou non selon la position de l'activateur
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

	// Mouvement de la plateforme, si la plateforme a atteint la destination, le pointeur s'incremente
	// et la plateforme continue vers sa prochaine destination

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

	//Detecte si la plateforme et le player sont en collisions
	// Si c'est le cas, le player et son groundChecker passent en fils de la plateforme
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

	// Detecte si la plateforme et le player sortent de collisions
	// Si c'est le cas, player et groundChecker ne sont plus fils de la plateforme
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
