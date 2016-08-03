using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour {

	private GameObject ShotManager;

	void Start() {
		ShotManager = GameObject.FindGameObjectWithTag("PlayerCharacter");
	}
	void OnTriggerEnter(Collider other) {
		if(other.tag == "SoftWall") {
			Destroy(other.gameObject);
			ShotManager.SendMessage("DecrimentShot");
			Destroy(this.gameObject);
	    } else if(other.tag == "Wall") {
			ShotManager.SendMessage("DecrimentShot");
			Destroy(this.gameObject);
		}
	}
}
