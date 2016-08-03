using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float followSpeed;

	Vector3 diff;

	// Use this for initialization
	void Start () {
		diff = target.transform.position - transform.position;
	}
	
	void LateUpdate () {
		if(target != null) {
			transform.position = Vector3.Lerp(
				transform.position,
				target.transform.position - diff,
				Time.deltaTime * followSpeed
			);
		}
	}
}
