using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class RulesShow : MonoBehaviour {

	public Text RulesText;
	public StartupManager SM;

	private static string Rules = "Вам нужно исправлять грамматические ошибки, зачеркивая пальцем неправильные буквы в словах. \nЧем быстрее вы это делаете, тем лучше! \n\nЗа выполнение задания, состоящего из нескольких фраз, вы получаете звезды. Собирая звезды, вы повышаете свой уровень в игре. \n\nС каждым новым уровнем задания становятся все интереснее! \nПопробуйте достичь максимального уровня и заработать звание Волшебника!"; 

	// Use this for initialization
	void Start () {
		Awake ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Awake () {
		RulesText.text = Rules;
	}


}
}
