using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoost : MonoBehaviour {
	 Hero hs;
	public  Image img;
	// Use this for initialization
	void Start () {
		hs = GameObject.Find ("hero").GetComponent<Hero> ();
		img = this.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		img.fillAmount = hs.BoostAmount; 

	}
}
