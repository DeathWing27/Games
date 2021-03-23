using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class police : MonoBehaviour {
	public GameObject Hero;
	Vector3 napr;
	public GameObject Benzin;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		napr = Hero.transform.position - this.transform.position;
		this.transform.position += napr * 0.01f;
		this.transform.right = napr;

		if (napr.magnitude < 5) {
			Benzin.transform.localScale -= new Vector3 (0.05f, 0, 0);
		}
	

		
	}
}
