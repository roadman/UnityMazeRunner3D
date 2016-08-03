using UnityEngine;
using System.Collections;

public class GoalCheck : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag != "PlayerCharacter") {
			return;
		}
		
		other.gameObject.SendMessage("SetWin");
		
		GameObject gameController = GameObject.FindGameObjectWithTag("Game");
		gameController.SendMessage("SetGoal");
	}
}
