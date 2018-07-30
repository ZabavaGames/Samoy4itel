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

	public bool FirstRun, SayHello, SetScore, ShowPromo;
	public int TotalStars, Grade, Language, LessonLanguage;
	public bool EscapeSupported;
	public Text ScoreText, OptionYes, OptionNo, OptionLater;

	public struct Purchase {
		public bool DisableAds;
	}
	public Purchase InAppItems = new Purchase ();

	public GiftsShow SaleWindow;
	public MainLevelShow MenuWindow;
	public GameObject Otzenka, LangWindow;

	private Action StartScene;


	// Use this for initialization
	void Start () {	
		TotalStars = 0;
   		Grade = GradesConst.MinGrade;
		InAppItems.DisableAds = false; 

		// задействуем кнопку назад или клавишу escape
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
			}

            // выбираем тип урока - русский или английский; тоже самое в руслессон
            if (Application.identifier == GradesConst.ApplicationIdRus)
                LessonLanguage = (int)languages.russian;
            else if (Application.identifier == GradesConst.ApplicationIdEng)
                LessonLanguage = (int)languages.english;
            else
                ExitGame(false);

            // выставляем язык интерфейса
            if (!PlayerPrefs.HasKey (GradesConst.lang)) {
            Debug.Log ("System language is: " + Application.systemLanguage);
			if (Application.systemLanguage == SystemLanguage.Russian)
				Language = (int)languages.russian;
			else if (Application.systemLanguage == SystemLanguage.English)
				Language = (int)languages.english;
			else ChooseLanguageDialog (true);
			PlayerPrefs.SetString (GradesConst.lang, GradesConst.langs [Language]);
			}
		else {
			string s = PlayerPrefs.GetString (GradesConst.lang);
			int i = Array.IndexOf (GradesConst.langs, s);
			Language = (int)languages.russian + i;
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

        //  проверяем, отключена ли у нас реклама
        CheckAds();
        }

        private void ResetPrefs () {
            Debug.Log("эта фигня работает?");
        PlayerPrefs.SetString (GradesConst.firstrun, GradesConst.started);
		PlayerPrefs.SetInt (GradesConst.stars, 0);
		PlayerPrefs.SetInt (GradesConst.grade, GradesConst.MinGrade);
		PlayerPrefs.SetString (GradesConst.ads, GradesConst.enabled); // сбросить
		PlayerPrefs.DeleteKey (GradesConst.lang);
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

        if (!InAppItems.DisableAds && !FirstRun && Grade > GradesConst.MinGrade && SayHello)
                ShowPromo = true;
   //     ShowDebugInfo ();
	}

	// при покупке (отмене) задействуем эту ф-ию, прописываем флаг и проверяем активность кнопки покупки дизейбла
	public void DisableAds (bool state) {
Debug.Log ("Работает ф-ия DisableAds. Значения state = " + state);
		InAppItems.DisableAds = state;
	//	SetADText_and_Button (state);
		PlayerPrefs.SetString (GradesConst.ads, (state) ? GradesConst.disabled : GradesConst.enabled);
		Reload ();
	}

	// при покупке разблокировки уровня
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
		Reload ();
	}

/*	private void SetADText_and_Button (bool state) {
		bool btn, txt;
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

		if (SaleWindow != null) 
			SaleWindow.Awake ();
		if (MenuWindow != null)
			MenuWindow.Awake ();
	} */

	public void Reload () {
		SceneManager.LoadScene (GradesConst.Scene0);
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
            string name = string.Empty;
            if (LessonLanguage == (int)languages.russian)
                name = GradesConst.Scene1;
            else if (LessonLanguage == (int)languages.english)
                name = GradesConst.Scene2;
            StartLesson (flag, name);
	}

        public void StartEngLesson(int flag)
        {
            string name = string.Empty;
            if (LessonLanguage == (int)languages.russian)
                name = GradesConst.Scene1;
            else if (LessonLanguage == (int)languages.english)
                name = GradesConst.Scene2;
            StartLesson(flag, name);
        }

        private void StartLesson (int flag, string name) {
	    //	PlayerPrefs.SetInt (GradesConst.scene, flag);
		    if (FirstRun || Grade == GradesConst.MinGrade) 
			    flag = 0;
		    PlayerPrefs.SetInt (GradesConst.rank, flag);
	
		    StartScene = () => SceneManager.LoadScene (name);
 
            // показать рекламу, если не отключена (при первом зап. не показывать)
            if (ShowPromo)
    			ShowSkippableVideo ();
	    	else
		    	StartScene ();
	}

	public void ShowVideoButton () {
		StartScene = () => Reload ();
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
        // предложить поставить оценку
		if (!FirstRun && SetScore && Grade > GradesConst.MinGrade + 1) {
			ShowScoreWindow (true);
			}
		else {
            // показать рекламу на выходе
            if (withPromo && ShowPromo)
                    ShowPromoScreen_OnExit();
            ApplicationManager Ap = GameObject.Find ("ApplicationManager").GetComponent<ApplicationManager>();
			if (Ap != null) Ap.Quit ();
		    else	Application.Quit ();
			}
	}

	public void ShowScoreWindow (bool state) {
		if (state) {
			// show 5 stars window
			// убедиться, чтобы окно показывал только 1 раз
			MenuWindow.gameObject.SetActive (false);
			SetScore = false;
			Otzenka.gameObject.SetActive (true);
			ScoreText.text = GradesConst.ScoreText[Language];
			OptionYes.text = GradesConst.OptionYes[Language];
			OptionNo.text = GradesConst.OptionNo[Language];
			OptionLater.text = GradesConst.OptionLater[Language];
			}
		else {
			Otzenka.gameObject.SetActive (false);
			MenuWindow.gameObject.SetActive (true);
			}
	}

	// перейти в плеймаркет, чтобы поставить оценку
	public void GoToMarket () {
		PlayerPrefs.SetString (GradesConst.score, GradesConst.fivestars);
            // Application.OpenURL (GradesConst.PlayMarketUrl[LessonLanguage]);
            Application.OpenURL (GradesConst.PlayMarketLink[LessonLanguage]);
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
  		string email = GradesConst.MailTo;
  		string subject = WWW.EscapeURL(GradesConst.MailSubject[Language]).Replace("+","%20");
  		Application.OpenURL("mailto:" + email + "?subject=" + subject);
 	}
 

	public void ShowDebugInfo () {
		GameObject o = GameObject.Find ("SystemText");
		if (o == null) return;
		Text t = o.GetComponent<Text>();
//		string s = Application.buildGUID;
		string ss = Application.productName;
		string vcode = Application.version;
 // это было для теста
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
		return GradesConst.GradeStrings[Language][Grade];
	}

	public string GetNextGradeString () {
		if (Grade <= GradesConst.MaxGrade)
			return GradesConst.GradeStrings[Language][Grade+1];
		else 
			return String.Empty;
	}

	public int GetNextLevelStars () {
		if (Grade < GradesConst.MaxGrade)
			return GradesConst.StarsToPromote[Grade+1] - TotalStars;
		else return 0;
	}

	public void SetLanguage (int lang) {
		Language = lang;
		PlayerPrefs.SetString (GradesConst.lang, GradesConst.langs [Language]);
		ChooseLanguageDialog (false);
		Reload ();
	}

	public void ChooseLanguageDialog (bool state) {
		if (state) {
			MenuWindow.gameObject.SetActive (false);
			LangWindow.gameObject.SetActive (true);
			}
		else {
			LangWindow.gameObject.SetActive (false);
			MenuWindow.gameObject.SetActive (true);
			}
	}


  }
}
