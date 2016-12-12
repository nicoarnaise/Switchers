using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textMaker : MonoBehaviour {

    //gestion du texte
    public Text textBox;
    public GameObject Panel;
    public int pageNumber = 0;
    public string[] pageContent;

    // Use this for initialization
    void Start () {
        textBox = Panel.GetComponentInChildren<Text>();
        Panel.SetActive(false);
        Invoke("PageTurner", 1);
    }

    void PageTurner()
    {
        pageNumber++;
        Invoke("PageTurner", 4);
    }

    // Update is called once per frame
    void Update () {
        if (pageNumber == 1)
            Panel.SetActive(true);
        if (pageNumber == pageContent.Length + 1)
        {
            Panel.SetActive(false);
        }
        if (pageNumber >= 1 && Panel.activeInHierarchy)
            textBox.text = pageContent[pageNumber - 1];
    }
}
