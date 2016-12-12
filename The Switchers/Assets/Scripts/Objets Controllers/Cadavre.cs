using UnityEngine;
using System.Collections;

public class Cadavre : MonoBehaviour
{

    public GameObject globalState;

    public AudioClip appel;
    public AudioClip merci;
    public int statut;
    public bool isSpirit;
    public bool notIntro = true;
    Animator anim;

    public bool isFree;

    private Rigidbody2D rb;


    void Awake()
    {
        globalState = GameObject.Find("GlobalState");
    }
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isSpirit", isSpirit);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("free"))
        {

            gameObject.SetActive(false);
        }

    }

    public void activate()
    {

        if (!isFree)
        {
            isFree = true;
            anim.SetTrigger("liberation");
            ((AudioSource)GetComponent<AudioSource>()).PlayOneShot(merci, 1);
            if (notIntro)
            {
                GlobalState gs = globalState.GetComponent<GlobalState>();
                gs.nbCadavre++;
				gs.nbCavavreCurrent [gs.currentScene];
            }
            anim.SetBool("isFree", true);

        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            ((AudioSource)GetComponent<AudioSource>()).PlayOneShot(appel,1);
        }
    }
}
