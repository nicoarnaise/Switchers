﻿using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Continuer : MonoBehaviour {

	public GameObject globalState;
	public Button button;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

			GlobalState gs = globalState.GetComponent<GlobalState> ();
			if (!gs.hasStarted){
			button.interactable = false;
			}
	}
}
