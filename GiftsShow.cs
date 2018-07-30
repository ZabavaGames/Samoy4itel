using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace MyMobileProject1 {

public class GiftsShow : MonoBehaviour {

	public StartupManager SM;
	public Button BuyADButton, BuyLvButton;
	public Text Title, BuyADText, BuyADPrice, BuyLvText, BuyLvPrice, Close;

	private bool state;

	// Use this for initialization
	void Start () {
		Title.text = GradesConst.Basket[SM.Language];
		BuyADPrice.text = GradesConst.PremiumPrice[SM.Language];
		BuyLvPrice.text = GradesConst.LvUnlockPrice[SM.Language];
		Close.text = GradesConst.Close[SM.Language];

		state = SM.InAppItems.DisableAds;
		if (state) {
			BuyADButton.gameObject.SetActive (false);
			BuyADText.text = GradesConst.ADPurchased[SM.Language];
			}
		else {
			BuyADButton.gameObject.SetActive (true);
			BuyADText.text = GradesConst.ADnotPurchased[SM.Language];
			}
		if (SM.Grade >= GradesConst.MaxGrade - 1)
			BuyLvText.text = GradesConst.AllLvPurchased[SM.Language];
		else 
			BuyLvText.text = GradesConst.LvnotPurchased[SM.Language];
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
				BuyADText.text = GradesConst.ADPurchased[SM.Language];
				}
			else {
Debug.Log ("не фигачим!");
				BuyADButton.gameObject.SetActive (true);
				BuyADText.text = GradesConst.ADnotPurchased[SM.Language];
				}
			state = SM.InAppItems.DisableAds;
		}
	}

}
}
