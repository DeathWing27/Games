using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventForwarder : MonoBehaviour {
	public GameBehaviour model;
	// Use this for initialization
	void OnCollisionEnter(Collision col){

		model.OnCollisionEnter (col);

	}
}
