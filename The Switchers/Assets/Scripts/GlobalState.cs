using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour {

	public int nbCadavre;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
}
