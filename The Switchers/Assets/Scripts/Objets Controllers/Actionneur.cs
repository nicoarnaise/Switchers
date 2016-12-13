using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actionneur : MonoBehaviour {

	// statut =
	// 0 : Mixte
	// 1 : Physique
	// 2 : Spirituel
	public int statut;

	// isSpirit, correspondant au mode de vue du joueur
	//sert pour l'animation du mode physique ou spirituel
	public bool isSpirit;

	//isActive correspond à l'activation ou non de l'actionneur
	public bool isActive;

	// Animator de l'objet
	Animator anim;

	// Variables utilisees pour savoir si une plaque est en contact avec un cadavre
	// ou un joueur
	private bool hasCadavre;
	public Cadavre cadavre;
	private bool hasPlayer;


	// pour savoir si une plaque est en contact avec un cadavre
	// ou un joueur
	public bool isTriggered{
		get { return (hasPlayer || hasCadavre); }
	}
		
	void Start () {
		anim = GetComponent<Animator>();
        if (cadavre != null)
        {
            hasCadavre = true;
        }
	}
	

	void Update () {
		
		anim.SetBool ("isSpirit", isSpirit);

		// Si le cadavre se trouvant sur la plaque est liberee, 
		// il n'y a plus de cadavre.
		if ( hasCadavre && cadavre.isFree )
		{
			hasCadavre = false;
		}

		// Active la plaque si isTriggered est vrai
		if (gameObject.CompareTag ("Plaque")) {
			if (isTriggered) {
				isActive = true;
			} else {
				isActive = false;
			}
			anim.SetBool ("Activated", isActive);
		}
	
	}

	// Active / Desactive le levier selon si il est Desactive/Active
	public void activate(){

		if (isActive) {
			isActive = false;
		} else {
			isActive = true;

		}
		anim.SetBool("Activated", isActive);
	}

	// On regarde les collisions entre plaque et joueur/cadavre
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

	// On regarde les collisions entre plaque et joueur/cadavre
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

	// On regarde si le joueur sort de la plaque
    void OnTriggerExit2D(Collider2D collider){
		if (collider.gameObject.CompareTag("Player")
			&& gameObject.CompareTag ("Plaque")) {
			hasPlayer = false;
		}

	}

}
