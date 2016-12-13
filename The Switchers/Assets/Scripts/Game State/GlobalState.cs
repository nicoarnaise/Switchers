using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalState : MonoBehaviour {

	// On a besoin d'une variable globalState pour qu'une seule instance de la classe soit creee,
	// lors du Awake()
	public static GlobalState globalState;

	// Les 3 boutons du menu principal, c'est le globalState qui gere les fonctions onClick ()
	// de ces derniers. 
	public Button b1;
	public Button b2;
	public Button b3;

	// Variables globales du jeu tels que le nombre de morts, de cadavres liberes, de la scene courante.
	public int nbMort;
	public int nbCadavre;
	public int nbCadavreTot;
	public int[] nbCavavreCurrent;
	public int[] nbCadavreTotal; 
	public int currentScene;
	public bool hasStarted;

	void Awake() {
		// Utilisation du design Pattern Singleton, si c'est la première fois que ce script est appele, il est cree, 
		// sinon, il est detruit car il y en a deja un
		if (globalState == null) {
			DontDestroyOnLoad (transform.gameObject);
			globalState = this;
		} else if (globalState != this) {
			Destroy (gameObject);
		}
	}
		

	void Update(){
		// On cherche les boutons et on leur attribut les fonctions onClick() uniquement dans le menu principal
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			findButtons ();
		} 
		
	}
		

	public void findButtons(){

		// Recherche des boutons dans la scène, et attribution des fonctions onClick()
			b1 = GameObject.Find("Canvas/Panel/Panel/NP").GetComponent<Button>();
			b1.onClick.AddListener (() => {
				newGame ();
			});
			b2 = GameObject.Find("Canvas/Panel/Panel/Continuer").GetComponent<Button>();
			b2.onClick.AddListener (() => {
				loadGame ();
			});
			b3 = GameObject.Find("Canvas/Panel/Panel/Quitter").GetComponent<Button>();
			b3.onClick.AddListener (() => {
				quit ();
			});

		if (!hasStarted) {
			b2.interactable = false;
		} else {
			b2.interactable = true;
		}
		
	}


	// Methode permettant de sauvegarder les donnees de ce script
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gameInfo.dat");

		hasStarted = true;
		GameData data = new GameData (); 
		data.nbMort = nbMort;
		data.nbCadavre = nbCadavre;
		data.nbCadavreTot = nbCadavreTot;
		data.currentScene = currentScene;
		data.nbCavavreCurrent = nbCavavreCurrent;
		data.nbCadavreTotal = nbCadavreTotal;
		data.hasStarted = hasStarted;

		bf.Serialize (file, data);
		file.Close ();
	}

	// eéthode permettant de charger les donnees d'un fichier dans le globalState
	public void Load(){
		if (File.Exists (Application.persistentDataPath + "gameInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "gameInfo.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			nbMort = data.nbMort;
			nbCadavre = data.nbCadavre;
			nbCadavreTot = data.nbCadavreTot;
			currentScene = data.currentScene;
			nbCavavreCurrent = data.nbCavavreCurrent;
			nbCadavreTotal = data.nbCadavreTotal;
		}
	}

	//Methode permettant de lancer une nouvelle partie
	public void newGame(){
		nbMort = 0;
		nbCadavre = 0;
		currentScene = 1;
		for (int i=0; i<8; i++){
			nbCavavreCurrent [i] = 0;
		}
		SceneManager.LoadSceneAsync(1);
	}

	// Suite de Load ...
	public void loadGame(){
		Load ();
		SceneManager.LoadScene (currentScene);
	}

	// Quitter le programme
	public void quit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}

// Creation d'une classe serialisable pour sauvegarder les donnees
[Serializable]
class GameData{
	public int nbMort;
	public int nbCadavre;
	public int nbCadavreTot;
	public int[] nbCavavreCurrent;
	public int[] nbCadavreTotal; 
	public int currentScene;
	public bool hasStarted;
}