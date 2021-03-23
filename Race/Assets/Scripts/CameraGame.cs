using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//НЕ объявлять здесь
//НЕ объявлять здесь
public class CameraGame : MonoBehaviour {
	public GameObject Hero ;
	Vector3 napr; 



	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		napr = Hero.transform.position - this.transform.position;
		napr.z = 0;
		this.transform.position += napr * 0.05f;


		
	}
}
