using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public GameObject player;
	
	void Update () {
		
		//Get the player's position in x-axis and y and keep the z same.
		Camera.main.transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, -10);
		
	}
}
