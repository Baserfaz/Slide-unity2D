using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasFollow : MonoBehaviour {

	public Canvas canvas;
	public Vector2 offset;
	public GameObject playerHead;
	
	void Update () {
		
		//Get the current position of player's head, add offset to the bar location and move the canvas.
		Vector2 curPos = playerHead.transform.position;
		curPos.x += offset.x;
		curPos.y += offset.y;

		canvas.transform.position = curPos;

	}
}
