using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using PlayDarts.Com;  
using UnityEngine.UI;  

public class GameStatus : MonoBehaviour {  
	public GameObject canvasItem, roundTextItem, scoreTextItem, TipsTextItem;  

	private int roundNum = 1;  
	private int score = 0;  

	private const float TIPS_TEXT_SHOW_TIME = 0.8f;  

	private GameObject canvas, roundText, scoreText, TipsText;  
	private MainSceneController scene;  

	void Start () {  
		scene = MainSceneController.getInstance();  
		scene.setGameStatus(this);  

		canvas = Instantiate(canvasItem);  
		roundText = Instantiate(roundTextItem, canvas.transform);  
		roundText.transform.Translate(canvas.transform.position);  
		roundText.GetComponent<Text>().text = "Round: " + roundNum;  

		scoreText = Instantiate(scoreTextItem, canvas.transform);  
		scoreText.transform.Translate(canvas.transform.position);  
		scoreText.GetComponent<Text>().text = "Score:  " + score + " / " + (roundNum * 100);  

		TipsText = Instantiate(TipsTextItem, canvas.transform);  
		TipsText.transform.Translate(canvas.transform.position);  
		showTipsText();  
	}  

	void Update () {  

	}  

	public int getRoundNum() {  
		return roundNum;  
	}  

	void addRoundNum() {  
		roundNum++;  
		roundText.GetComponent<Text>().text = "Round: " + roundNum;  
	}  

	public int getScore() {  
		return score;  
	}  

	 
	public void addScore() {  
		score += 100;  
		scoreText.GetComponent<Text>().text = "Score:  " + score + " / " + (roundNum * 100);  
		checkScore();  
	}  

	
	public void subScore() {  
		score = score >= 100 ? score - 100 : 0;  
		scoreText.GetComponent<Text>().text = "Score:  " + score + " / " + (roundNum * 100);  
	}  

	
	void checkScore() {  
		if (score >= roundNum * 100) {  
			addRoundNum();  
			resetScore();  

			showTipsText();  
		}  
	}  
	void resetScore() {  
		score = 0;  
		scoreText.GetComponent<Text>().text = "Score:  " + score + " / " + (roundNum * 100);  
	}  
	void showTipsText() {  
		TipsText.GetComponent<Text>().text = "Round " + roundNum + " !";  
		TipsText.SetActive(true);  
		StartCoroutine(waitForSomeAndDisappearTipsText());  
	}  
	IEnumerator waitForSomeAndDisappearTipsText() {  
		yield return new WaitForSeconds(TIPS_TEXT_SHOW_TIME);  

		TipsText.SetActive(false);  
	}  
}  

