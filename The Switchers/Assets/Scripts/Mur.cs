using UnityEngine;
using System.Collections;

public class Mur : MonoBehaviour {

	public bool isSpirit;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		anim.SetBool ("isSpirit", isSpirit);
	
	}
}
