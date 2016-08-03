using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator animator;
    
	bool isPlay;
	int shotCount;
    
	public float 		runSpeed;
	public float 		shotSpeed;
	public GameObject 	GameController;
	public GameObject 	ShotHolder;
	public int			MaxShotNum;
	
	public GameObject PrefabShot;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        isPlay = false;
        shotCount = 0;
    }
	
    void Update () {
    	if(!isPlay) {
			return;
		}
		
		if (Input.GetKey("up")) {
            transform.position += transform.forward * Time.deltaTime * runSpeed;
            animator.SetBool("Run", true);
        } else {
            animator.SetBool("Run", false);
        }
        
		if (Input.GetKey("down")) {
			if(shotCount < MaxShotNum) {
				shotCount++;
				GameController.SendMessage("DecrimentTimesec");
				Shot();
			}
		}
        
        if (Input.GetKey("right")) {
            transform.Rotate(0, 10, 0);
		} else if (Input.GetKey ("left")) {
            transform.Rotate(0, -10, 0);
        }
    }
    
	public void DecrimentShot() {
		if(shotCount > 0) {
			shotCount--;
		}
	}
    
	void Shot() {
		GameObject obj = (GameObject)Instantiate(
			PrefabShot,
			this.transform.position + new Vector3(0, 1, 0),
			Quaternion.identity
		);
		
		obj.transform.parent = ShotHolder.transform;

		Rigidbody shotRigidBody = obj.GetComponent<Rigidbody> ();
		shotRigidBody.AddForce (transform.forward * shotSpeed);
	}
    
    public void SetIdle() {
    	animator.SetBool("Run", false);
	}
	
    public void SetWin() {
    	animator.SetBool("Win", true);
	}
	
    public void SetLose() {
    	animator.SetBool("Lose", true);
	}
	
	public void SetPlay() {
		isPlay = true;
	}
	
	public void SetNotPlay() {
		isPlay = false;
	}

}
