using UnityEngine;
using System.Collections;

public class finPlayer : MonoBehaviour {

    // Ce script controle le Switcher pendant la cinematique pour que le scenario soit respecte

	public bool isSpirit;
    public Level level;
    public GroundChecker groundChecker;
    public Vector2 destination;
    public float moveSpeed;
    // reccueille l'ame pour pouvoir lancer la suite de scenario.
    public ameFinController ame;

    private Animator animator;
    private Rigidbody2D rb;
    // etat permet de savoir la progression dans le scenario.
    private int etat;
    private Vector3 init;

    // Use this for initialization
    void Start()
    {
        etat = 0;
        init = ame.transform.position;
        isSpirit = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isGround", true);
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // etape 1 : avancer jusqu'au milieu de la scene
        if (etat == 0)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = moveSpeed;
            animator.SetFloat("hSpeed", Mathf.Abs(rb.velocity.x));
            rb.velocity = velocity;
        }
        // etape 3 : transformer le joueur en ame
        if (etat == 2)
        {
            etat = 3;
            ame.gameObject.SetActive(true);
            Invoke("kill", 1);
        }
        // etape 2 : stopper le joueur et passer en mode spirituel
        if (etat == 1)
        {
            // on stoppe le joueur
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            animator.SetFloat("hSpeed", 0);
            etat = 2;
            // on passe en mode esprit
            isSpirit = !isSpirit;
            animator.SetBool("isSpirit", isSpirit);
            // on passe les elements de la scene en mode esprit
            int lengthPlateformes = level.plateformes.Length;
            int lengthBackground = level.backgrounds.Length;

            Plateforme plateforme = (Plateforme)level.plateformes[0];
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
            for (int i = 0; i < lengthBackground; i++)
            {
                backgroundAdapter back = (backgroundAdapter)level.backgrounds[i];
                back.changeUnivers(isSpirit);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Quand on arrive au milieu de l'ecran, passer a l'etape 2
        if (collider.gameObject.CompareTag("Cadavre"))
        {
            etat = 1;
        }
    }

    // lancer la cinematique de l'ame et disparaitre
    void kill()
    {
        ame.envoleToi();
        gameObject.SetActive(false);
    }
}
