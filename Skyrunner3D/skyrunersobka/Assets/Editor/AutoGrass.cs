//Doctrina(c)
//http://doctrina-kharkov.blogspot.com/
using UnityEngine;
using System.Collections;
using UnityEditor;

public class AutoGrass : EditorWindow
{
	Terrain ter;
	int selTexture;
	int selDetail;
	Material mat;

	[MenuItem("Doctrina/AutoGrass")]
	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(AutoGrass));
	}

	void  Awake ()
	{
		mat = new Material (Shader.Find ("Unlit/Transparent"));
	}

	void OnGUI ()
	{
		GUILayout.Label ("Select terrain with textures and grass");
				
		ter = (Terrain)EditorGUILayout.ObjectField (ter, typeof(Terrain), true);
		
		if (ter != null) {

			selTexture = Mathf.Clamp (EditorGUILayout.IntField ("Texture index:", selTexture), 0, ter.terrainData.alphamapLayers - 1);

			EditorGUI.DrawPreviewTexture (new Rect (10, 70, 100, 100), ter.terrainData.splatPrototypes [selTexture].texture, mat);

			GUILayout.Space (125f);


			//
			selDetail = Mathf.Clamp (EditorGUILayout.IntField ("Detail index:", selDetail), 0, ter.terrainData.detailPrototypes.Length - 1);
			
			EditorGUI.DrawPreviewTexture (new Rect (10, 200, 100, 100), ter.terrainData.detailPrototypes [selDetail].prototypeTexture, mat);

			GUILayout.Space (125f);

			if (GUILayout.Button ("Apply")) {
				int res = ter.terrainData.detailResolution;
				int [,] dens = new int[res, res];

				float[,,] alph = ter.terrainData.GetAlphamaps (0, 0, res, res); 

				for (int y = 0; y < res; y++) {
					for (int x = 0; x < res; x++) {
						//According to experiments 0..16
						dens [x, y] = (int)(alph [x, y, selTexture] * 16);
					}
				}

				ter.terrainData.SetDetailLayer (0, 0, selDetail, dens);
			}
		}
	}
}
