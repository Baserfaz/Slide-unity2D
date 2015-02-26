using UnityEngine;
using System.Collections;

public class particleEffect : MonoBehaviour {

	Transform [] children;
	Transform [] childrenOfChild;

	void Start() {

		//get all children
		children = gameObject.GetComponentsInChildren<Transform>();
				
		//get the children of children[0]
		childrenOfChild = children[1].GetComponentsInChildren<Transform>();


		//if a children has a tag...   disable emission.
		foreach (Transform child in children) {
			if (child.gameObject.tag == "groundCheck") {
				child.particleSystem.enableEmission = false;
			}
		}
	}

	void Update() {

		// if the player is grounded puff some snow!
		if (children[1].GetComponent<playerMovement>().grounded) {
			childrenOfChild[2].particleSystem.enableEmission = true;
		} else {
			childrenOfChild[2].particleSystem.enableEmission = false;
		}

	}
}
