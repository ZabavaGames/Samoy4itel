using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class RulesShow : MonoBehaviour {

	public Text RulesText;
	public Button Back;
	public StartupManager SM;

	private static string Rules = "В этой игре тебе нужно исправлять грамматические ошибки, зачёркивая пальцем неправильные буквы в словах. \nЧем быстрее ты это делаешь, тем лучше! \n\nЗа выполнение задания, состоящего из нескольких фраз, ты получаешь звёзды. Собирая звёзды, ты повышаешь свой уровень в игре. \n\nС каждым новым уровнем задания становятся всё интереснее! \nПопробуй достичь максимального уровня и заработать звание Волшебника!"; 

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
		RulesText.text = Rules;
	}


}
}
