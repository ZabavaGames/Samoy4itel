using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms.Impl;
using System.Security;
using UnityEngine.UI;
using System;

namespace MyMobileProject1 {

	public class StartupManager : MonoBehaviour {

	public bool FirstRun, SayHello;
	public int TotalStars;
	public int Grade;

	public struct Purchase {
		public bool DisableAds;
	}
	public Purchase InAppItems = new Purchase ();

	public GiftsShow SaleWindow;
	public MainLevelShow MenuWindow;

	private Action StartScene;


	// Use this for initialization
	void Start () {	

		FirstRun = true;	
		TotalStars = 0;
   		Grade = GradesConst.MinGrade;
		InAppItems.DisableAds = false; 

		// читаем настройки данные пользователя
		// если первый запуск, то инициализируем их
		if (!PlayerPrefs.HasKey (GradesConst.firstrun)) {
			ResetPrefs ();
			}
		else {
			string s = PlayerPrefs.GetString (GradesConst.firstrun);
			if (string.Compare (s, GradesConst.done) == 0) 
				FirstRun = false;	
	    // выставить свой уровень и достижения
			LoadPrefs ();
		//  проверяем, отключена ли у нас реклама
			CheckAds ();
			}	

		// проверяем, заходил ли сегодня юзер, чтобы поздороваться
		string Today = DateTime.Now.ToShortDateString ();
		string Yesterday = PlayerPrefs.GetString (GradesConst.date);
		if (String.Compare (Today, Yesterday) != 0 || FirstRun) 
			SayHello = true;
		else SayHello = false;
		PlayerPrefs.SetString (GradesConst.date, Today);
	}

	private void ResetPrefs () {
		PlayerPrefs.SetString (GradesConst.firstrun, GradesConst.started);
		PlayerPrefs.SetInt (GradesConst.stars, 0);
		PlayerPrefs.SetInt (GradesConst.grade, GradesConst.MinGrade);
		PlayerPrefs.SetString (GradesConst.ads, GradesConst.enabled); // сбросить
		ClearAllSaves ();		
	}

	private void ClearAllSaves () {
		for (int r = (int)ranks.zero; r <= (int)ranks.hard; r++)
			PlayerPrefs.SetInt (r.ToString () + GradesConst.saved_session, 0);
	}

	private void SavePrefs () {
		PlayerPrefs.SetInt (GradesConst.stars, TotalStars);
		PlayerPrefs.SetInt (GradesConst.grade, Grade);
	}

	private void LoadPrefs () {
		TotalStars = PlayerPrefs.GetInt (GradesConst.stars);
		Grade = PlayerPrefs.GetInt (GradesConst.grade);
	}

	public void CheckAds () {
		bool state = false;
		if (PlayerPrefs.HasKey (GradesConst.ads)) {
			string s1 = PlayerPrefs.GetString (GradesConst.ads);
			if (string.Compare (s1, GradesConst.disabled) == 0)
				state = true;
			}
		InAppItems.DisableAds = state;  // возвращаем тру, если реклама отключена
	//	SetADText_and_Button (state);
		
		ShowDebugInfo ();
	}

	// при покупке (отмене) задействуем эту ф-ию, прописываем флаг и проверяем активность кнопки покупки дизейбла
	public void DisableAds (bool state) {
Debug.Log ("Работает ф-ия DisableAds. Значения state = " + state);
		InAppItems.DisableAds = state;
	//	SetADText_and_Button (state);
		PlayerPrefs.SetString (GradesConst.ads, (state) ? GradesConst.disabled : GradesConst.enabled);
		SceneManager.LoadScene (GradesConst.Scene0);
	}

	// при покупке разблокировки уоровня
	public void LvUnlock (bool state) {
		LoadPrefs ();
	//	int i = 0, newgrade = Grade;
		int	i = GradesConst.StarsToPromote [Grade+1];
	//	while (TotalStars > i)
	//		newgrade ++;
		if (TotalStars < i)
			TotalStars = i;
		Grade ++;

		SavePrefs ();
		SceneManager.LoadScene (GradesConst.Scene0);
	}

	private void SetADText_and_Button (bool state) {
/*		bool btn, txt;
		Text t = null;
		if (BuyADButton == null && (BuyADButton = GameObject.Find ("BuyADButton")) == null) 
			btn = false;
			else btn = true;
		if (BuyADText == null && (BuyADText = GameObject.Find ("BuyADText")) == null) 
			txt = false;
			else txt = true;
//	Debug.Log ("ща посмотрим " + state + " txt " + txt + " button " + btn);
		
		if (txt)
			t = BuyADText.GetComponent<Text>();
//		bool button = BuyADButton.activeInHierarchy;
		if (state && btn) {
			BuyADButton.SetActive (false);
	Debug.Log ("Отключаем кнопку");
			if (txt) t.text = ADPurchased;
			}
		else if (!state && btn) {
			BuyADButton.SetActive (true);
	Debug.Log ("Включаем кнопку");
			if (txt) t.text = ADnotPurchased;
			}
		else if (!btn)
			if (txt) t.text = ADPurchased;
*/
		if (SaleWindow != null) 
			SaleWindow.Awake ();
		if (MenuWindow != null)
			MenuWindow.Awake ();

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void StartRusLesson (int flag) {
		StartLesson (flag);
	}

	private void StartLesson (int flag) {
	//	string name = (flag == 1) ? GradesConst.Scene1: GradesConst.Scene2;
		string name = GradesConst.Scene1;
	//	PlayerPrefs.SetInt (GradesConst.scene, flag);
		if (FirstRun || Grade == GradesConst.MinGrade) 
			flag = 0;
		PlayerPrefs.SetInt (GradesConst.rank, flag);
	
		StartScene = () => SceneManager.LoadScene (name);
	// показать рекламу, если не отключена (при первом зап. не показывать
		if (!InAppItems.DisableAds && !FirstRun)
			ShowSkippableVideo ();
		else
			StartScene ();
	}

	public void ShowVideoButton () {
		StartScene = () => SceneManager.LoadScene (GradesConst.Scene0);
		ShowRewardedVideo ();
	}

	private void ShowSkippableVideo () {
		ShowOptions options = new ShowOptions ();
		options.resultCallback = HandleShowResults1;
		Advertisement.Show(options);	
	}

	private void ShowRewardedVideo () {
		ShowOptions options = new ShowOptions ();
		options.resultCallback = HandleShowResults2;
		Advertisement.Show("rewardedVideo", options);	
	}
	
	private void HandleShowResults1 (ShowResult result) {
		if (result == ShowResult.Finished) {
			Debug.Log ("Video completed - offer reward");
			}
		else if (result == ShowResult.Skipped) {
			Debug.Log ("Video skipped");
			}
		else if (result == ShowResult.Failed) {
			Debug.Log ("Video failed");
		}

		if (StartScene != null) 
			StartScene ();
	}

	private void HandleShowResults2 (ShowResult result) {
		if (result == ShowResult.Finished) {
			Debug.Log ("Video completed - offer reward");
			TotalStars ++;
			if (Grade < GradesConst.MaxGrade && TotalStars >= GradesConst.StarsToPromote[Grade+1])  
				Grade ++;
	//		Grade = PlayerPromotion (TotalStars, Grade);
			SavePrefs ();
			}
		else if (result == ShowResult.Skipped) {
			Debug.Log ("Video skipped");
			}
		else if (result == ShowResult.Failed) {
			Debug.Log ("Video failed");
		}

		if (StartScene != null) 
			StartScene ();
	}


	public void ExitGame () {
////////////////////////////////
// ResetPrefs ();  // в тестовых целях!!!
//////////////////////////////////

		ApplicationManager ap = GameObject.Find ("ApplicationManager").GetComponent<ApplicationManager>();
		ap.Quit ();
		//	Application.Quit ();
	}

	public void ShowDebugInfo () {
		GameObject o = GameObject.Find ("SystemText");
		if (o == null) return;
		Text t = o.GetComponent<Text>();
//		string s = Application.buildGUID;
		string ss = Application.productName;
		string vcode = Application.version;
		t.text = "\n" + "\nVersion: " + vcode + "\nBundle: 22" + "\nFirstRun: " + FirstRun;
//			+ "\nstars = " + TotalStars + "\ngrade = " + Grade + "\nads disabled = " + InAppItems.DisableAds;
	}

	public void ShowPurchaseInfo (string info) {
		GameObject o = GameObject.Find ("PurchaseText");
		if (info != null && o != null) {
			Text t = o.GetComponent<Text>();
			t.text += info + "\n";
		}
	}

	public string GetGradeString () {
		return GradesConst.GradeStringsRus[Grade];
	}

	public string GetNextGradeString () {
//		if (Grade+1 < GradesConst.GradeStringsRus.Length)
		if (Grade <= GradesConst.MaxGrade)
			return GradesConst.GradeStringsRus[Grade+1];
		else 
			return "";
	}

	public int GetNextLevelStars () {
		if (Grade < GradesConst.MaxGrade)
			return GradesConst.StarsToPromote[Grade+1] - TotalStars;
		else return 0;
	}


  }
}
