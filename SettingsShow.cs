using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class SettingsShow : MonoBehaviour {

	public Button Back;
	public StartupManager SM;
	public Text Title, Rules, Trophy, Basket, Tools, Nazad;

	// Use this for initialization
	void Start () {
		Title.text = GradesConst.ControlString[SM.Language];
		Rules.text = GradesConst.Rules[SM.Language];
		Trophy.text = GradesConst.Trophy[SM.Language];
		Basket.text = GradesConst.Basket[SM.Language];
		Tools.text = GradesConst.Tools[SM.Language];
		Nazad.text = GradesConst.Nazad[SM.Language];
	}
	
	// Update is called once per frame
	void Update () {
		if (SM.EscapeSupported)  // ловим аппаратную кнопку
			if (Input.GetKeyDown (KeyCode.Escape)) 
				Back.onClick.Invoke ();	
	}

}
}
