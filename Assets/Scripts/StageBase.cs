using UnityEngine;
using System.Collections;

public class StageBase : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag == "PlayerCharacter") {
			other.gameObject.SendMessage("StageReload");
		}
	}
}
