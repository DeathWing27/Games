using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameBehaviour {
	float timer;
	// Use this for initialization
	public Enemy (string name) {

		this.gameobject = GameObject.Find (name);

	}
	public override void Update ()
	{
		base.Update ();
		timer += Time.deltaTime;
		if (timer > 2) {
			this.gameobject.transform.position = new Vector3 (Random.Range (-5, 5),0.61f, Random.Range (-5, 5));
			timer = 0;
		}

		}
		
	}

