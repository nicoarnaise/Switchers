using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public Image pausePanel;
	public Level level;
	public GameObject globalState;
	public int sceneIndex;

    public AudioClip mort;

	public bool isSpirit;
	public bool isBonus;

	public bool isPhysic;
	public bool isGrounded;
	public int moveSpeed;
	public float flyInertia = 0.98f;
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

	void Awake (){
		globalState = GameObject.Find("GlobalState");
	}
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
        int lengthBackground = level.backgrounds.Length;

        Physics2D.IgnoreLayerCollision(9, 10);

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
        for (int i = 0; i < lengthBackground; i++)
        {
            backgroundAdapter back = (backgroundAdapter)level.backgrounds[i];
            back.changeUnivers(isSpirit);
        }
    }
		
	
	// Update is called once per frame
	void Update () {

		// Menu Pause
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale != 0) {
				pause ();
			} else {
				reprendre ();
			}
		}

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


		if ((rb.velocity.y == 0 || isGrounded) && Input.GetButtonDown("Jump")) {
            rb.isKinematic = false;	
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
            int lengthBackground = level.backgrounds.Length;

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
            for (int i = 0; i < lengthBackground; i++)
            {
                backgroundAdapter back = (backgroundAdapter)level.backgrounds[i];
                back.changeUnivers(isSpirit);
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

        rb.isKinematic = isGrounded && rb.velocity.y == 0 && velocity.x == 0;
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
            ((AudioSource)GetComponent<AudioSource>()).PlayOneShot(mort, 1);
        }

		if (collider.gameObject.CompareTag ("NextLevel") && !isActivating && !isTimerOn) {
            isActivating = true;
            GlobalState gs = globalState.GetComponent<GlobalState>();
			gs.currentScene++;
			SceneManager.LoadScene (gs.currentScene);


		}

		if (collider.gameObject.CompareTag ("CheckPoint")) {
			checkpoint = collider.gameObject;
		}

		if (collider.gameObject.CompareTag ("Bonus") && !isActivating && !isTimerOn) {
			if (!isBonus) {
				transform.position = new Vector3 (-105, -22, 0);
				isBonus = true;
				isActivating = true;
			} else {
				transform.position = new Vector3 (-30, -25, 0);
				isBonus = false;
				isActivating = true;
				Debug.Log (isBonus);
			}
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


	public void save(){
		GlobalState gs = globalState.GetComponent<GlobalState>();
		gs.Save ();
	}

	public void pause(){
		pausePanel.gameObject.SetActive (true);

		Time.timeScale = 0;
	}

	public void reprendre(){

		Time.timeScale = 1;
		pausePanel.gameObject.SetActive (false);

	}

	public void retourMenu(){
		Time.timeScale = 1;
		pausePanel.gameObject.SetActive (false);
		SceneManager.LoadScene (0);
	}



}
