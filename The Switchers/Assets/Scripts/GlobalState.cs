using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalState : MonoBehaviour {

	public static GlobalState globalState;
	public Button b1;
	public Button b2;
	public Button b3;

	public int nbCadavre;
	public int nbCadavreTot;
	public int[] nbCavavreCurrent;
	public int[] nbCadavreTotal; 
	public int currentScene;
	public bool hasStarted;

	void Awake() {
		//DontDestroyOnLoad (transform.gameObject);
		if (globalState == null) {
			DontDestroyOnLoad (transform.gameObject);
			globalState = this;
		} else if (globalState != this) {
			Destroy (gameObject);
		}
	}

	void Update(){

		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			findButtons ();
		} 
		
	}
		

	public void findButtons(){
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

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/gameInfo.dat");

		hasStarted = true;
		GameData data = new GameData (); 
		data.nbCadavre = nbCadavre;
		data.nbCadavreTot = nbCadavreTot;
		data.currentScene = currentScene;
		data.nbCavavreCurrent = nbCavavreCurrent;
		data.nbCadavreTotal = nbCadavreTotal;
		data.hasStarted = hasStarted;

		bf.Serialize (file, data);
		file.Close ();

	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "gameInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "gameInfo.dat", FileMode.Open);
			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();

			nbCadavre = data.nbCadavre;
			nbCadavreTot = data.nbCadavreTot;
			currentScene = data.currentScene;
			nbCavavreCurrent = data.nbCavavreCurrent;
			nbCadavreTotal = data.nbCadavreTotal;
		}
	}

	public void newGame(){
		nbCadavre = 0;
		currentScene = 1;
		for (int i=0; i<8; i++){
			nbCavavreCurrent [i] = 0;
		}
		SceneManager.LoadSceneAsync(1);
	}

	public void loadGame(){
		Load ();
		SceneManager.LoadScene (currentScene);
	}

	public void quit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}

[Serializable]
class GameData{
	public int nbCadavre;
	public int nbCadavreTot;
	public int[] nbCavavreCurrent;
	public int[] nbCadavreTotal; 
	public int currentScene;
	public bool hasStarted;
}