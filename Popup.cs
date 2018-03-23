using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace MyMobileProject1 {

public class Popup : MonoBehaviour {
	public RusLesson1 RS;
	private Text Ttime, Tmess, Msg1, Msg2;
	public Image Star1, Star2, Star3;
//	public Transform PopupW;
//	private Transform canv, pw;
	public Transform MyPopupWindow, MyHelpWindow, MyEndSessionWindow, MyPromWindow;
	private Action PopupCloseAction, HelpCloseAction, PromCloseAction;

	private static string YourTime = "Ваше время: ";  // :-(( константа
	private static string YourResult = "Ваш результат: ";  
	private static string Percents = " процентов.";
	private static string Congratulations = "Поздравляем! Вы успешно выполнили все задания!";
	private static string HelpPhrase1 = "Добро пожаловать! Глупенький Двойкин наделал ошибок. Исправь их волшебной ручкой!";
	private static string HelpPhrase2 = "Просто зачеркни неправильную букву пальцем. Попробуй! Желаем удачи!";
	private static string EndPhrase1 = "Прекрасно! Вы справились с заданием! Получите вашу награду!";
	private static string EndPhrase2 = "Собирайте звезды, чтобы повысить свой уровень. У вас всего звезд: ";
	private static string EndPhrase3 = "К сожалению, ваш результат не позволил вам получить ни одной звезды. Попытайтесь еще раз!";
	private static string PromPhrase1 = "Великолепно! Вы достигли нового уровня знаний!";
	private static string PromPhrase2 = "Продолжайте получать достижения и не забудьте поделиться со своими друзьями!";

	// Use this for initialization
	void Start () {
		RS = GameObject.Find ("SceneControl").GetComponent<RusLesson1>();		
	//	canv = GameObject.Find ("Canvas").GetComponent<Transform>();
	}
	
	private void SetPopup () {
	// создать объект из префаба
	//	pw = Instantiate (PopupW).GetComponent<Transform>();
	//	pw.SetParent (canv);
	//	pw.right.Set (0, 0, 0); 
	//	pw.up.Set (0, 0, 0);

	// или просто включить готовое окно :)))
		MyPopupWindow.gameObject.SetActive (true);

		Ttime = GameObject.Find ("TimeLabel").GetComponent<Text>();
		Tmess = GameObject.Find ("MessageLabel").GetComponent<Text>();
	}

	private void SetHelpWindow () {
		MyHelpWindow.gameObject.SetActive (true);

		Msg1 = GameObject.Find ("HelpMsg1").GetComponent<Text>();
		Msg2 = GameObject.Find ("HelpMsg2").GetComponent<Text>();
	}

	private void SetEndWindow (int stars) {
		MyEndSessionWindow.gameObject.SetActive (true);

		Msg1 = GameObject.Find ("EndMsg1").GetComponent<Text>();
		Msg2 = GameObject.Find ("EndMsg2").GetComponent<Text>();

//		Star1 = GameObject.Find ("Star1").GetComponent<Image>();
//		Star2 = GameObject.Find ("Star2").GetComponent<Image>();
//		Star3 = GameObject.Find ("Star3").GetComponent<Image>();
		if (stars == 3) {
			Star1.gameObject.SetActive (true);
			Star2.gameObject.SetActive (true);
			Star3.gameObject.SetActive (true);
			}
		else if (stars == 2) {
			Star1.gameObject.SetActive (true);
			Star2.gameObject.SetActive (true);
			Star3.gameObject.SetActive (false);
			}
		else if (stars == 1) {
			Star1.gameObject.SetActive (false);
			Star2.gameObject.SetActive (true);
			Star3.gameObject.SetActive (false);
			}
		else {
			Star1.gameObject.SetActive (false);
			Star2.gameObject.SetActive (false);
			Star3.gameObject.SetActive (false);
			}
	}

	private void SetPromWindow (string grade) {
		MyPromWindow.gameObject.SetActive (true);

		Msg1 = GameObject.Find ("Promote1").GetComponent<Text>();
		Msg2 = GameObject.Find ("Promote3").GetComponent<Text>();
		Text gr = GameObject.Find ("Promote2").GetComponent<Text>();
		gr.text = grade;
	}

	public void EndOfTurn_Popup (string message, double time) {
		SetPopup ();
	// установить в окне время и очки
		time = Math.Round (time/1000, 2);  // переводим в сек. и округляем
		Ttime.text = YourTime + time.ToString () + " c.";
		Tmess.text = message;

		PopupCloseAction = RS.DrawLesson;
	}

	public void EndOfSession_Popup (int score) {
		SetPopup ();  // тут нужно другое окно, с кнопками заново, выход и т.д.

		Msg1.text = YourResult + score.ToString () + Percents;
		Msg2.text = Congratulations;
	}

	public void HelpWindow (string message1, string message2, Action start) {
		SetHelpWindow ();

		if (message1 != string.Empty)
			Msg1.text = message1;
		else Msg1.text = HelpPhrase1;
		if (message2 != string.Empty)
			Msg2.text = message2;
		else Msg2.text = HelpPhrase2;

		HelpCloseAction = start;
	}

	public void EndSessionWindow (string message1, string message2, int stars, int totals) {
		SetEndWindow (stars);
		
		if (message1 != string.Empty)
			Msg1.text = message1;
		else if (stars < 1)
			Msg1.text = EndPhrase3;
		else Msg1.text = EndPhrase1;
		if (message2 != string.Empty)
			Msg2.text = message2;
		else Msg2.text = EndPhrase2;

		Msg2.text += totals.ToString();
	}

	public void PromotionWindow (string message1, string message2, string grade, Action prom) {
		SetPromWindow (grade);

		if (message1 != string.Empty)
			Msg1.text = message1;
		else Msg1.text = PromPhrase1;
		if (message2 != string.Empty)
			Msg2.text = message2;
		else Msg2.text = PromPhrase2;

		PromCloseAction = prom;
	}

	public void ClosePopup () {
	//	Destroy (pw.gameObject);
		MyPopupWindow.gameObject.SetActive (false);
		if (PopupCloseAction != null)
			PopupCloseAction ();
	}

	public void CloseHelp () {
		MyHelpWindow.gameObject.SetActive (false);
		if (HelpCloseAction != null)
			HelpCloseAction ();
	}

	public void CloseEndWindow (int param) {
		MyEndSessionWindow.gameObject.SetActive (false);
		if (param == 0) {
			// replay
			RS.RestartSession ();
			}
		else if (param == 1) {
			// next
			RS.NextSession ();
			}
		else if (param == 2) {
			// exit
			RS.Quit ();
			}

	}

	public void ClosePromotion (int param) {
		MyPromWindow.gameObject.SetActive (false);
		switch (param) {
		case 1: {
			break;
			}
		case 2: {
			break;
			}
		case 3: {
			break;
			}
		case 4: {
			if (PromCloseAction != null)
				PromCloseAction ();
			break;
			}

		}
	}

}
}
