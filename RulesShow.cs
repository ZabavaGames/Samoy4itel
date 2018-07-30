using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class RulesShow : MonoBehaviour {

	public Text RulesText, Title, Close;
	public Button Back;
	public StartupManager SM;


	// Use this for initialization
	void Start () {
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {
		if (SM.EscapeSupported)  // ловим аппаратную кнопку
			if (Input.GetKeyDown (KeyCode.Escape)) 
				Back.onClick.Invoke ();	
	}

	void Awake () {
		RulesText.text = GradesConst.RulesText[SM.Language];
		Close.text = GradesConst.Close[SM.Language];
		Title.text = GradesConst.Rules[SM.Language];
	}


}
}
