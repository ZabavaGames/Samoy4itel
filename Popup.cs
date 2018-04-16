using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace MyMobileProject1 {

public class Popup : MonoBehaviour {
	public RusLesson1 RS;
	private Text Ttime, Tmess, Msg1, Msg2, Msg3, Msg4, Msg5, Msg6;
	public Image Star1, Star2, Star3;
//	public Transform PopupW;
//	private Transform canv, pw;
	public Transform MyPopupWindow, MyHelpWindow, MoreHelpWindow, MyEndSessionWindow, MyPromWindow;
	public Button MoreHelpClose, PromClose, HelpClose, EndSessionClose;
	private Action PopupCloseAction, HelpCloseAction, PromCloseAction;

	private static string YourTime = "Ваше время: ";  // :-(( константа
	private static string YourResult = "Ваш результат: ";  
	private static string Percents = " процентов.";
	private static string Congratulations = "Поздравляем! Ты успешно выполнил все задания!";
	private static string HelpPhrase1 = "Добро пожаловать! Глупенький Двойкин наделал ошибок. Исправь их волшебной ручкой!";
	private static string HelpPhrase2 = "Просто зачеркни неправильную букву пальцем. Попробуй! Желаем удачи!";
	private static string EndPhrase1 = "Прекрасно! Ты справился с заданием! Получи награду!";
	private static string EndPhrase2 = "Собирай звезды, чтобы повысить свой уровень. У тебя всего звезд: ";
	private static string EndPhrase3 = "К сожалению, твой результат не позволил тебе получить ни одной звезды. Не расстраивайся! Попытайся еще раз!";
	private static string PromPhrase1 = "Великолепно! Ты достиг нового уровня знаний!";
	private static string PromPhrase2 = "Продолжай получать достижения и не забудь поделиться со своими друзьями!";
	private static string HelpText1 = "Исправление";
	private static string HelpText2 = "Подсказка подсвечивает слова, в которых есть ошибки";
	private static string HelpText3 = "Таймер";
	private static string HelpText4 = "Пропустить пример и перейти к следующему";
	private static string HelpText5 = "Выход в меню";
	private static string HelpText6 = "Твой помощник. Он дает ценные советы!";
	private static string NewLevel = "Получен_новый_уровень!";
	private static string Nazvanie = "Сам_себе_учитель";

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

	private void SetMoreHelpWindow () {
		MoreHelpWindow.gameObject.SetActive (true);

		Msg1 = GameObject.Find ("HelpText1").GetComponent<Text>();
		Msg2 = GameObject.Find ("HelpText2").GetComponent<Text>();
		Msg3 = GameObject.Find ("HelpText3").GetComponent<Text>();
		Msg4 = GameObject.Find ("HelpText4").GetComponent<Text>();
		Msg5 = GameObject.Find ("HelpText5").GetComponent<Text>();
		Msg6 = GameObject.Find ("HelpText6").GetComponent<Text>();
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

	//	PopupCloseAction = RS.DrawLesson;
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

	public void HelpWindow2 () {
		SetMoreHelpWindow ();

		Msg1.text = HelpText1;
		Msg2.text = HelpText2;
		Msg3.text = HelpText3;
		Msg4.text = HelpText4;
		Msg5.text = HelpText5;
		Msg6.text = HelpText6;
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

	public void CloseHelp2 () {
		MoreHelpWindow.gameObject.SetActive (false);
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
		string url = String.Empty,
			newlevel = NewLevel,
			game = Nazvanie,
			pict = "https://lh3.googleusercontent.com/AYj4Gqahus0IFLHDPlhw11OCkToySgLWsgrUVt2CZOSE438xjsIposVO-NjzemfPGqg=w720-h310-rw",
			fblink = "https://www.facebook.com/sharer/sharer.php?u=",
			fbgroup = "https://www.facebook.com/groups/46232190085663",
			fbtitle = "&summary=&title=" + newlevel + "&description=",
			fbpic = "&picture=" + pict,
			twgroup = "https://twitter.com/Samoychitel",
			twlink = "https://twitter.com/intent/tweet?text=" + newlevel + "&url=" + twgroup + "&picture=" + pict, // + "&via=TWITTER-HANDLE",
			ytchannel = "https://www.youtube.com/channel/UCusunBbnocagQukPXgbclVg",
			gplus = "https://plus.google.com/share?url=",
			okgroup = "https://ok.ru/group/55649113276435",
			oklink = "https://www.odnoklassniki.ru/dk?st.cmd=addShare&st.s=1&st._surl=" + okgroup + "&st.comments=" + newlevel,
			vkgroup = "https://vk.com/club164467553",
			vklink = "https://vk.com/share.php?url=" + vkgroup + "&title=" + game + "&description=" + newlevel + "&image=" + pict + "&noparse=true";

		switch (param) {
			case 1: {  // facebook
				url = fblink + fbgroup + fbtitle + fbpic;
			break;
			}
			case 2: {  // twitter
				url = twlink;
			break;
			}
			case 3: {  // google+
				url = gplus + ytchannel;
			break;
			}
			case 4: {  // odnoklassniki
				url = oklink;
			break;
			}
			case 5: {  // vk
				url = vklink;
			break;
			}
			case 0: {  // закрыть
				MyPromWindow.gameObject.SetActive (false);
				if (PromCloseAction != null)
					PromCloseAction ();
			break;
			}
		}
		if (param > 0) {
			Debug.Log (url);
			Application.OpenURL (url);
		}
	}


}
}
