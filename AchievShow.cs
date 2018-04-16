using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class AchievShow : MonoBehaviour {

	public Text LevelString, Stars, StarsLeft, StarsCount, NextLevelString, ViewAdsText, AdsInfoText;
	public StartupManager SM;
	public Button ViewAds;

	private static string Level = "Твой уровень: ";
	private static string NextLevel = "Следующий уровень: ";
	private static string Star_S = "Собрано звезд: ";
	private static string Stars_L = "Осталось собрать звезд: ";
	private static string AdsInfo = "Ты можешь посмотреть рекламный ролик, чтобы получить звезду!";
	private static string AdsText = "Смотреть видео";

	// Use this for initialization
	void Start () {
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake () {
		LevelString.text = Level + SM.GetGradeString ();
		Stars.text = Star_S;
		StarsCount.text = SM.TotalStars.ToString ();
		NextLevelString.text = NextLevel + SM.GetNextGradeString ();
		StarsLeft.text = Stars_L + (SM.GetNextLevelStars());
		ViewAdsText.text = AdsText;
		AdsInfoText.text = AdsInfo;
	}

}
}
