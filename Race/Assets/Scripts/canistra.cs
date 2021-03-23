using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class canistra : MonoBehaviour {
	public GameObject Benzin;

	Vector3 napr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		napr = this.transform.position - Benzin.transform.position;
		if (napr.magnitude<5) {
			Benzin.transform.localScale -= new Vector3 (-2, 0, 0);
			Destroy (this.gameObject);
			
		}
		
		
	}
}
