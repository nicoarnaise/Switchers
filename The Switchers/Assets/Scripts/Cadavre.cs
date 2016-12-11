using UnityEngine;
using System.Collections;

public class Cadavre : MonoBehaviour {

	public GameObject globalState;

	public int statut;
	public bool isSpirit;
    public bool notIntro = true;
	Animator anim;

	public bool isFree;

	private Rigidbody2D rb;

		
	void Awake (){
		globalState = GameObject.Find("GlobalState");
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("isSpirit", isSpirit);
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("free")) {
			
			gameObject.SetActive (false);
		}
		
	}

	public void activate(){

		if (!isFree) {
			isFree = true;
            anim.SetTrigger("liberation");
            if (notIntro)
            {
                GlobalState gs = globalState.GetComponent<GlobalState>();
			    gs.nbCadavre++;
            }			
            anim.SetBool("isFree", true);

        }
	}
}
