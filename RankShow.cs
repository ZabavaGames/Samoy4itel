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
	public Text Title, Novice, Student, Master, Expert, Close;


	// Use this for initialization
	void Start () {
		Title.text = GradesConst.Difficulty[SM.Language];
		Novice.text = GradesConst.Novice[SM.Language];
		Student.text = GradesConst.Student[SM.Language];
		Master.text = GradesConst.Master[SM.Language];
		Expert.text = GradesConst.Expert[SM.Language];
		Close.text = GradesConst.Close[SM.Language];
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
			StartCoroutine (SayHello (GradesConst.Helpach0[SM.Language]));
			}
		else if (SM.Grade < GradesConst.RankGradeReq[(int)ranks.medium]) {
			matrix = new bool[] {false, false, true, true};
			StartCoroutine (SayHello (GradesConst.Helpach1[SM.Language]));
			}
		else if (SM.Grade < GradesConst.RankGradeReq[(int)ranks.advanced]) {
			matrix = new bool[] {false, false, false, true};
			StartCoroutine (SayHello (GradesConst.Helpach2[SM.Language]));
			}
		else {
			matrix = new bool[] {false, false, false, false};
			StartCoroutine (SayHello (GradesConst.Helpach3[SM.Language]));
			}
// для инглиша пока фигачим только так... тоже в ruslesson1.cs :108
        if (SM.LessonLanguage == (int)languages.english)
            {
             matrix = new bool[] { true, true, true, false };
             StartCoroutine(SayHello(GradesConst.Helpach4[SM.Language]));
            }

        RestrictAll(matrix);
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
