using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class myLeaderBoard : MonoBehaviour
{
	dreamloLeaderBoard dl;
	//dreamloPromoCode pc;
	public RectTransform rectTransform;
	public GameObject obj;
	public List<GameObject> scoreObj= new List<GameObject>();
	private string playerName, totalScore;

	public int maxToDisplay = 10;

	void Start()
	{
		// get the reference here...
		this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

		// get the other reference here
		//this.pc = dreamloPromoCode.GetSceneDreamloPromoCode();
	}


	//private void Update()
	//{
	//	if (Input.GetKeyDown(KeyCode.G)) 
	//	{

	//		foreach (GameObject g in scoreObj)

	//		{ 
	//			Destroy(g);


	//		}
	//		scoreObj.Clear();
	//	}
	//}
	public void SaveLoadScore()
	{
		if (dl.publicCode == "") Debug.LogError("You forgot to set the publicCode variable");
		if (dl.privateCode == "") Debug.LogError("You forgot to set the privateCode variable");

		dl.AddScore(playerName, int.Parse(totalScore));


	}
	public void LoadScore()
	{
		foreach (GameObject g in scoreObj)
		{
			Destroy(g);
		}
		scoreObj.Clear();
		StartCoroutine(ScoreCoroutine());

	}

	IEnumerator ScoreCoroutine() 
	{
		dl.GetScores();
		yield return new WaitForSeconds(2f);
		List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();
		int count = 0;
		//print(scoreList.Count);
		foreach (dreamloLeaderBoard.Score currentScore in scoreList)
		{
			count++;
			print(currentScore.playerName + ":" + currentScore.score);
			GameObject o = Instantiate(obj, rectTransform);
			Text[] text = o.GetComponentsInChildren<Text>();
			text[0].text = currentScore.playerName;
			text[1].text = currentScore.score.ToString();
			scoreObj.Add(o);
			if (count >= maxToDisplay) break;
		}
		yield return null;

	}
}
