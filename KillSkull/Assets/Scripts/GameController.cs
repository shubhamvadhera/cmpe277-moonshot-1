﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public bool isGameOver;
	public Text scoreTxt;
	public Text gameOverTxt;
	public Canvas gameOverCanvas;
    public Canvas VRGameOverCanvas;
    public Text VRGameOverTxt;
    public Text VRScoreTxt;

    private int _currScore;
	private int _scoreToWin = 20;
	private bool _didIWin;

	/// <summary>
	/// Start a new game.
	/// </summary>
	void NewGame() {
		ResetGame();
	}
	

	/// <summary>
	/// Got an enemy! Increment the score and see if we win.
	/// </summary>
	public void GotOne() {
		_currScore++;
		scoreTxt.text =  "" + _currScore;
        VRScoreTxt.text = "" + _currScore;
        if (_currScore >= _scoreToWin) {
			GameOver(true);
		}
	}
	
	/// <summary>
	/// Game's over. 
	/// </summary>
	/// <param name="didIWin">Did the playeer win?</param>	
	public void GameOver(bool didIWin) {
		isGameOver = true;
		_didIWin = didIWin;
        string finalTxt = (_didIWin) ? "You won!" : "You Lost!";
        //gameOverTxt.text = (_didIWin) ? "You won!" : "Too bad";
        if (Cardboard.SDK.VRModeEnabled)
        {
            VRGameOverCanvas.enabled = true;
            VRGameOverTxt.text = finalTxt;
        }
        else {
            gameOverCanvas.enabled = true;
            gameOverTxt.text = finalTxt;
        }
        //gameOverCanvas.enabled = true;
    }
	

	/// <summary>
	/// Resets the interface, removes remaining game objects. Basically gets us to a point
	/// where we're ready to play again.
	/// </summary>
	public void ResetGame() {
        // Reset the interface
        VRGameOverCanvas.enabled = false;
        gameOverCanvas.enabled = false;
		isGameOver = false;
		_currScore = 0;
		scoreTxt.text = "--";
        VRScoreTxt.text = "--";
        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
        	Destroy(enemy);
        }
        
        GameObject[] ninjaStars = GameObject.FindGameObjectsWithTag("NinjaStar");
        foreach (GameObject ninjaStar in ninjaStars) {
        	Destroy (ninjaStar);
        }

    }
	
	void Start () {
		NewGame();
	}

    public void QuitGame() {
        Application.Quit();
    }
	
}
