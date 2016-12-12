using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour {

	public int statut;
	public bool isSpirit;
	public bool isMoving;
	public int moveSpeed;
	Animator anim;

    //timer
    public bool isTimerOn;
    public float period = 0.2f;
    public float timer;

    bool facingRight = false;


	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			makeMove ();
		}

        // Allows to have a period elapsed function
        if (isTimerOn && Time.time > timer + period)
        {
            isTimerOn = false;
        }


    }


	public void makeMove(){


		Vector2 velocity = rb.velocity;

		if (facingRight) {
			velocity.x = moveSpeed;
		} else {
			velocity.x = -moveSpeed;
		}
		rb.velocity = velocity;



	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D collider){

        if (!isTimerOn && collider.gameObject.CompareTag("Bords"))
        {
            timer = Time.time;
            isTimerOn = true;
            Flip();
        }

    }

    void OnTriggerStay2D(Collider2D collider)
    {
        
        if (!isTimerOn && collider.gameObject.CompareTag("Bords"))
        {
            timer = Time.time;
            isTimerOn = true;
            Flip();
        }

    }
}
