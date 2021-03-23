using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour {
	Rigidbody rb;
	GameObject pfbFire;
	public GameObject[] shootPlaces;
	//public Image Boost;
	public Image HP;
	public Image EnergyShield;
	public GameObject energyshieldlvl1;
	public GameObject energyshieldlvl2;
	public int Shieldlvl;

	float timer ;
	public float BoostAmount;
	bool boosttest;


	// Use this for initialization
	void Awake () {
		
	rb = GetComponent<Rigidbody> ();
	pfbFire = Resources.Load<GameObject> ("fire");


			
	}
	void Start () {
		

	}
	void Update () {



		Control ();
		Shoot ();
		Boosted ();
	}
	void Control (){
		if (Input.GetKey("space")) {
			rb.AddForce (this.transform.forward * 10000);//едем вперед
		}

		if (Input.GetKey("q")) {
			rb.AddForce (-this.transform.forward * 3000); //едем назад
		}

		float h = Input.GetAxis ("Horizontal");
		rb.AddRelativeTorque (0, h*4000, 0); // вращаемся влево вправо

		float v = Input.GetAxis ("Vertical");
		rb.AddRelativeTorque (-v*1000, 0, 0);// аналогично вверх вниз

		this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0); //замараживаем вращение по Z
	}
	void Shoot(){
		if (Input.GetMouseButtonDown(0)) {
			for (int i = 0; i < shootPlaces.Length; i++) {
				GameObject c = Instantiate (pfbFire);
				c.transform.position = shootPlaces [i].transform.position;
				c.GetComponent<Rigidbody> ().AddForce (this.transform.forward * 10000);
				c.transform.forward = this.transform.forward;
				c.name = pfbFire.name;

			}
		}
	}
	void Boosted () {
		if (Input.GetKey ("f")&& BoostAmount>0) {
			BoostAmount -= 0.009f;
			rb.AddForce (this.transform.forward * 40000);
		} else {
			BoostAmount += 0.05f;

		}

		BoostAmount = Mathf.Clamp01 (BoostAmount);

	}
	public void OnCollisionEnter (Collision col) {
		if (col.gameObject.name == "energyshieldIsActive") {

			print ("1");
			Shieldlvl++;
			Destroy (col.gameObject);
			if (Shieldlvl>=1) {
				energyshieldlvl1.SetActive (true);
			}
			if (Shieldlvl>=2) {
				energyshieldlvl2.SetActive (true);
			}

		}


	}



		


}
