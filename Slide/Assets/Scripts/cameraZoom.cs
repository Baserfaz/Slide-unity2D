using UnityEngine;
using System.Collections;

public class cameraZoom : MonoBehaviour {

	public float zoomSpeed = 1f;
	public GameObject Player;
	playerMovement PlayerMovement;
	float currentSpeed = 0f;
	float currentZoom;

	void Start() {

		// Get the springy_player's script.
		PlayerMovement = Player.GetComponentInChildren<playerMovement>();

	}

	void Update () {
		
		//Save the state of cameras zoom in variable.
		currentZoom = Camera.main.orthographicSize;

		//Get the velocity of the player on x-axis.
		currentSpeed = Mathf.Abs(PlayerMovement.localVel.x);

		//move the camera according to the current movement speed.
		Camera.main.orthographicSize = Mathf.Lerp(currentZoom, currentSpeed * 0.6f, zoomSpeed * Time.deltaTime);	

	}
}
