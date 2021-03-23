using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour {
	List < GameBehaviour> models;
	public Hero hs;
	public static Root Instance;
	// Use this for initialization
	void Start () {
		Instance = this;
		models = new List<GameBehaviour> ();

		hs = new Hero("hero");
		models.Add(hs);
		models.Add (new Enemy ("Enemy"));
		models.Add(new Enemy("Enemy (1)"));
		models.Add(new Enemy("Enemy (2)"));
		models.Add(new UIgold ("Score"));

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < models.Count; i++) {
			models [i].Update ();
		}
	}
}
