using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace MyMobileProject1 {

public class RankShow : MonoBehaviour {

	public StartupManager SM;

	public Button Light, Medium, Hard, Advanced, Back;
	public Image SayCloud2;

	private static string Helpach0 = "Выбери уровень сложности. Начни с легкого!";
	private static string Helpach1 = "Ты открыл более сложный уровень. Попробуй сыграть на нём!";
	private static string Helpach2 = "Получи еще несколько звёзд, чтобы открыть самый сложный уровень!";
	private static string Helpach3 = "Выбери уровень сложности. Какой тебе нравится?";

	// Use this for initialization
	void Start () {
		SayCloud2.gameObject.SetActive (false);
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SM.EscapeSupported)  // ловим аппаратную кнопку
			if (Input.GetKeyDown (KeyCode.Escape)) 
				Back.onClick.Invoke ();	
	}

	void Awake () {
		bool[] matrix;
		if (SM.Grade < GradesConst.RankGradeReq[(int)ranks.light]) {
			matrix = new bool[] {false, true, true, true};
			StartCoroutine (SayHello (Helpach0));
			}
		else if (SM.Grade < GradesConst.RankGradeReq[(int)ranks.medium]) {
			matrix = new bool[] {false, false, true, true};
			StartCoroutine (SayHello (Helpach1));
			}
		else if (SM.Grade < GradesConst.RankGradeReq[(int)ranks.advanced]) {
			matrix = new bool[] {false, false, false, true};
			StartCoroutine (SayHello (Helpach2));
			}
		else {
			matrix = new bool[] {false, false, false, false};
			StartCoroutine (SayHello (Helpach3));
			}

		RestrictAll (matrix);
	}

	private void RestrictLight (bool state) {
		Light.interactable = !state;
		Light.GetComponentsInChildren<Text>()[0].color = (state) ? Color.gray : Color.white;
	}

	private void RestrictMedium (bool state) {
		Medium.interactable = !state;
		Medium.GetComponentsInChildren<Text>()[0].color = (state) ? Color.gray : Color.white;
	}

	private void RestrictHard (bool state) {
		Hard.interactable = !state;
		Hard.GetComponentsInChildren<Text>()[0].color = (state) ? Color.gray : Color.white;
	}

	private void RestrictAdvanced (bool state) {
		Advanced.interactable = !state;
		Advanced.GetComponentsInChildren<Text>()[0].color = (state) ? Color.gray : Color.white;
	}

	private void RestrictAll (bool[] matrix) {
			RestrictLight (matrix[0]);
			RestrictMedium (matrix[1]);
			RestrictAdvanced (matrix[2]);
			RestrictHard (matrix[3]);
	
	}

	private IEnumerator SayHello (string s) {
		SayCloud2.gameObject.SetActive (true);
		SayCloud2.GetComponentInChildren<Text>().text = s;
		yield return new WaitForSeconds (15f);
		SayCloud2.gameObject.SetActive (false);
	}


}
}
