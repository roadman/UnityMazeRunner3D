using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public enum GameState {
	WaitGenerate,
	PreReady,
	Ready,
	PrePlay,
	Play,
	PreGoal,
	Goal,
	PreGameOver,
	GameOver,
}
	
public class GameController : MonoBehaviour {

	private int score = 0;
	private int goalDistance;
	private double timesec;
	private GameObject goal;
	private GameState state;
	
	public GameObject Player;
	public int StartTimeSec;
	public Text Score;
	public Text GameTime;
	public Text GoalDistance;
	public Text Notice;
	
	public void SetReady() {
		state = GameState.PreReady;
	}
	
	public void SetGoal() {
		state = GameState.PreGoal;
	}
	
	public void DecrimentTimesec() {
		timesec -= 1;
	}
	
	// Use this for initialization
	void Start () {
		goalDistance = 0;
		timesec = StartTimeSec;
		state = GameState.WaitGenerate;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		switch(state) {
			case GameState.WaitGenerate:
				break;
			case GameState.PreReady:
				goal = GameObject.FindGameObjectWithTag("Goal");
				
				UpdateUI();
				UpdateNoticeText("Click Start!");
				state = GameState.Ready;
				Player.SendMessage("SetNotPlay");
				break;
			case GameState.Ready:
				if(Input.GetButtonDown("Fire1")) {
					state = GameState.PrePlay;
				}
				break;
			case GameState.PrePlay:
				UpdateNoticeText("");
				Player.SendMessage("SetPlay");
				state = GameState.Play;
				break;
			case GameState.Play:
				timesec -= Time.deltaTime;
				UpdateUI();
				if((int)timesec == 0) {
					state = GameState.PreGameOver;
				}
				break;
			case GameState.PreGoal:
				score += (int)timesec;
				UpdateUI();
				UpdateNoticeText("Goal!");
				state = GameState.Goal;
				Player.SendMessage("SetNotPlay");
				break;
			case GameState.Goal:
				if(Input.GetButtonDown("Fire1")) {
					NextStage();
				}
				break;
			case GameState.PreGameOver:
				UpdateNoticeText("GameOver!");
				state = GameState.GameOver;
				Player.SendMessage("SetNotPlay");
				Player.SendMessage("SetLose");
				break;
			case GameState.GameOver:
				if(Input.GetButtonDown("Fire1")) {
					StageReload();
				}
				break;
		}	
	}
	
	private void UpdateUI() {
		UpdateScoreText(score);
		
		UpdateTimeText((int)timesec);
		
		int dis = (int)Vector3.Distance(Player.transform.position, goal.transform.position);
		UpdateGoalDistanceText(dis);
	}
	
	private void NextStage() {
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void StageReload() {
		score = 0;
		Application.LoadLevel(Application.loadedLevel);
	}
	
	private void UpdateScoreText(int score) {
		//Score.text = "Score: " + score;
		Score.text = "";
	}
	
	private void UpdateTimeText(int timesec) {
		GameTime.text = "Time: " + timesec + "sec";
	}
	
	private void UpdateGoalDistanceText(int distance) {
		GoalDistance.text = "Goal: " + distance;
	}
	
	private void UpdateNoticeText(string text) {
		Notice.text = text;
	}
}
