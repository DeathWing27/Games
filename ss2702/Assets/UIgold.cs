using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIgold : GameBehaviour {
	Text tx;


	public UIgold (string name) {
		this.gameobject = GameObject.Find (name);
		tx = gameobject.GetComponent<Text> ();


	}
	public override void Update ()
	{
		base.Update ();
		Debug.Log(Root.Instance.hs.score);
	}

}
