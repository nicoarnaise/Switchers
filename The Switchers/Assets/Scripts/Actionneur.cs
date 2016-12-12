﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actionneur : MonoBehaviour {

	public int statut;
	public bool isSpirit;
	public bool isActive;

	Animator anim;

	private bool hasCadavre;
	public Cadavre cadavre;
	private bool hasPlayer;


	public bool isTriggered{
		get { return (hasPlayer || hasCadavre); }
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
        if (cadavre != null)
        {
            hasCadavre = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
		anim.SetBool ("isSpirit", isSpirit);

		if ( hasCadavre && cadavre.isFree )
		{
			hasCadavre = false;
		}

		// Activate plaque
		if (gameObject.CompareTag ("Plaque")) {
			if (isTriggered) {
				isActive = true;
			} else {
				isActive = false;
			}
			anim.SetBool ("Activated", isActive);
		}
	
	}

	// Activate levier
	public void activate(){

		if (isActive) {
			isActive = false;
		} else {
			isActive = true;

		}
		anim.SetBool("Activated", isActive);


	}


	void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Player")
			&& gameObject.CompareTag ("Plaque")) {
			hasPlayer = true;
		}

		if (collider.gameObject.CompareTag("Cadavre")
			&& gameObject.CompareTag ("Plaque")) {
			hasCadavre = true;
		}
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")
            && gameObject.CompareTag("Plaque"))
        {
            hasPlayer = true;
        }

        if (collider.gameObject.CompareTag("Cadavre")
            && gameObject.CompareTag("Plaque"))
        {
            hasCadavre = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Player")
			&& gameObject.CompareTag ("Plaque")) {
			hasPlayer = false;
		}

	}

}
