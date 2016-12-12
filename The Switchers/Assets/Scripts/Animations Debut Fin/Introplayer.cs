using UnityEngine;
using System.Collections;

public class Introplayer : MonoBehaviour
{

    public bool isSpirit;
    public Level level;
    public GroundChecker groundChecker;
    public Vector2 destination;
    public float moveSpeed;
    public ameController ame;

    private Animator animator;
    private Rigidbody2D rb;
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
        if (etat == 0)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = moveSpeed;
            animator.SetFloat("hSpeed", Mathf.Abs(rb.velocity.x));
            rb.velocity = velocity;
        }
        if (!level.cadavres[0].isActiveAndEnabled && ame.transform.position == init)
        {
            ame.gameObject.SetActive(true);
            ame.envoleToi();
        }
        if (etat == 2)
        {
            etat = 3;
            level.cadavres[0].activate();
        }
        if (etat == 1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            animator.SetFloat("hSpeed", 0);
            etat = 2;
            isSpirit = !isSpirit;
            animator.SetBool("isSpirit", isSpirit);
            int lengthCadavres = level.cadavres.Length;
            int lengthPlateformes = level.plateformes.Length;
            int lengthBackground = level.backgrounds.Length;

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
            for (int i = 0; i < lengthBackground; i++)
            {
                backgroundAdapter back = (backgroundAdapter)level.backgrounds[i];
                back.changeUnivers(isSpirit);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cadavre"))
        {
            etat = 1;
        }
    }
}