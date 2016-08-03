using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    
	public float run_speed;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
    void Update () {
        if (Input.GetKey("up")) {
            transform.position += transform.forward * run_speed;
            animator.SetBool("Run", true);
        } else {
            animator.SetBool("Run", false);
        }
        if (Input.GetKey("right")) {
            transform.Rotate(0, 10, 0);
        }
        if (Input.GetKey ("left")) {
            transform.Rotate(0, -10, 0);
        }
    }

}
