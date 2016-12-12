using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class passeurControl : MonoBehaviour {

    public GameObject globalState;


    void Awake()
    {
        globalState = GameObject.Find("GlobalState");
    }

    // Use this for initialization
    void Start()
    {
        GlobalState gs = globalState.GetComponent<GlobalState>();
        gs.currentScene++;
        SceneManager.LoadScene(gs.currentScene);
    }
}
