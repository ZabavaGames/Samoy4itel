using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class AchievShow : MonoBehaviour {

	public Text Title, LevelString, Stars, StarsLeft, StarsCount, NextLevelString, ViewAdsText, AdsInfoText, Close;
	public StartupManager SM;
	public Button ViewAds;

	// Use this for initialization
	void Start () {
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake () {
		Title.text = GradesConst.Trophy[SM.Language];
		LevelString.text = GradesConst.LevelString[SM.Language] + SM.GetGradeString ();
		Stars.text = GradesConst.Star_S[SM.Language];
		StarsCount.text = SM.TotalStars.ToString ();
		NextLevelString.text = GradesConst.NextLevel[SM.Language] + SM.GetNextGradeString ();
		StarsLeft.text = GradesConst.Stars_L[SM.Language] + (SM.GetNextLevelStars());
		ViewAdsText.text = GradesConst.AdsText[SM.Language];
		AdsInfoText.text = GradesConst.AdsInfo[SM.Language];
		Close.text = GradesConst.Close[SM.Language];
	}

}
}
