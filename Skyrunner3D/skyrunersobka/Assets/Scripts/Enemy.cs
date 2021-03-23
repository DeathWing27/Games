using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	GameObject Hero;
	Vector3 napr;
	public GameObject[] shootPlaces;
	GameObject pfbFire;
	// Use this for initialization
	void Awake () {
		Hero = GameObject.Find ("hero");
		pfbFire = Resources.Load<GameObject> ("fire");



	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		napr = Hero.transform.position - this.transform.position;
		this.transform.position += napr * 0.005f;
		this.transform.forward = napr;
		Shoot ();
	}
	void Shoot () {
		for (int i = 0; i < shootPlaces.Length; i++) {
			GameObject c = Instantiate (pfbFire);
		c.transform.position = shootPlaces [i].transform.position;
			c.GetComponent<Rigidbody> ().AddForce (this.transform.forward * 10000);
			c.transform.forward = this.transform.forward;
			c.name = pfbFire.name;
		    

		}



	}
}
