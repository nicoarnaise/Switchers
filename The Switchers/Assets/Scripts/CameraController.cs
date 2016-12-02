using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float startLevelPosition;
	public float finishLevelPosition;

	public float verticalInit;

	private float topLimit;
	private float botLimit;
	// Use this for initialization

	private float offset;
	private float verticalTopOffset;
	private float verticalBotOffset;

	// Use this for initialization
	void Start () {
        offset = 0;
		topLimit = verticalInit + 6;
		botLimit = verticalInit - 6;
		verticalTopOffset = topLimit - verticalInit;
		verticalBotOffset = verticalInit - botLimit;
	}

	// Update is called once per frame
	void LateUpdate () {
		float positionY = player.transform.position.y;
		float positionX = player.transform.position.x;

		float cameraX;
		float cameraY;

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

		transform.position = new Vector3 (cameraX, cameraY, -20);
	}
}
