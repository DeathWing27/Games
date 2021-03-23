using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : GameBehaviour {
	public int score;
	public Hero (string name){

		this.gameobject = GameObject.Find (name);
		this.gameobject.AddComponent<EventForwarder> ().model = this;

	}
  



	public override void Update ()
	{
		base.Update ();
		if (Input.GetKey("d")) {
			this.gameobject.transform.position+=new Vector3 (-0.1f, 0, 0);
		}
		if (Input.GetKey("a")) {
			this.gameobject.transform.position+=new Vector3 (0.1f, 0, 0);
		}
		if (Input.GetKey("w")) {
			this.gameobject.transform.position+=new Vector3 (0, 0,-0.1f);
		}
		if (Input.GetKey("s")) {
			this.gameobject.transform.position+=new Vector3 (0, 0, 0.1f);
		}


	}
	public override void OnCollisionEnter (Collision col)
	{
		base.OnCollisionEnter (col);
		Debug.Log (col.gameObject);
	}

}
