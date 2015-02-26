using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumpToFillAmount : MonoBehaviour {

	public GameObject player;
	float currentJumpPower = 0f;
	float perc;
	Image image;

	void Start() {

		//get the image component
		image = GetComponent<Image>();

	}

	void Update () {
		
		//get the current jump power and calculate a percentage of it, then put it in the fill amount.
		currentJumpPower = player.GetComponent<playerMovement>().currentJumpPower;
			
		perc = currentJumpPower/70f;	
	
		image.fillAmount = perc;

	}
}
