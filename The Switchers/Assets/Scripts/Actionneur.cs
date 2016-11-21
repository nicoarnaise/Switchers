using UnityEngine;
using System.Collections;

public class Actionneur : MonoBehaviour {


	public bool isActive;
	public Vector2 position;
	public Plateforme support;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = support.transform.position;
	
	}
}
