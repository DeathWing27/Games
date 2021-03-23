using UnityEngine;
using UnityEditor;

public class RockPlacer : EditorWindow {

	Vector3 shift = new Vector3(0,-1,0);

	[MenuItem("Doctrina/Placer %g")]
	public static void ShowWindow ()
	{
		//Создать инстанс окна
		EditorWindow.GetWindow (typeof(RockPlacer));
	}
	
	//Обрабатываем код окна
	void OnGUI ()
	{

		shift = EditorGUILayout.Vector3Field ("Shift:", shift);

		if (GUILayout.Button ("Place")) {
			for (int i = 0; i < Selection.gameObjects.Length; i++) {
				RaycastHit h;
				if(Physics.Raycast(Selection.gameObjects[i].transform.position,Vector3.down,out h)){
					Selection.gameObjects[i].transform.position = h.point + shift;
				}
			}
		}

	}
}
