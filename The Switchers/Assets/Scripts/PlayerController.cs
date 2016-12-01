using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public Level level;
	public int sceneIndex;

	public bool isSpirit;

	public bool isPhysic;
	public bool isGrounded;
	public int moveSpeed;
	public float flyInertia = 0.9f;
	public int jumpSpeed;
	public bool hasJumped;

	public bool isActivating;
	public bool isTimerOn;
	public float period;
	public float timer;

	public GroundChecker groundChecker;
	public GameObject checkpoint;

    private Animator animator;

	private Rigidbody2D rb;
	public bool facingRight;

	// Use this for initialization
	void Start () {
		isTimerOn = false;
		isActivating = false;
		hasJumped = false;
		period = 0.1f;
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

		int lengthActionneurs = level.actionneurs.Length;
		int lengthEnnemis = level.ennemis.Length;
		int lengthCadavres = level.cadavres.Length;
		int lengthPlateformes = level.plateformes.Length;
		int lengthMurs = level.murs.Length;

        animator.SetBool("isSpirit", isSpirit);
        for (int i = 0; i < lengthActionneurs; i++)
        {
            Actionneur actionneur = (Actionneur)level.actionneurs[i];
            actionneur.isSpirit = isSpirit;
            actionneur.gameObject.SetActive(actionneur.statut == 0 || isSpirit && actionneur.statut == 2 || !isSpirit && actionneur.statut == 1);
        }
        for (int i = 0; i < lengthEnnemis; i++)
        {
            Ennemi ennemi = (Ennemi)level.ennemis[i];
            ennemi.isSpirit = isSpirit;
            ennemi.gameObject.SetActive(ennemi.statut == 0 || isSpirit && ennemi.statut == 2 || !isSpirit && ennemi.statut == 1);
        }
        for (int i = 0; i < lengthCadavres; i++)
        {
            Cadavre cadavre = (Cadavre)level.cadavres[i];
            cadavre.isSpirit = isSpirit;
            cadavre.gameObject.SetActive(cadavre.statut == 0 || isSpirit && cadavre.statut == 2 || !isSpirit && cadavre.statut == 1);
        }

        for (int i = 0; i < lengthPlateformes; i++)
        {
            Plateforme plateforme = (Plateforme)level.plateformes[i];
            plateforme.isSpirit = isSpirit;
            if (!(plateforme.statut == 0 || isSpirit && plateforme.statut == 2 || !isSpirit && plateforme.statut == 1))
            {
                for (int j = 0; j < plateforme.transform.childCount; j++)
                {
                    Transform objet = plateforme.transform.GetChild(j);
                    if (objet.name == "Player")
                    {
                        groundChecker.getTriggers().Clear();
                        objet.transform.parent = null;
                    }
                }
            }

            plateforme.gameObject.SetActive(plateforme.statut == 0 || isSpirit && plateforme.statut == 2 || !isSpirit && plateforme.statut == 1);
        }
        for (int i = 0; i < lengthMurs; i++)
        {
            Mur mur = (Mur)level.murs[i];
            mur.isSpirit = isSpirit;
            mur.gameObject.SetActive(mur.statut == 0 || isSpirit && mur.statut == 2 || !isSpirit && mur.statut == 1);
        }
    }
		
	
	// Update is called once per frame
	void Update () {

		//Movement direction control
		Flip();

		// setTimerOn to allow player to switch actionner only once every period Time
		if (isActivating) {
			timer = Time.time;
			isActivating = false;
			isTimerOn = true;
		}

		// Allows to have a period elapsed function
		if (isTimerOn && Time.time > timer + period) {
			isTimerOn = false;
		}


		if (isGrounded && Input.GetButtonDown("Jump")) {	
			animator.SetBool ("isGround", false);
			rb.AddForce(new Vector2(0f,jumpSpeed), ForceMode2D.Impulse);
			}

		if (Input.GetButtonDown ("Switch")) {
			isSpirit = !isSpirit;
            animator.SetBool("isSpirit", isSpirit);
			int lengthActionneurs = level.actionneurs.Length;
			int lengthEnnemis = level.ennemis.Length;
			int lengthCadavres = level.cadavres.Length;
			int lengthPlateformes = level.plateformes.Length;
			int lengthMurs = level.murs.Length;


            for (int i = 0; i < lengthActionneurs; i++)
            {
                Actionneur actionneur = (Actionneur)level.actionneurs[i];
                actionneur.isSpirit = isSpirit;
                actionneur.gameObject.SetActive(actionneur.statut == 0 || isSpirit && actionneur.statut == 2 || !isSpirit && actionneur.statut == 1);
            }
            for (int i = 0; i < lengthEnnemis; i++)
            {
                Ennemi ennemi = (Ennemi)level.ennemis[i];
                ennemi.isSpirit = isSpirit;
                ennemi.gameObject.SetActive(ennemi.statut == 0 || isSpirit && ennemi.statut == 2 || !isSpirit && ennemi.statut == 1);
            }
            for (int i = 0; i < lengthCadavres; i++)
            {
                Cadavre cadavre = (Cadavre)level.cadavres[i];
                cadavre.isSpirit = isSpirit;
                cadavre.gameObject.SetActive(cadavre.statut == 0 || isSpirit && cadavre.statut == 2 || !isSpirit && cadavre.statut == 1);
            }

            for (int i = 0; i < lengthPlateformes; i++)
            {
                Plateforme plateforme = (Plateforme)level.plateformes[i];
                plateforme.isSpirit = isSpirit;
                if (!(plateforme.statut == 0 || isSpirit && plateforme.statut == 2 || !isSpirit && plateforme.statut == 1))
                {
                    for (int j = 0; j < plateforme.transform.childCount; j++)
                    {
                        Transform objet = plateforme.transform.GetChild(j);
                        if (objet.name == "Player")
                        {
                            groundChecker.getTriggers().Clear();
                            objet.transform.parent = null;
                        }
                    }
                }

                plateforme.gameObject.SetActive(plateforme.statut == 0 || isSpirit && plateforme.statut == 2 || !isSpirit && plateforme.statut == 1);
            }
            for (int i = 0; i < lengthMurs; i++)
            {
                Mur mur = (Mur)level.murs[i];
                mur.isSpirit = isSpirit;
                mur.gameObject.SetActive(mur.statut == 0 || isSpirit && mur.statut == 2 || !isSpirit && mur.statut == 1);
            }
        }


	}

	void FixedUpdate(){

		// Jump

		isGrounded = groundChecker.isTriggered;
		animator.SetBool("isGround", isGrounded);
		animator.SetFloat("vSpeed", rb.velocity.y);
        animator.SetFloat("hSpeed", Mathf.Abs(rb.velocity.x));

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
		

	}

	void OnTriggerStay2D(Collider2D other){


		if (Input.GetKeyDown(KeyCode.DownArrow) && !isActivating && !isTimerOn){

			// Actionneur
			if (other.gameObject.CompareTag ("Actionneur")) {
				Actionneur actionneur = other.gameObject.GetComponent<Actionneur>();
				actionneur.activate();
				isActivating = true;
			}

			// Cadavre
			if (other.gameObject.CompareTag ("Cadavre") && isSpirit) {
				Cadavre cadavre = other.gameObject.GetComponent<Cadavre>();
				cadavre.activate();
				isActivating = true;
			}

		}


	}

	void OnTriggerEnter2D(Collider2D collider){

		if (collider.gameObject.CompareTag ("Ennemi")) {
			transform.position = checkpoint.transform.position;
		}

		if (collider.gameObject.CompareTag ("NextLevel")) {
			SceneManager.LoadScene (sceneIndex);
		}

		if (collider.gameObject.CompareTag ("CheckPoint")) {
			checkpoint = collider.gameObject;
		}



	}

	void Flip(){
		if ((facingRight && rb.velocity.x < -0.1) || (!facingRight && rb.velocity.x > 0.1)) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			facingRight = !facingRight;
		}
	}



}
