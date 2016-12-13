using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {

	//// statut =
	// 0 : Mixte
	// 1 : Physique
	// 2 : Spirituel
	public int statut;

	// isSpirit, correspondant au mode de vue du joueur
	//sert pour l'animation du mode physique ou spirituel
	public bool isSpirit;

	//Variables de mouvement
	public bool isMoving;
	public int moveSpeed;

	// Animator
	Animator anim;

    //timer
    public bool isTimerOn;
    public float period = 0.2f;
    public float timer;

	// Direction de l'ennemi
    bool facingRight = false;

	// RigidBody
	private Rigidbody2D rb;

	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	

	void Update () {
		// L'ennemi bouge
		if (isMoving) {
			makeMove ();
		}

        // laisse un temps pour permettre à l'ennemi de sortir de collision avec les box
		// de collisions
        if (isTimerOn && Time.time > timer + period)
        {
            isTimerOn = false;
        }


    }

	// Defini une vitesse à l'ennemi selon sa direction
	public void makeMove(){


		Vector2 velocity = rb.velocity;

		if (facingRight) {
			velocity.x = moveSpeed;
		} else {
			velocity.x = -moveSpeed;
		}
		rb.velocity = velocity;



	}

	// Change la direction de l'ennemi
	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// Detecte si l'ennemi entre en collision avec des "Bords", en general places
	// d'un bout à l'autre de la plateforme pour permettre à l'ennemi de faire
	// des allez retours.
	void OnTriggerEnter2D(Collider2D collider){

        if (!isTimerOn && collider.gameObject.CompareTag("Bords"))
        {
            timer = Time.time;
            isTimerOn = true;
            Flip();
        }

    }

	// Detecte si l'ennemi entre en collision avec des "Bords", en general places
	// d'un bout à l'autre de la plateforme pour permettre à l'ennemi de faire
	// des allez retours.
    void OnTriggerStay2D(Collider2D collider)
    {
        
        if (!isTimerOn && collider.gameObject.CompareTag("Bords"))
        {
            timer = Time.time;
            isTimerOn = true;
            Flip();
        }

    }
}
