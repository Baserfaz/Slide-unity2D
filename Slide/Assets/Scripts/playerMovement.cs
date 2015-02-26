using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {	

	public float rotSpeed = 2f;
	public float maxJumpPower = 30f;
	public Vector3 localVel;
	public float currentJumpPower = 0f;
	float distToGround;
	public bool grounded = false;
	public Transform groundCheck;
	public Transform headCheck;
	float groundRadius = 0.5f;
	public LayerMask WhatIsGround;
	public float jumpPowerMultiplier = 7f;
	public Transform spawnPoint;
	public GameObject playerParent;
	Rigidbody2D [] children;
	float currentRotationTorso;
	float closestAngle;
	float z_axis;
	Rigidbody2D torso;
	Rigidbody2D legs;
	Rigidbody2D head;
	public float timeTillResetJump = 3f;
	float timeTillReset;
	public float jumpCooldownTimer = 1f;
	float jumpCooldown;
	public float turningSpeed = 1f;
	bool flipCoolDown = false;
	public float flipCDTime = 3f;
	float realFlipCDTime;

	void Start() {
		
		//get all the rigidbodies from the children gameobjects and save them in variables for later use.
		children = playerParent.GetComponentsInChildren<Rigidbody2D>();
		torso = children[3];
		head = children[1];
		legs = children [0];

		timeTillReset = timeTillResetJump;
		jumpCooldown = jumpCooldownTimer;
		realFlipCDTime = flipCDTime;

	}


	void FixedUpdate() {

		//rotation counter-clockwise. 
		if (Input.GetAxisRaw("Horizontal") < 0 && !grounded) {

				torso.rigidbody2D.AddTorque(rotSpeed);
				legs.rigidbody2D.AddTorque(rotSpeed);			
		} 
		
		//rotation clockwise. 
		if (Input.GetAxisRaw("Horizontal") > 0 && !grounded) {
		
				torso.rigidbody2D.AddTorque(-rotSpeed);
				legs.rigidbody2D.AddTorque(-rotSpeed);
			
		} 

		//if the player is in the air...
		if (!grounded) {
			
			//...turn the fixedAngle off. (-> makes rotating possible)
			if (torso.fixedAngle && head.fixedAngle) {
				
				torso.rigidbody2D.fixedAngle = false;
				head.rigidbody2D.fixedAngle = false;
				
			}
			
		} 
		
		//if the player is grounded, put the rotation lock on and rotate torso upwards. torso = children[3], head = children[1].
		if (grounded) {
		
			//change the z-axis of the torso
			torso.rigidbody2D.MoveRotation(z_axis);	

			//turn fixedAngle on. (-> makes rotating impossible)
			if (torso.fixedAngle == false && head.fixedAngle == false) {
				
				torso.rigidbody2D.fixedAngle = true;
				head.rigidbody2D.fixedAngle = true;

			}
			
		}

	} //end of Start()

	void Update() {

		//check if grounded
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, WhatIsGround);
		
		//get speed. right = positive, left = negative.
		localVel = transform.InverseTransformDirection(rigidbody2D.velocity);

		//get the rotation of the torso.
		currentRotationTorso = torso.transform.rotation.eulerAngles.z;
		
		//calculate the shortest angle (rotate clockwise or counter-clockwise)
		closestAngle = Mathf.DeltaAngle (currentRotationTorso, 0);
	
		//calculate rotation needed to keep the torso upwards
		//immediate rotation: z_axis = currentRotationTorso + closestAngle
		z_axis = Mathf.Lerp (currentRotationTorso, currentRotationTorso + closestAngle, Time.deltaTime * 10f);
		
		//Keep the head always upwards.
		head.rigidbody2D.MoveRotation(z_axis);

		//tick the jump cooldown timer 
		jumpCooldown -= Time.deltaTime;
		
		//if the jump power is maxed and the key is still being pressed..
		//and if the key has been held down for too long -> reset jump power.
		//also reset timetillreset and jumpcooldown.
		if (currentJumpPower >= maxJumpPower) {

			timeTillReset -= Time.deltaTime;

			if (timeTillReset < 0) {

				currentJumpPower = 0f;
				timeTillReset = timeTillResetJump;
				jumpCooldown = jumpCooldownTimer;

			}

		}
			
		//charge jump when held down.
		if (Input.GetAxisRaw("Vertical") < 0 && grounded && jumpCooldown <= 0) {
			
			if (currentJumpPower < maxJumpPower) {
				
				currentJumpPower += Time.deltaTime * jumpPowerMultiplier;
				
			}
			
		} 
		
		//jump on release.
		if (Input.GetAxisRaw("Vertical") >= 0 && grounded && currentJumpPower != 0) {
			
			//jump
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x + 0.5f, currentJumpPower);
			
			currentJumpPower = 0f;
			jumpCooldown = jumpCooldownTimer;
			
		//else if released jump on air -> no jump when grounded again
		} else if (!grounded) {
			
			currentJumpPower = 0f;
			
		}
		
		//flipcooldown-handling
		if (flipCoolDown) {
	
			if (realFlipCDTime <= 0f) {

				flipCoolDown = false;	

				realFlipCDTime = flipCDTime;

			} else {

				realFlipCDTime -= Time.deltaTime;

			}

		}		


		//turn the player around if pressed space.
		if (Input.GetKeyDown(KeyCode.Space) && !flipCoolDown) {
			
			flipCoolDown = true;

			StartTurning();

		}

		//Respawn when pressed R
		if (Input.GetKeyDown(KeyCode.R)) {
			
			Application.LoadLevel(Application.loadedLevel);
			
		}

	} //end of Update()



	//turn the character 180 degrees on Y-axis.
	void StartTurning() {
		
		//grab all children and turn their x-axis around.
		foreach(Rigidbody2D child in children) {

			Vector3 CurScale = child.transform.localScale;
	
			if(CurScale.x == 1) {

				CurScale.x = -1;
				
				child.transform.localScale = CurScale;

			} else if (CurScale.x == -1) {

				CurScale.x = 1;
				
				child.transform.localScale = CurScale;

			}

		}

	}




} // end class

