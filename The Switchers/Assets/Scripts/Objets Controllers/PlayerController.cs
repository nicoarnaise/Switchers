using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	// Texte du nombre d'ame liberees
	public Text ame;
	// Texte du nombre de mort
	public Text textMort;

	// Menu pause
	public Image pausePanel;

	public Level level;
	public GameObject globalState;
	public GroundChecker groundChecker;

	// Son de la mort du joueur
    public AudioClip mort;

	// Controle le mode de vue du joueur
	public bool isSpirit;

	// Si le joueur se trouve dans le niveau bonus
	public bool isBonus;

	// Si le joueur peut sauter
	public bool isGrounded;
	// Vitesse du joueur
	public int moveSpeed;
	// Faculté du joueur a se deplacer dans les airs.
	public float flyInertia = 0.98f;
	// Vitesse de saut
	public int jumpSpeed;
	// Si le joueur a saute
	public bool hasJumped;

	// Gestion des Timers permettant d'allouer un temps pour une action pour ne pas qu'elle
	// se repete
	public bool isActivating;
	public bool isTimerOn;
	public float period;
	public float timer;


	// Dernier point de sauvegarde du joueur
	public GameObject checkpoint;
	// Animator
    private Animator animator;
	// RigidBody
	private Rigidbody2D rb;
	// Direction du joueur
	public bool facingRight;

	// On recupere le globalState
	void Awake (){
		globalState = GameObject.Find("GlobalState");
	}

	// A l'initialisation, le monde est en mode physique, on passe tout
	// les objets de la scene en mode physique
	void Start () {
		isTimerOn = false;
		isActivating = false;
		hasJumped = false;
        period = 0.1f;
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

		//-	On recupere les tableaux des elements de notre GameObject Level.
		//-	Pour chaque tableau, on parcourt les elements
		//-	On recupere l’element, on change son attribut isSpirit.
		//-	On desactive l’element si c’est un element physique et qu’on est en mode esprit, ou si c’est 
		//un element spirituel et qu’on est en mode physique. On active l’element sinon

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

		// On ecrit le texte des ames et morts.
		setText ();

		// Menu Pause
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Time.timeScale != 0) {
				pause ();
			} else {
				reprendre ();
			}
		}

		// Gere le changement de direction du joueur
		Flip();

		// On allour un temps entre chaque action du joueur pour eviter 2 actions consecutives non voulues
		if (isActivating) {
			timer = Time.time;
			isActivating = false;
			isTimerOn = true;
		}
			
		if (isTimerOn && Time.time > timer + period) {
			isTimerOn = false;
		}

		// Si le joueur peut sauter et qu'il veut sauter, il saute
		if ((rb.velocity.y == 0 || isGrounded) && Input.GetButtonDown("Jump")) {
            rb.isKinematic = false;	
			animator.SetBool ("isGround", false);
            rb.AddForce(new Vector2(0f,jumpSpeed), ForceMode2D.Impulse);
			}

		// Changement de vue
		//-	Verification de l’appui du bouton « touche haut » clavier.
		//-	Changement de l’attribut booleen isSpirit du player, correspondant au mode Physique ou Esprit.
		//-	Changement de l’animation du player.
		//-	On recupere les tableaux des elements de notre GameObject Level.
		//-	Pour chaque tableau, on parcourt les elements
		//-	On recupere l’element, on change son attribut isSpirit.
		//-	On desactive l’element si c’est un element physique et qu’on est en mode esprit, ou si c’est 
		//un element spirituel et qu’on est en mode physique. On active l’element sinon.

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

	// On defini le texte pour le nombre de mort et le nombre d'ame liberees
	public void setText(){
		GlobalState gs = globalState.GetComponent<GlobalState>();
		ame.text = "Âmes libérées : " + gs.nbCavavreCurrent [gs.currentScene-3] + "/" +
			gs.nbCadavreTotal [gs.currentScene-3];
		textMort.text = "Morts : " + gs.nbMort;
	}


	void OnTriggerStay2D(Collider2D other){


		if (Input.GetKeyDown(KeyCode.DownArrow) && !isActivating && !isTimerOn){

			// Activation d'un levier
			if (other.gameObject.CompareTag ("Actionneur")) {
				Actionneur actionneur = other.gameObject.GetComponent<Actionneur>();
				actionneur.activate();
				isActivating = true;
			}

			// Liberation d'une ame
			if (other.gameObject.CompareTag ("Cadavre") && isSpirit) {
				Cadavre cadavre = other.gameObject.GetComponent<Cadavre>();
				cadavre.activate();
				isActivating = true;
			}

		}


	}

	void OnTriggerEnter2D(Collider2D collider){

		// Si le joueur entre en contact avec un ennemi, humain, ou bas de l'ecran, il meurt, 
		// et se retrouve transporte au dernier point de sauvegarde (checkpoint)
		if (collider.gameObject.CompareTag ("Ennemi") && !isActivating && !isTimerOn) {
			isActivating = true;
			GlobalState gs = globalState.GetComponent<GlobalState>();
			gs.nbMort++;
			transform.position = checkpoint.transform.position;
            ((AudioSource)GetComponent<AudioSource>()).PlayOneShot(mort, 1);
        }

		// Si le joueur entre en contact avec la zone de fin, il change de niveau
		if (collider.gameObject.CompareTag ("NextLevel") && !isActivating && !isTimerOn) {
            isActivating = true;
            GlobalState gs = globalState.GetComponent<GlobalState>();
			gs.currentScene++;
			SceneManager.LoadScene (gs.currentScene);
		}

		// Si le joueur entre en contact avec un point de sauvegarde, son point de sauvegarde est
		// change.
		if (collider.gameObject.CompareTag ("CheckPoint")) {
			checkpoint = collider.gameObject;
		}

		// Si le joueur entre avec la zone de bonus, il est teleporte.
		if (collider.gameObject.CompareTag ("Bonus") && !isActivating && !isTimerOn) {
			if (!isBonus) {
				transform.position = new Vector3 (-105, -22, 0);
				isBonus = true;
				isActivating = true;
			} else {
				transform.position = new Vector3 (-30, -25, 0);
				isBonus = false;
				isActivating = true;
			}
		}

	}

	// Change la direction du joueur selon la direction initiale et sa vitesse
	void Flip(){
		if ((facingRight && rb.velocity.x < -0.1) || (!facingRight && rb.velocity.x > 0.1)) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
			facingRight = !facingRight;
		}
	}

	// Appelle la fonction de sauvegarde de GlobalState
	public void save(){
		GlobalState gs = globalState.GetComponent<GlobalState>();
		gs.Save ();
	}

	// Pause le jeu
	public void pause(){
		pausePanel.gameObject.SetActive (true);

		Time.timeScale = 0;
	}

	// Reprend le jeu
	public void reprendre(){

		Time.timeScale = 1;
		pausePanel.gameObject.SetActive (false);

	}

	// Retourne au menu principal du jeu
	public void retourMenu(){
		Time.timeScale = 1;
		pausePanel.gameObject.SetActive (false);
		SceneManager.LoadScene (0);
	}



}
