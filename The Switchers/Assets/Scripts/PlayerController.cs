﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public Level level;
	public bool isSpirit;

	public bool isPhysic;
	public bool isGrounded;
	public int moveSpeed;
	public float flyInertia = 0.9f;
	public int jumpSpeed;
	public bool hasJumped;

	public bool isActivating;
	public bool isTimerOn;
	public float period;
	public float timer;

	public GroundChecker groundChecker;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		isTimerOn = false;
		isActivating = false;
		hasJumped = false;
		period = 0.1f;
		rb = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {
		

		// setTimerOn to allow player to switch actionner only once every period Time
		if (isActivating) {
			timer = Time.time;
			isActivating = false;
			isTimerOn = true;
		}

		// Allows to have a period elapsed function
		if (isTimerOn && Time.time > timer + period) {
			isTimerOn = false;
		}


		if (isGrounded && Input.GetButtonDown("Jump")) {	
			rb.AddForce(new Vector2(0f,jumpSpeed), ForceMode2D.Impulse);
			}

		if (Input.GetButtonDown ("Switch")) {
			isSpirit = !isSpirit;
			int lengthActionneurs = level.actionneurs.Length;
			int lengthEnnemis = level.ennemis.Length;
			int lengthCadavres = level.cadavres.Length;
			int lengthPlateformes = level.plateformes.Length;
			int lengthMurs = level.murs.Length;


			for (int i = 0; i < lengthActionneurs; i++) {
				Animator anim = ((Actionneur)level.actionneurs[i]).GetComponent<Animator>();
				anim.SetBool("isSpirit", isSpirit);
			}
			for (int i = 0; i < lengthEnnemis; i++) {
				Animator anim = ((Ennemi)level.ennemis[i]).GetComponent<Animator>();
				anim.SetBool("isSpirit", isSpirit);
			}
			for (int i = 0; i < lengthCadavres; i++) {
				Animator anim = ((Cadavre)level.cadavres[i]).GetComponent<Animator>();
				anim.SetBool("isSpirit", isSpirit);
			}
			for (int i = 0; i < lengthPlateformes; i++) {
				Animator anim = ((Plateforme)level.plateformes[i]).GetComponent<Animator>();
				anim.SetBool("isSpirit", isSpirit);
			}
			/*for (int i = 0; i < lengthMurs; i++) {
				Animator anim = ((Mur)level.murs[i]).GetComponent<Animator>();
				anim.SetBool("isSpirit", isSpirit);
			}*/
		}


	}

	void FixedUpdate(){

		// Jump

		isGrounded = groundChecker.isTriggered;

		// Movement

		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 velocity = rb.velocity;
		if (isGrounded) {
			velocity.x = moveHorizontal * moveSpeed;
		} else {
			// While in the air, player can influence movement by 10%
			velocity.x = (velocity.x * flyInertia + moveHorizontal * moveSpeed * (1 - flyInertia));
		}
		rb.velocity = velocity;
		

	}

	void OnTriggerStay2D(Collider2D other){

		// Actionneur
		if (Input.GetKeyDown(KeyCode.DownArrow) && !isActivating && !isTimerOn){

			if (other.gameObject.CompareTag ("Actionneur")) {
				Actionneur actionneur = other.gameObject.GetComponent<Actionneur>();
				actionneur.activate();
				isActivating = true;
			}
		}
	}



}
