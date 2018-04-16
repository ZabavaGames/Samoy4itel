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

	public bool FirstRun, SayHello, SetScore;
	public int TotalStars;
	public int Grade;
	public bool EscapeSupported;

	public struct Purchase {
		public bool DisableAds;
	}
	public Purchase InAppItems = new Purchase ();

	public GiftsShow SaleWindow;
	public MainLevelShow MenuWindow;
	public GameObject Otzenka;

	private Action StartScene;

	private const string PlayMarketUrl = "https://play.google.com/store/apps/details?id=com.ZabavaGames.Samoy4itel";
	private const string PlayMarketLink = "market://details?id=com.ZabavaGames.Samoy4itel";
//	private const string PlayMarketLink = "market://details?id=" + Application.productName;

	// Use this for initialization
	void Start () {	
		TotalStars = 0;
   		Grade = GradesConst.MinGrade;
		InAppItems.DisableAds = false; 

		if (Application.platform == RuntimePlatform.Android || 
			Application.platform == RuntimePlatform.WindowsPlayer ||
			Application.platform == RuntimePlatform.WindowsEditor)
			EscapeSupported = true;
		else EscapeSupported = false;

		// читаем настройки данные пользователя
		// если первый запуск, то инициализируем их
		FirstRun = true;	
		if (!PlayerPrefs.HasKey (GradesConst.firstrun)) {
			ResetPrefs ();
			}
		else {
			string fs = PlayerPrefs.GetString (GradesConst.firstrun);
			if (string.Compare (fs, GradesConst.done) == 0) 
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

		// проверяем, ставить ли оценку
		string sc = PlayerPrefs.GetString (GradesConst.score);
		if (String.Compare (sc, GradesConst.fivestars) != 0)
			SetScore = true;
		else
			SetScore = false;
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
		if (EscapeSupported)
			if (Input.GetKeyDown (KeyCode.Escape))  // ловим аппаратную кнопку
			{
			// сделать подтверждение выхода?..
				if (MenuWindow.isActiveAndEnabled) {
//						ExitGame ();
					}
			}
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
//		if (!InAppItems.DisableAds && !FirstRun && Grade > GradesConst.MinGrade)
//			ShowSkippableVideo ();
//////////////////////////////
//  РЕКЛАМУ ВРЕМЕННО УБРАЛ!!!!
//////////////////////////////
//		else
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

	private void ShowPromoScreen_OnExit () {
		ShowOptions options = new ShowOptions ();
		options.resultCallback = HandleShowResults3;
		Advertisement.Show(options);	
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

	private void HandleShowResults3 (ShowResult result) {
		ExitGame (false);
	}

	public void ExitGame (bool withPromo) {
////////////////////////////////
// ResetPrefs ();  // в тестовых целях!!!
//////////////////////////////////
		SavePrefs ();
		if (!FirstRun && SetScore && Grade > GradesConst.MinGrade + 1) {
			ShowScoreWindow (true);
			}
		else {
			ApplicationManager Ap = GameObject.Find ("ApplicationManager").GetComponent<ApplicationManager>();
			if (withPromo)
				ShowPromoScreen_OnExit ();
			Ap.Quit ();
		//	Application.Quit ();
			}
	}

	public void ShowScoreWindow (bool state) {
		if (state) {
			// show 5 stars window
			// убедиться, чтобы окно показывал только 1 раз
			MenuWindow.gameObject.SetActive (false);
			SetScore = false;
			Otzenka.gameObject.SetActive (true);
			}
		else {
			Otzenka.gameObject.SetActive (false);
			MenuWindow.gameObject.SetActive (true);
			}
	}

	// перейти в плеймаркет, чтобы поставить оценку
	public void GoToMarket () {
		PlayerPrefs.SetString (GradesConst.score, GradesConst.fivestars);
	//	Application.OpenURL (PlayMarketUrl);
		Application.OpenURL (PlayMarketLink);
		ExitGame (false);
	}

	// отложить
	public void NotNow () {
		PlayerPrefs.SetString (GradesConst.score, GradesConst.undecised);
		ExitGame (true);
	}

	// больше не поазывать
	public void ForgetAboutIt () {
		PlayerPrefs.SetString (GradesConst.score, GradesConst.fivestars);
		ExitGame (true);
	}

	public void SendEmail ()
 	{
  	string email = "zabava.games.studio@gmail.com";
  	string subject = WWW.EscapeURL("About application Sam_sebe_uchitel").Replace("+","%20");
  	Application.OpenURL("mailto:" + email + "?subject=" + subject);
 	}
 

	public void ShowDebugInfo () {
		GameObject o = GameObject.Find ("SystemText");
		if (o == null) return;
		Text t = o.GetComponent<Text>();
//		string s = Application.buildGUID;
		string ss = Application.productName;
		string vcode = Application.version;
//		t.text = "\n" + "\nVersion: " + vcode + ".026" + "\nFirstRun: " + FirstRun;
//			+ "\nstars = " + TotalStars + "\ngrade = " + Grade + "\nads disabled = " + InAppItems.DisableAds;
	}

	//  это была тестовая фигня
	public void ShowPurchaseInfo (string info) {
/*		GameObject o = GameObject.Find ("PurchaseText");
		if (info != null && o != null) {
			Text t = o.GetComponent<Text>();
			t.text += info + "\n";
		}
*/	}

	public string GetGradeString () {
		return GradesConst.GradeStringsRus[Grade];
	}

	public string GetNextGradeString () {
		if (Grade <= GradesConst.MaxGrade)
			return GradesConst.GradeStringsRus[Grade+1];
		else 
			return String.Empty;
	}

	public int GetNextLevelStars () {
		if (Grade < GradesConst.MaxGrade)
			return GradesConst.StarsToPromote[Grade+1] - TotalStars;
		else return 0;
	}


  }
}
