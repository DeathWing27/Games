using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGame : MonoBehaviour
{
	
	// Use this for initialization
	GameObject hero;

	void Awake ()
	{
		hero = GameObject.Find ("hero");




	}

	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		

	}

	void FixedUpdate ()
	{

//		this.transform.rotation  = Quaternion.Lerp (this.transform.rotation, hero.transform.rotation, 5 *Time.deltaTime);

		this.transform.position = Vector3.Lerp (this.transform.position, hero.transform.position, 8 * Time.deltaTime); //позиция камеры 

		if (Input.GetMouseButton (1)) {
			float mh = Input.GetAxis ("Mouse X"); // считываем мышку 
			float mv = Input.GetAxis ("Mouse Y");
			this.transform.eulerAngles += new Vector3 (-mv, mh, 0) * 2f; //прибавляем к углам вращение мышки
		} else {
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, hero.transform.rotation, 5 * Time.deltaTime); //если кнопка не нажата ставим вращение такое же как и у героя
		}
		Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, 55 + hero.GetComponent<Rigidbody> ().velocity.magnitude / 2, 20f * Time.deltaTime);
	}


}

