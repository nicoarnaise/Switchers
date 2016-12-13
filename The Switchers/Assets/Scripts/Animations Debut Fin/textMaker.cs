using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class textMaker : MonoBehaviour {

    // Cette classe permet l'affichage du texte de Bob dans les niveaux 0 et 1
    // qui sont des niveaux tutoriaux.
        
    // l'objet texte
    public Text textBox;
    // le conteneur des infobulles de Bob 
    public GameObject Panel;
    // le numero de la String a afficher
    public int pageNumber = 0;
    // le tableau des Strings a afficher
    public string[] pageContent;

    // Use this for initialization
    void Start () {
        textBox = Panel.GetComponentInChildren<Text>();
        Panel.SetActive(false);
        // permet de lancer l'affichage du texte 1s apres le chargement de la scene.
        Invoke("PageTurner", 1);
    }

    // permet de changer la String a afficher.
    void PageTurner()
    {
        pageNumber++;
        Invoke("PageTurner", 4);
    }

    // Update is called once per frame
    void Update () {
        // si on doit commencer a afficher quelque chose.
        if (pageNumber == 1)
            Panel.SetActive(true);
        // si on est arrive a la fin de ce qu'on doit afficher.
        if (pageNumber == pageContent.Length + 1)
            Panel.SetActive(false);
        // si on a quelque chose a afficher
        if (pageNumber >= 1 && Panel.activeInHierarchy)
            textBox.text = pageContent[pageNumber - 1];
    }
}
