using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class RankShow : MonoBehaviour {

	public StartupManager SM;

	public Button Light, Medium, Hard, Advanced;
	public Image SayCloud2;

	private static string Helpach0 = "Выберите уровень сложности. Начните с легкого!";
	private static string Helpach1 = "Вы открыли более сложный уровень. Попробуйте сыграть на нем!";
	private static string Helpach2 = "Получите еще несколько звезд, чтобы открыть самый сложный уровень!";
	private static string Helpach3 = "Выберите уровень сложности. Какой вам нравится?";

	// Use this for initialization
	void Start () {
		SayCloud2.gameObject.SetActive (false);
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake () {
		bool[] matrix;
		if (SM.Grade < (int)grades.shkolnik) {
			matrix = new bool[] {false, true, true, true};
			StartCoroutine (SayHello (Helpach0));
			}
		else if (SM.Grade < (int)grades.gramotei) {
			matrix = new bool[] {false, false, true, true};
			StartCoroutine (SayHello (Helpach1));
			}
		else if (SM.Grade < (int)grades.otlichnik) {
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
