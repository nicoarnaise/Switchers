using UnityEngine;
using System.Collections;

public class Cadavre : MonoBehaviour
{

    public GameObject globalState;

	// variables de sons du Cadavre
    public AudioClip appel;
    public AudioClip merci;

	// statut =
	// 0 : Mixte
	// 1 : Physique
	// 2 : Spirituel
	public int statut;

	// isSpirit, correspondant au mode de vue du joueur
	//sert pour l'animation du mode physique ou spirituel
    public bool isSpirit;
	// notIntro permet de savoir si il n'est pas dans la scene d'intro
    public bool notIntro = true;

	// Animator
    Animator anim;

	// Variable permettant de savoir si il est libere
    public bool isFree;

	// RigidBody
    private Rigidbody2D rb;

	// On recupere l'objet GlobalState
    void Awake()
    {
        globalState = GameObject.Find("GlobalState");
    }
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
		// Si le cadavre est libere, il n'est plus actif.
        anim.SetBool("isSpirit", isSpirit);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("free"))
        {

            gameObject.SetActive(false);
        }

    }

    public void activate()
    {
		// Animation/Son de la liberation de l'ame et modification des variables de globalState
        if (!isFree)
        {
            isFree = true;
            anim.SetTrigger("liberation");
            ((AudioSource)GetComponent<AudioSource>()).PlayOneShot(merci, 1);
            if (notIntro)
            {
                GlobalState gs = globalState.GetComponent<GlobalState>();
                gs.nbCadavre++;
				gs.nbCavavreCurrent [gs.currentScene-3] ++;
            }
            anim.SetBool("isFree", true);

        }
    }

	// Son du contact entre le joueur et le cadavre
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ((AudioSource)GetComponent<AudioSource>()).PlayOneShot(appel,1);
        }
    }
}
