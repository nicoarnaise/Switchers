using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
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
		transform.position = new Vector3(player.transform.position.x + offset,transform.position.y, -20);
	}


}
