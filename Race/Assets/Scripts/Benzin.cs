using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Не объявлять переменные здесь
//Не объявлять переменные здесь
public class Benzin : MonoBehaviour {
	public GameObject Hero;
	float benzin; 
	Vector3  napr;

    //Объявлять переменные здесь

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = Hero.transform.position;

		this.transform.position = Hero.transform.position + new Vector3(0,-2,0);
		if (this.transform.localScale.x<0 ) {
			this.transform.localScale= new Vector3  (0, this.transform.localScale.y, 1);

				SceneManager.LoadScene("gameover");

		}
		
	}

}
