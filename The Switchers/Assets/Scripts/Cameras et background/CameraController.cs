using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Variables initialisant la position de la camera
	public GameObject player;
	public float startLevelPosition;
	public float finishLevelPosition;

	public float verticalInit;

	private float topLimit;
	private float botLimit;

	private float offset;
	private float verticalTopOffset;
	private float verticalBotOffset;

	// Initialisation position
	void Start () {
        offset = 0;
		topLimit = verticalInit + 6;
		botLimit = verticalInit - 6;
		verticalTopOffset = topLimit - verticalInit;
		verticalBotOffset = verticalInit - botLimit;
	}


	void LateUpdate () {

		// On se cale sur la position du joueur

		PlayerController pc = player.GetComponent<PlayerController> ();

		float positionY = player.transform.position.y;
		float positionX = player.transform.position.x;

		float cameraX;
		float cameraY;

		// Si on est dans le niveau bonus, on se cale sur la position du joueur
		// Sinon, on gere la position verticale puis horizontale :
		// Pour la position verticale, on a une limite haute et basse. Au dela de ces
		// limites, la camera suit plus ou moins le joueur avec un offset, sinon elle a une position fixe.
		// Pour la position horizontale, on a une limite gauche et droite. A l'interieur de
		// ces limites, la camera suit plus ou moins le joueur avec un offset, sinon elle a une position fixe.
		if (pc.isBonus) {
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -30);
		} else {

			if (positionY > topLimit) {
				cameraY = positionY - verticalTopOffset;
			} else {
				if (positionY < topLimit && positionY > botLimit) {
					cameraY = verticalInit;
				} else {
					cameraY = positionY + verticalBotOffset;
				}
			}

			if (positionX < startLevelPosition) {
				cameraX = startLevelPosition;
			} else {
				if (positionX > startLevelPosition && positionX < finishLevelPosition) {
					cameraX = player.transform.position.x + offset;
				} else {
					cameraX = finishLevelPosition;
				}
			}

			transform.position = new Vector3 (cameraX, cameraY, -30);
		}
	}
}
