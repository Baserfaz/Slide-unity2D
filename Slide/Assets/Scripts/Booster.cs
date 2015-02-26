using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	public float boostAmount = 10f;
	Vector2 currentVel;	

	void OnTriggerEnter2D (Collider2D col) {
		
		if(col.gameObject.layer == 8) {

			GameObject parent = col.gameObject.transform.parent.gameObject;
			Rigidbody2D [] children = parent.gameObject.GetComponentsInChildren<Rigidbody2D>();
			currentVel = children[3].velocity;

			currentVel.x += boostAmount;
			children[3].velocity = currentVel;

			Destroy (gameObject);
		}

	}
}
