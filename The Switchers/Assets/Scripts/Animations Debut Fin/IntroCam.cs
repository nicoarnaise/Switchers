using UnityEngine;
using System.Collections;

public class IntroCam : MonoBehaviour {

    // Ce script permet a la camera de garder l'ame du joueur au centre et
    // de lancer l'animation du background quand cette derniere a atteind
    // le haut de sa progression (quand les elements sont tous hors-champs)

    public ameController ame;
    private Animator anim;
    private Vector3 pos;
	
    void Start()
    {
        anim = GetComponent<Animator>();
        pos = transform.position;
    }

	// Update is called once per frame
	void Update () {
        // si il y a une ame, la suivre
        if(ame.gameObject.activeInHierarchy)
            transform.position = new Vector3(transform.position.x, ame.transform.position.y,transform.position.z);
        // si on a bouge, sauvegarder la derniere position
        if (!pos.Equals(transform.position))
            pos = transform.position;
        else if(ame.aBouge)
        {
            //si l'ame a bouge et que l'on ne bouge plus, lancer l'annimation
            anim.speed = 0.1f;
        }
	}

    // Cette fonction est lancee par l'animateur des la creation de la camera
    public void wait()
    {
        // on attend le bon moment pour lancer l'animation
        anim.speed = 0;
    }
}
