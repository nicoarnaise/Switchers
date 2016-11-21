using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public bool isPhysic;
	public bool isGrounded;
	public int moveSpeed;
	public float flyInertia = 0.9f;
	public int jumpSpeed;
	public bool hasJumped;

	public GroundChecker groundChecker;

	private Rigidbody2D rb;
	/*private BoxCollider2D collider;
	private BoxCollider2D other;*/

	// Use this for initialization
	void Start () {
		hasJumped = false;
		/*collider = GetComponent<BoxCollider2D> ();
		other = GameObject.FindGameObjectWithTag ("Plateforme").GetComponent<BoxCollider2D>();*/
		rb = GetComponent<Rigidbody2D>();
	
	}
	
	// Update is called once per frame
	void Update () {


		if (!hasJumped && isGrounded && Input.GetButtonDown("Jump")) {
			
				rb.AddForce(new Vector2(0f,jumpSpeed));
				hasJumped = true;
			
			}

		}

	void FixedUpdate(){

		// Jump

		isGrounded = groundChecker.isTriggered;
		if (!isGrounded) {
			hasJumped = false;
		}

		// Movement

		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 velocity = rb.velocity;
		if (isGrounded) {
			velocity.x = moveHorizontal * moveSpeed;
		} else {
			// While in the air, player can influence movement by 10%
			velocity.x = (velocity.x * flyInertia + moveHorizontal * moveSpeed * (1 - flyInertia));
		}
		rb.velocity = velocity;
		
			
			/*if (collider.IsTouching(other)) {
				isSaut = false;
				StartCoroutine (WaitAFrame ());	
			}*/

	}

	/*IEnumerator WaitAFrame(){
		yield return new WaitForSeconds(0.001f);
	}*/
}
