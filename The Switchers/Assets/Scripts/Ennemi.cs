using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {

	public Vector2 position;
	public Plateforme support;
	public Vector2 destination1;
	public Vector2 destination2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			MakeMove ();
		}
	
	}
}
