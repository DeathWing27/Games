using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Не объявлять переменные здесь
//Не объявлять переменные здесь
public class Hero : MonoBehaviour {

	//Объявлять переменные здесь

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w")) {
			this.transform.Translate (0.1f, 0, 0);

		}
		if (Input.GetKey("s")) {
			this.transform.Translate (-0.1f, 0, 0);

		}
		if (Input.GetKey("a")) {
			this.transform.Translate (0, 0.1f, 0);
			this.transform.Rotate (0, 0, 3);
		}
		if (Input.GetKey("d")) {
			this.transform.Translate (0, 0.1f, 0);
			this.transform.Rotate (0, 0, -3);

		}

		
	}
}
