using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ameFinController : MonoBehaviour
{
    public float moveSpeed;
    public bool aBouge = false;
    public int nbCadavre;

    public GameObject globalState;

    //gestion du texte
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

    public void envoleToi()
    {
        GlobalState gs = globalState.GetComponent<GlobalState>();
        nbCadavre = gs.nbCadavre;
        if(nbCadavre >= 25)
        {
            envol = true;
            fin = true;
            if (pageNumber == 0)
                PageTurner();
        }
        else
        {
            fin = false;
            if (pageNumber == 0)
                PageTurner();
        }
    }

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
        if (collider.gameObject.CompareTag("Respawn"))
        {
            envol = false;
            aBouge = true;
        }
    }


}

