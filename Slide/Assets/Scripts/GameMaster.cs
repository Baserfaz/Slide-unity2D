using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	string levelName;

	//Handles the button behaviour:
	//if start is pressed in mainmenu.
	public void StartGame() {

		Application.LoadLevel ("GameScene");	

	}

	//if quit button is pressed in mainmenu.
	public void ExitGame() {

		Application.Quit();	

	}

	//if how to play is pressed
	public void HowToPlay() {

		Application.LoadLevel ("helpScene");

	}

	//back button
	public void Back() {

		Application.LoadLevel ("mainMenuScene");

	}


	void Update() {

		levelName = Application.loadedLevelName;
		bool isInGame;

		//if not in menu -> then in game.
		if (levelName != "mainMenuScene") {

			isInGame = true;

		} else {

			isInGame = false;

		}

		//if player hits escape in the game and it's not in the menu. -> show mainmenu.
		if(Input.GetKeyDown(KeyCode.Escape) && isInGame) {

			Application.LoadLevel ("mainMenuScene");

		}

	}

}
