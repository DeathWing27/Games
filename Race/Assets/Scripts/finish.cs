using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour {
	public GameObject Hero;
	Vector3 napr;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		napr = Hero.transform.position - this.transform.position;
		if (napr.magnitude<3) {
			SceneManager.LoadScene ("gamewin");
		}





			
		}
		
	}

