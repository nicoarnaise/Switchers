using UnityEngine;
using System.Collections;

public class Mur : MonoBehaviour {

	// statut =
	// 0 : Mixte
	// 1 : Physique
	// 2 : Spirituel
	public int statut;

	// isSpirit, correspondant au mode de vue du joueur
	//sert pour l'animation du mode physique ou spirituel
	public bool isSpirit;

	// Animator
	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	
	}
	

	void Update () {

		anim.SetBool ("isSpirit", isSpirit);
	
	}
}
