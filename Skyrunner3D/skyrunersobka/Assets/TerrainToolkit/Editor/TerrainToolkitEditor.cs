/*
---------------------- Unity Terrain Toolkit ----------------------
--
-- Unity Summer of Code 2009
-- Terrain Toolkit for Unity (Version 1.0)
-- All code by Sándor Moldán.
--
-- TerrainToolkitEditor.cs
--
-------------------------------------------------------------------
*/

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

// -------------------------------------------------------------------------------------------------------- EDITOR

[CustomEditor(typeof(TerrainToolkit))]
public class TerrainToolkitEditor : Editor {
	
	private bool showAdvancedSettings;
	private bool showInterfaceSettings;
	private string dragControl = "";
	private bool assignTexture = false;
	int i;
	int n;
	
	public override void OnInspectorGUI() {
        
		EditorGUIUtility.LookLikeControls();
		if (Application.isPlaying) {
			//return;
		}
		TerrainToolkit terrain = (TerrainToolkit) target as TerrainToolkit;
		if (!terrain.gameObject) {
			return;
		}
		Terrain terComponent = (Terrain) terrain.GetComponent(typeof(Terrain));
		if (!terrain.guiSkin) {
			terrain.guiSkin = (GUISkin) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/TerrainErosionEditorSkin.guiskin", typeof(GUISkin));
			terrain.createIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/createIcon.png", typeof(Texture2D));
			terrain.erodeIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/erodeIcon.png", typeof(Texture2D));
			terrain.textureIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/textureIcon.png", typeof(Texture2D));
			terrain.mooreIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/mooreIcon.png", typeof(Texture2D));
			terrain.vonNeumannIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/vonNeumannIcon.png", typeof(Texture2D));
			terrain.mountainsIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/mountainsIcon.png", typeof(Texture2D));
			terrain.hillsIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/hillsIcon.png", typeof(Texture2D));
			terrain.plateausIcon = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Editor/Resources/plateausIcon.png", typeof(Texture2D));
			terrain.defaultTexture = (Texture2D) AssetDatabase.LoadAssetAtPath("Assets/TerrainToolkit/Textures/default.jpg", typeof(Texture2D));
		}
		if (!terrain.presetsInitialised) {
			terrain.addPresets();
		}

		if (terComponent == null) {
			EditorGUILayout.Separator();
			EditorGUILayout.BeginHorizontal();
			GUI.skin = terrain.guiSkin;
			GUILayout.Label("The GameObject that Terrain Toolkit is attached to", "errorText");
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("does not have a Terrain component.", "errorText");
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label("Please attach a Terrain component.", "errorText");
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			return;
		}
		if (terrain.heightBlendPoints == null) {
			terrain.heightBlendPoints = new List<float>();
		}

        //

		Rect buttonRect;
		EditorGUILayout.Separator();
//		EditorGUILayout.BeginHorizontal();
//		GUIContent[] toolbarOptions = new GUIContent[3];
//		toolbarOptions[0] = new GUIContent("Create", terrain.createIcon);
//		toolbarOptions[1] = new GUIContent("Erode", terrain.erodeIcon);
//		toolbarOptions[2] = new GUIContent("Texture", terrain.textureIcon);
//		terrain.toolModeInt = GUILayout.Toolbar(terrain.toolModeInt, toolbarOptions);
//		EditorGUILayout.EndHorizontal();

		switch (terrain.toolModeInt) {
			// -------------------------------------------------------------------------------------------------------- GENERATOR TOOLS
			case 0:
			// -------------------------------------------------------------------------------------------------------- TEXTURING TOOLS
			case 2:
			Terrain ter = (Terrain) terrain.GetComponent(typeof(Terrain));
			if (ter == null) {
				return;
			}
			TerrainData terData = ter.terrainData;
			terrain.splatPrototypes = terData.splatPrototypes;
			EditorGUILayout.Separator();
			float mouseX;
			EditorGUILayout.BeginHorizontal();
			GUI.skin = terrain.guiSkin;
			GUILayout.Label("Texture Slope");
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			Rect gradientRect = EditorGUILayout.BeginHorizontal();
			float gradientWidth = gradientRect.width - 55;
			gradientRect.width = 15;
			gradientRect.height = 19;
			GUI.skin = terrain.guiSkin;
			// Slope stop 1...
			if (dragControl == "slopeStop1" && Event.current.type == EventType.MouseDrag) {
				mouseX = Event.current.mousePosition.x - 7;
				if (mouseX < 20) {
					mouseX = 20;
				} else if (mouseX > 19 + gradientWidth * (terrain.slopeBlendMaxAngle / 90)) {
					mouseX = 19 + gradientWidth * (terrain.slopeBlendMaxAngle / 90);
				}
				gradientRect.x = mouseX;
				terrain.slopeBlendMinAngle = ((mouseX - 20) / (gradientWidth + 1)) * 90;
			} else {
				gradientRect.x = 20 + gradientWidth * (terrain.slopeBlendMinAngle / 90);
			}
			if (Event.current.type == EventType.MouseDown && gradientRect.Contains(Event.current.mousePosition)) {
				dragControl = "slopeStop1";
			}
			if (dragControl == "slopeStop1" && Event.current.type == EventType.MouseUp) {
				dragControl = "";
			}
			GUI.Box(gradientRect, "", "slopeStop1");
			// Slope stop 2...
			if (dragControl == "slopeStop2" && Event.current.type == EventType.MouseDrag) {
				mouseX = Event.current.mousePosition.x - 7;
				if (mouseX < 21 + gradientWidth * (terrain.slopeBlendMinAngle / 90)) {
					mouseX = 21 + gradientWidth * (terrain.slopeBlendMinAngle / 90);
				} else if (mouseX > 21 + gradientWidth) {
					mouseX = 21 + gradientWidth;
				}
				gradientRect.x = mouseX;
				terrain.slopeBlendMaxAngle = ((mouseX - 20) / (gradientWidth + 1)) * 90;
			} else {
				gradientRect.x = 20 + gradientWidth * (terrain.slopeBlendMaxAngle / 90);
			}
			if (Event.current.type == EventType.MouseDown && gradientRect.Contains(Event.current.mousePosition)) {
				dragControl = "slopeStop2";
			}
			if (dragControl == "slopeStop2" && Event.current.type == EventType.MouseUp) {
				dragControl = "";
			}
			GUI.Box(gradientRect, "", "slopeStop2");
			gradientRect.y += 19;
			gradientRect.width = gradientWidth * (terrain.slopeBlendMinAngle / 90);
			gradientRect.x = 27;
			GUI.Box(gradientRect, "", "black");
			gradientRect.width = gradientWidth * ((terrain.slopeBlendMaxAngle / 90) - (terrain.slopeBlendMinAngle / 90));
			gradientRect.x = 27 + gradientWidth * (terrain.slopeBlendMinAngle / 90);
			GUI.Box(gradientRect, "", "blackToWhite");
			gradientRect.width = gradientWidth - gradientWidth * (terrain.slopeBlendMaxAngle / 90);
			gradientRect.x = 27 + gradientWidth * (terrain.slopeBlendMaxAngle / 90);
			GUI.Box(gradientRect, "", "white");
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Cliff start");
			terrain.slopeBlendMinAngle = EditorGUILayout.FloatField(terrain.slopeBlendMinAngle);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Cliff end");
			terrain.slopeBlendMaxAngle = EditorGUILayout.FloatField(terrain.slopeBlendMaxAngle);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			GUI.skin = terrain.guiSkin;
			GUILayout.Label("Texture Height");
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			gradientRect = EditorGUILayout.BeginHorizontal();
			gradientWidth = gradientRect.width - 55;
			gradientRect.width = 15;
			gradientRect.height = 19;
			Rect gradientRect2 = gradientRect;
			gradientRect2.y += 19;
			GUI.skin = terrain.guiSkin;
			string[] gradientStyles = new string[9];
			gradientStyles[0] = "red";
			gradientStyles[1] = "redToYellow";
			gradientStyles[2] = "yellow";
			gradientStyles[3] = "yellowToGreen";
			gradientStyles[4] = "green";
			gradientStyles[5] = "greenToCyan";
			gradientStyles[6] = "cyan";
			gradientStyles[7] = "cyanToBlue";
			gradientStyles[8] = "blue";
			List<float> heightBlendPoints = terrain.heightBlendPoints;
			int numPoints = heightBlendPoints.Count;
			float firstLimit = 1;
			if (numPoints > 0) {
				firstLimit = (float) heightBlendPoints[0];
			} else {
				gradientRect.x = 20;
				GUI.Box(gradientRect, "", "greyStop");
				gradientRect.x = 20 + gradientWidth;
				GUI.Box(gradientRect, "", "greyStop");
			}
			gradientRect2.width = gradientWidth * firstLimit;
			gradientRect2.x = 27;
			if (terrain.splatPrototypes.Length < 2) {
				GUI.Box(gradientRect2, "", "grey");
			} else {
				GUI.Box(gradientRect2, "", "red");
			}
			for (i = 0; i < numPoints; i++) {
				// Height stop...
				float lowerLimit = 0;
				float upperLimit = 1;
				if (i > 0) {
					lowerLimit = (float) heightBlendPoints[i - 1];
				}
				if (i < numPoints - 1) {
					upperLimit = (float) heightBlendPoints[i + 1];
				}
				if (dragControl == "heightStop"+i && Event.current.type == EventType.MouseDrag) {
					mouseX = Event.current.mousePosition.x - 7;
					if (mouseX < 20 + gradientWidth * lowerLimit) {
						mouseX = 20 + gradientWidth * lowerLimit;
					} else if (mouseX > 19 + gradientWidth * upperLimit) {
						mouseX = 19 + gradientWidth * upperLimit;
					}
					gradientRect.x = mouseX;
					heightBlendPoints[i] = (mouseX - 20) / (gradientWidth + 1);
				} else {
					gradientRect.x = 20 + gradientWidth * (float) heightBlendPoints[i];
				}
				if (Event.current.type == EventType.MouseDown && gradientRect.Contains(Event.current.mousePosition)) {
					dragControl = "heightStop"+i;
				}
				if (dragControl == "heightStop"+i && Event.current.type == EventType.MouseUp) {
					dragControl = "";
				}
				int stopNum = (int) Mathf.Ceil((float) i / 2) + 1;
				if (i % 2 == 0) {
					GUI.Box(gradientRect, ""+stopNum, "blackStop");
				} else {
					GUI.Box(gradientRect, ""+stopNum, "whiteStop");
				}
				gradientRect2.width = gradientWidth * (upperLimit - (float) heightBlendPoints[i]);
				gradientRect2.x = 27 + gradientWidth * (float) heightBlendPoints[i];
				GUI.Box(gradientRect2, "", gradientStyles[i + 1]);
			}
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			string startOrEnd = "end";
			for (i = 0; i < numPoints; i++) {
				EditorGUILayout.BeginHorizontal();
				int floatFieldNum = (int) Mathf.Ceil((float) i / 2) + 1;
				EditorGUILayout.PrefixLabel("Texture "+floatFieldNum+" "+startOrEnd);
				heightBlendPoints[i] = EditorGUILayout.FloatField((float) heightBlendPoints[i]);
				EditorGUILayout.EndHorizontal();
				if (startOrEnd == "end") {
					startOrEnd = "start";
				} else {
					startOrEnd = "end";
				}
			}
			terrain.heightBlendPoints = heightBlendPoints;
			EditorGUILayout.BeginHorizontal();
			GUI.skin = terrain.guiSkin;
			GUILayout.Label("Textures");
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			int nTextures = 0;
			EditorGUILayout.Separator();
			if (GUI.changed) {
				EditorUtility.SetDirty(terrain);
			}
			GUI.changed = false;
			EditorGUILayout.BeginHorizontal();
			EditorGUIUtility.LookLikeControls(80, 0);
			foreach (SplatPrototype splatPrototype in terrain.splatPrototypes) {
				Rect textureRect = EditorGUILayout.BeginHorizontal();
				if (nTextures == 0) {
					splatPrototype.texture = EditorGUILayout.ObjectField("Cliff texture", splatPrototype.texture, typeof(Texture2D)) as Texture2D;
				} else {
					splatPrototype.texture = EditorGUILayout.ObjectField("Texture "+nTextures, splatPrototype.texture, typeof(Texture2D)) as Texture2D;
				}
				GUI.skin = terrain.guiSkin;
				textureRect.x += 146;
				textureRect.width = 18;
				textureRect.height = 18;
				if (GUI.Button(textureRect, "", "deleteButton")) {
					GUI.changed = true;
					terrain.deleteSplatPrototype(terrain.tempTexture, nTextures);
					EditorUtility.SetDirty(terrain);
				}
				GUI.skin = null;
				EditorGUILayout.EndHorizontal();
				if (nTextures % 2 == 1) {
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.Separator();
					EditorGUILayout.BeginHorizontal();
				}
				nTextures++;
				if (nTextures > 5) {
					break;
				}
			}

			EditorGUIUtility.LookLikeControls();
			EditorGUILayout.EndHorizontal();

			if (GUI.changed) {
				terData.splatPrototypes = terrain.splatPrototypes;
			}

			if (nTextures == 0 && !assignTexture) {
				EditorGUILayout.BeginHorizontal();
				GUI.skin = terrain.guiSkin;
				GUILayout.Label("No textures have been assigned! Assign a texture.", "errorText");
				GUI.skin = null;
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Separator();
			}
			if (nTextures < 6) {
				EditorGUILayout.Separator();
				buttonRect = EditorGUILayout.BeginHorizontal();
				buttonRect.x = buttonRect.width / 2 - 50;
				buttonRect.width = 100;
				buttonRect.height = 18;
				if (GUI.Button(buttonRect, "Add texture")) {
					terrain.addSplatPrototype(terrain.defaultTexture, nTextures);
					terData.splatPrototypes = terrain.splatPrototypes;
					EditorUtility.SetDirty(terrain);
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();
			}
			EditorGUILayout.Separator();
		
			buttonRect = EditorGUILayout.BeginHorizontal();
			buttonRect.x = buttonRect.width / 2 - 100;
			buttonRect.width = 200;
			buttonRect.height = 18;
			//GUI.skin = terrain.guiSkin;
			//Debug.Log( buttonRect );

			if (nTextures < 2) {
				GUI.Box(buttonRect, "Apply procedural texture", "disabledButton");
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();
				EditorGUILayout.BeginHorizontal();
				GUILayout.Label("This feature is disabled! You must assign at least 2 textures.", "errorText");
			} else {			
				if (GUI.Button(buttonRect, "Apply")) {
					// Undo not supported!
					TerrainToolkit.TextureProgressDelegate textureProgressDelegate = new TerrainToolkit.TextureProgressDelegate(updateTextureProgress);
					terrain.textureTerrain(textureProgressDelegate);
					EditorUtility.ClearProgressBar();
					GUIUtility.ExitGUI();
				}
			}
			GUI.skin = null;
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
                			
			// If the user has added or removed textures in the Terrain component, correct the number of blend points...
			if (Event.current.type == EventType.Repaint) {
				if (numPoints % 2 != 0) {
					terrain.deleteAllBlendPoints();
				}
				int correctNumPoints = (nTextures - 2) * 2;
				if (nTextures < 3) {
					correctNumPoints = 0;
				}
				if (numPoints < correctNumPoints) {
					terrain.addBlendPoints();
				} else if (numPoints > correctNumPoints) {
					terrain.deleteBlendPoints();
				}
			}
			break;
		}
		if (GUI.changed) {
			EditorUtility.SetDirty(terrain);
		}
	}
	
	public void OnSceneGUI() {
		TerrainToolkit terrain = (TerrainToolkit) target as TerrainToolkit;
		if (Event.current.type == EventType.MouseDown) {
			terrain.isBrushPainting = true;
		}
		if (Event.current.type == EventType.MouseUp) {
			terrain.isBrushPainting = false;
		}
		if (Event.current.shift) {
			if (!terrain.isBrushPainting) {
				// Undo...
				Terrain ter = (Terrain) terrain.GetComponent(typeof(Terrain));
				if (ter == null) {
					return;
				}
				TerrainData terData = ter.terrainData;
				Undo.RegisterUndo(terData, "Terrain Erosion Brush");
			}
			terrain.isBrushPainting = true;
		} else {
			terrain.isBrushPainting = false;
		}
		terrain.isBrushHidden = false;
		if (terrain.isBrushOn) {
			Vector2 mouse = Event.current.mousePosition;
			mouse.y = Camera.current.pixelHeight - mouse.y + 20;
			Ray ray = Camera.current.ScreenPointToRay(mouse);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if (hit.transform.GetComponent("TerrainToolkit")) {
					terrain.brushPosition = hit.point;
					if (terrain.isBrushPainting) {
						// Paint...
						terrain.paint();
					}
				}
			} else {
				terrain.isBrushHidden = true;
			}
		}
	}
	
	public void updateTextureProgress(string titleString, string displayString, float percentComplete) {
		EditorUtility.DisplayProgressBar(titleString, displayString, percentComplete);
	}
	
	
}

// -------------------------------------------------------------------------------------------------------- END