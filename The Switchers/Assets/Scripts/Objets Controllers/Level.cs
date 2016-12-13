using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	// On sauvegarde les differents objets de la scene dans des tableaux
	// utilise dans playercontroller pour changer l'etat des objets physique / spirituels
	public Plateforme[] plateformes; 
	public Ennemi[] ennemis; 
	public Actionneur[] actionneurs;
	public Cadavre[] cadavres; 
	public Mur[] murs;
    public backgroundAdapter[] backgrounds;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
