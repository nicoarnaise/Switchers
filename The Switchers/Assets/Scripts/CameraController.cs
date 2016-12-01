using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public int startLevelPosition;
	public int finishLevelPosition;
	// Use this for initialization

	private float offset;

	// Use this for initialization
	void Start () {
        //offset = transform.position.x - player.transform.position.x;
        offset = 0;
        //transform.position = new Vector3(player.transform.position.x + offset,player.transform.position.y + offset);
	}

	// Update is called once per frame
	void LateUpdate () {
		float positionX = player.transform.position.x;
		if (positionX < startLevelPosition) {
			transform.position = new Vector3 ((float)startLevelPosition, transform.position.y, -20);
		} else {
			if (positionX > startLevelPosition && positionX < finishLevelPosition) {
				transform.position = new Vector3 (player.transform.position.x + offset, transform.position.y, -20);
			} else {
				transform.position = new Vector3 ((float)finishLevelPosition, transform.position.y, -20);
			}
		}
	}
}
