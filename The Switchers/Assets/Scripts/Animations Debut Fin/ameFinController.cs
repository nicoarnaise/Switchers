using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ameFinController : MonoBehaviour
{
    // Ce script permet la gestion de la fin du scenario de la scene de fin 

    public float moveSpeed;
    public bool aBouge = false;
    // ici nbcadavre est remonte dans Unity pour tester les deux fins
    public int nbCadavre;

    public GameObject globalState;

    //gestion du texte (cf textMaker.cs)
    // ici on a deux dialogues differents suivant si le joueur a libere toutes les ames ou non
    public Text textBox;
    public GameObject Panel;
    public int pageNumber = 0;
    public string[] pageContent;
    public string[] fausseFin;
    
    public bool envol;
    private bool fin;
    private Rigidbody2D rb;
    private Vector3 pos;

    void Awake()
    {
        // on recupere le global state dans lequel est stocke le nombre d'ames secourues
        globalState = GameObject.Find("GlobalState");
    }

    // Use this for initialization
    void Start()
    {
        textBox = Panel.GetComponentInChildren<Text>();
        rb = GetComponent<Rigidbody2D>();
        envol = false;
        pos = transform.position;
        Panel.SetActive(false);
    }

    void PageTurner()
    {
        pageNumber++;
        Invoke("PageTurner", 7);
    }

    // Cette fonction permet la remise a zero des compteurs et le retour a la premiere scene jouable
    void debuter()
    {
        GlobalState gs = globalState.GetComponent<GlobalState>();
        gs.currentScene = 3;
        gs.nbCadavre = 0;
		int i;
		for (i=0; i<8; i++){
			gs.nbCavavreCurrent[i] = 0;
		}
        SceneManager.LoadScene(gs.currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pageNumber == 1)
            Panel.SetActive(true);
        // si on a libere toutes les ames, on affiche le texte de remerciement
        if (fin)
        {
            if (pageNumber == pageContent.Length + 1)
            {
                Panel.SetActive(false);
                Invoke("humanMaker", 0.5f);
            }
            if (pageNumber >= 1 && Panel.activeInHierarchy)
                textBox.text = pageContent[pageNumber - 1];
        }
        else
        {
            // sinon on previent le joueur avant de le faire recommencer le jeu
            if (pageNumber == fausseFin.Length + 1)
            {
                Panel.SetActive(false);
                Invoke("debuter", 0.5f);
            }
            if (pageNumber >= 1 && Panel.activeInHierarchy)
                textBox.text = fausseFin[pageNumber - 1];
        }

        if (envol)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            Vector2 velocity = rb.velocity;
            velocity.y = moveSpeed;
            rb.velocity = velocity;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    // lancement de la cinematique avec le jugement de Bob sur le travail accompli
    public void envoleToi()
    {
        // on recupere le nombre de cadavres 
        GlobalState gs = globalState.GetComponent<GlobalState>();
        nbCadavre = gs.nbCadavre;
        // si on a sauve tous les cadavres :
        if(nbCadavre >= 25)
        {
            envol = true;
            // on dit merci
            fin = true;
            if (pageNumber == 0)
                PageTurner();
        }
        else
        {
            // sinon on fait recommencer
            fin = false;
            if (pageNumber == 0)
                PageTurner();
        }
    }

    // permet ici de quitter le jeu, pourra etre remplace plus tard par une joli cinematique de fin ou un retour vers le menu principal.
    void humanMaker()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
		Application.Quit ();
        #endif
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // stoppe l'ascension de l'ame et indique qu'elle a bouge.
        if (collider.gameObject.CompareTag("Respawn"))
        {
            envol = false;
            aBouge = true;
        }
    }


}

