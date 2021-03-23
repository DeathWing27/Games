using UnityEngine;
using UnityEditor;

public class RockCreater : EditorWindow
{
	GameObject selection;
	GameObject pfbObj;

	Vector3 min;
	Vector3 max;

	Vector3 minAng = new Vector3(-30,0,-30);
	Vector3 maxAng = new Vector3(30,360,30);
	Vector3 Ang;

	Vector3 minSc = new Vector3 (0.3f, 0.3f, 0.3f);
	Vector3 maxSc = new Vector3 (1.2f, 1.2f, 1.2f);
	Vector3 Sc;

	int countOfObj = 1;

	[MenuItem("Doctrina/Creater %g")]
	public static void ShowWindow ()
	{
		//Создать инстанс окна
		EditorWindow.GetWindow (typeof(RockCreater));
	}
	
	//Обрабатываем код окна
	void OnGUI ()
	{
		//Выводим текст
		GUILayout.Label ("Plugin description");

		GUILayout.Label ("CentralObject:");
		selection = EditorGUILayout.ObjectField (selection, typeof(GameObject), true) as GameObject;
		GUILayout.Label ("PrefabOfObj:");
		pfbObj = EditorGUILayout.ObjectField (pfbObj, typeof(GameObject), true) as GameObject;
		GUILayout.Label ("Times:");
		countOfObj = EditorGUILayout.IntField (countOfObj);

		minAng = EditorGUILayout.Vector3Field ("Angle Min:", minAng);
		maxAng = EditorGUILayout.Vector3Field ("Angle Max:", maxAng);

		minSc = EditorGUILayout.Vector3Field ("Scale Min:", minSc);
		maxSc = EditorGUILayout.Vector3Field ("Scale Max:", maxSc);

		if (selection != null) {

		}
		//Выводим кнопку и обрабатываем нажатие
		if (selection != null) {
			if (GUILayout.Button ("Create")) {
				for (int i = 0; i<countOfObj; i++) {

					Vector3 rndPos = new Vector3 ();
			
					//rndPos.y = selection.transform.position.y + Random.Range( -50f, 50f );
					min = selection.GetComponent<Renderer> ().bounds.min;
					max = selection.GetComponent<Renderer> ().bounds.max;
					rndPos.x = Random.Range (min.x, max.x);
					rndPos.z = Random.Range (min.z, max.z);
					rndPos.y = Random.Range (min.y, max.y);

					Ang.x = Random.Range (minAng.x,maxAng.x);
					Ang.y = Random.Range (minAng.y,maxAng.y);
					Ang.z = Random.Range (minAng.z,maxAng.z);

					Sc.x = Random.Range (minSc.x,maxSc.x);
					Sc.y = Random.Range (minSc.y,maxSc.y);
					Sc.z = Random.Range (minSc.z,maxSc.z);

					if(Sc.x - Sc.z>0.5f){
						Sc.x = (Sc.x + Sc.z)/2;
						Sc.z = Sc.x;
					}

					GameObject clone = (GameObject)PrefabUtility.InstantiatePrefab (pfbObj);
					clone.transform.position = rndPos;
					clone.transform.localEulerAngles = Ang;
					clone.transform.localScale = Sc;

					Undo.RegisterCreatedObjectUndo (clone, "created prefab");
				}
			}
		}
	}

}
