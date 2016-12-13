using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ameController : MonoBehaviour
{
    // Cette classe permet la gestion de la fin de la cinematique d'intro

    public float moveSpeed;
    public bool aBouge = false;

    public GameObject globalState;

    //gestion du texte (cf textMaker.cs)
    public Text textBox;
    public GameObject Panel;
    public int pageNumber = 0;
    public string[] pageContent;

    public bool envol;
    private Rigidbody2D rb;
    private Vector3 pos;
    private bool aDebute = false;

    void Awake()
    {
        // 
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

    void debuter()
    {
        // on verifie qu'on ne lance pas deux fois le niveau 1 (qui reviendrait a lancer le niveau 2)
        if (!aDebute)
        {
            aDebute = true;
            // et on lance le niveau 1.
            GlobalState gs = globalState.GetComponent<GlobalState>();
            gs.currentScene++;
            SceneManager.LoadScene(gs.currentScene);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pageNumber == 1)
            Panel.SetActive(true);
        if (pageNumber == pageContent.Length + 1)
        {
            Panel.SetActive(false);
            Invoke("debuter", 0.5f);
        }
        if(pageNumber >= 1 && Panel.activeInHierarchy)
            textBox.text = pageContent[pageNumber-1];

        // si on a atteind dans le scenario le moment de l'ascension de l'ame
        if (envol)
        {
            // alors on la fait monter
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            Vector2 velocity = rb.velocity;
            velocity.y = moveSpeed;
            rb.velocity = velocity;
        }
        else
        {
            // sinon on l'empeche de bouger
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // on receptionne du Switcher le fait qu'il nous laisse la suite du scenario
    public void envoleToi()
    {
        envol = true;
        if (pageNumber == 0)
            PageTurner();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // si on a atteind le haut de notre ascension, on arrete et on dit qu'on est en haut (pour l'annimation du background)
        if (collider.gameObject.CompareTag("Respawn"))
        {
            envol = false;
            aBouge = true;
        }
    }


}
