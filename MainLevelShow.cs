using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;


namespace MyMobileProject1 {

public class MainLevelShow : MonoBehaviour {

	public Text LevelString;
	public Image Premium;
	public Image StarsCount;
	public GameObject SayCloud1;
	public Text SayCloud_text;
	public Text SCtext;
	public StartupManager SM;

	private const string HelloWorld = "Привет!";
	private const string Level = "Твой уровень: ";
	private static string[] reklama = {
		"Рекламу можно отключить, приобретя пакет \"Премиум\" в разделе Покупки.",
		"Рекламу можно отключить, приобретя пакет \"Премиум\" в разделе Покупки.",
		"Рекламу можно отключить, приобретя пакет \"Премиум\" в разделе Покупки.",
		};
	private static string[] helpa = {
		"Если ты не закончил задание и вышел, то сможешь потом продолжить.",
		"Ты можешь повторить задание, если оно тебе понравилось.",
		"Если ты не можешь найти ошибку, воспользуйся подсказкой!",
		"Когда ищешь ошибку, не торопись. Правильный ответ важнее, чем потраченное время!",
		"Непонятный пример можно пропустить, но это ухудшит твой результат. :-(",
		"Задания кажутся слишком лёгкими? Попробуй более сложный уровень!",
		"На сложном уровне тебя ждут случайные примеры - всегда новые!",
		"Если тебе нравится игра, поставь нам оценку в Play Market. :-)",
		"Велик и могуч русский язык! А ты его хорошо знаешь?"
		 };
	private string[] replika;		
	List<int> ReplikaDubl; 
	private bool state;
	private int grade, stars;
	private DateTime TimePoint = DateTime.MinValue, TimeStop;

	// Use this for initialization
	void Start () {
		state = SM.InAppItems.DisableAds; 
		grade = SM.Grade;
		stars = SM.TotalStars;
		LevelString.text = Level + SM.GetGradeString ();
		Premium.gameObject.SetActive (state);
		TimePoint = DateTime.Now;

		// для показа подсказок, с упоминанием рекламы и без него
		// без повторов, но в случайном порядке
		if (!SM.InAppItems.DisableAds && !SM.FirstRun) {
			replika = MergeStringArrays (reklama, helpa);
			}
		else replika = helpa;
		ReplikaDubl = InitDublikat (replika.Length);

		if (SM.SayHello)		
			SayHello (2, 3);
		else
			SaySomething (1, GradesConst.TimeToShowHelp);
	}
	
	// Update is called once per frame
	void Update () {
		Awake ();	

		if (GetTimer (TimePoint) > GradesConst.ReplikaShowTimer) {
			TimePoint = DateTime.Now;
			SaySomething (0, GradesConst.TimeToShowHelp);
			}

		if (SM.EscapeSupported)  // ловим аппаратную кнопку
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				SM.ExitGame (true);
			}

	}

	public void Awake () {
		if (state != SM.InAppItems.DisableAds || grade != SM.Grade) {
			state = SM.InAppItems.DisableAds;
			grade = SM.Grade;
			LevelString.text = Level + SM.GetGradeString ();
			Premium.gameObject.SetActive (state);
		}
		stars = (stars > 999) ? 999 : stars;
		SCtext.text = stars.ToString ();
	}

	// вызывает StartupManager
	public void SayHello (int start_time, int duration) {
		StartCoroutine (SayCloud (HelloWorld, start_time, duration));
	}

	public void SaySomething (int start_time, int duration) {
		int r = RandPhraseCut (replika, ReplikaDubl);
		StartCoroutine (SayCloud (replika[r], start_time, duration));
	}

	private IEnumerator SayCloud (string msg, int t1, int t2) {
		SayCloud1.gameObject.SetActive (false);
		yield return new WaitForSeconds (t1);
		SayCloud1.gameObject.SetActive (true);
		SayCloud_text.text = msg;
		yield return new WaitForSeconds (t2);
		SayCloud1.gameObject.SetActive (false);
	}

	private double GetTimer (DateTime curtime) {
		TimeSpan ts = DateTime.Now - curtime; 
		return ts.TotalMilliseconds;
	}

	// слепить строковыe массивы в один
	private string[] MergeStringArrays (string[] a1, string[] a2) {
		int size = a1.Length + a2.Length;
		string[] aa = new string[size];
		a1.CopyTo (aa, 0);
		a2.CopyTo (aa, a1.Length);
		return aa;
	}

	// случайно выбираем фразу из пула примеров, но так, чтобы использовать все фразы по одному
	// разу, прежде чем зайти на новый круг; для этого создаем "обрезанный" дубликат пула 
	private int RandPhraseCut (string[] L1, List<int> L1b) {
		if (L1b.Count <= 0)
			L1b = InitDublikat (L1.Length);
		int r = UnityEngine.Random.Range (0, L1b.Count);
		int k = L1b [r];
		L1b.RemoveAt(r);
		return k;
	}

	private List<int> InitDublikat (int number) {
		List<int> temp = new List<int>();
		for (int i = 0; i < number; i++)
			temp.Add (i);
		return temp;
	}

}
}
