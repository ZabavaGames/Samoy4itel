using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class ToolsShow : MonoBehaviour {

	public Text Title, Field, Style, Style1, Table1, Font, Close;
	public StartupManager SM;

	// Use this for initialization
	void Start () {
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake () {
		Title.text = GradesConst.Tools[SM.Language];
		Field.text = GradesConst.TableText[SM.Language];
		Style.text = GradesConst.StyleText[SM.Language];
		Style1.text = GradesConst.Style1Text[SM.Language];
		Table1.text = GradesConst.Table1Text[SM.Language];
	//	Font.text = GradesConst.FontText[SM.Language];
		Close.text = GradesConst.Close[SM.Language];
	}

}
}
