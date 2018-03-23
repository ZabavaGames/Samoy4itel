using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyMobileProject1 {

public class GiftsShow : MonoBehaviour {

	public StartupManager SM;

	public Button BuyADButton, BuyLvButton;
	public Text BuyADText, BuyLvText;

	private static string ADPurchased = "Вы уже купили пакет \"Премиум\". Спасибо за вашу поддержку!";
	private static string ADnotPurchased = "Купите пакет \"Премиум\" для того, чтобы отключить рекламу в игре и получать все будущие обновления бесплатно!";
	private static string AllLvPurchased = "Вы не можете купить максимальный уровень в игре! Но вы можете достичь его сами!";
	private static string LvnotPurchased = "Вы можете мгновенно повысить свой уровень. Вы автоматически получите необходимое для этого количество звезд.";
	private bool state;

	// Use this for initialization
	void Start () {
		state = SM.InAppItems.DisableAds;
		if (state) {
			BuyADButton.gameObject.SetActive (false);
			BuyADText.text = ADPurchased;
			}
		else {
			BuyADButton.gameObject.SetActive (true);
			BuyADText.text = ADnotPurchased;
			}
		if (SM.Grade >= GradesConst.MaxGrade - 1)
			BuyLvText.text = AllLvPurchased;
		else 
			BuyLvText.text = LvnotPurchased;
	}
	
	// Update is called once per frame
	void Update () {
		Awake ();
	}

	public void Awake () {
		if (state != SM.InAppItems.DisableAds) {
			if (SM.InAppItems.DisableAds) {
Debug.Log ("фигачим!");
				BuyADButton.gameObject.SetActive (false);
				BuyADText.text = ADPurchased;
				}
			else {
Debug.Log ("не фигачим!");
				BuyADButton.gameObject.SetActive (true);
				BuyADText.text = ADnotPurchased;
				}
			state = SM.InAppItems.DisableAds;
		}
	}

}
}
