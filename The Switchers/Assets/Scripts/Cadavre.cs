using UnityEngine;
using System.Collections;

public class Cadavre : MonoBehaviour {

	public bool isSpirit;
	Animator anim;

	private Rigidbody2D rb;

		

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("isSpirit", isSpirit);
		
	}
}
