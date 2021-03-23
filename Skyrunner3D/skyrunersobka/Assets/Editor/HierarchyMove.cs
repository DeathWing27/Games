using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HierarchyMove : Editor
{

    [MenuItem("GameObject/Doctrina/MoveDown", false, 49)]
    public static void MoveDown()
    {
        if (Selection.activeGameObject != null)
        {
            
            GameObject go = Selection.activeGameObject;
            Undo.SetTransformParent(Selection.activeGameObject.transform, null, "move down");
            Selection.activeGameObject.transform.SetAsLastSibling();
        }
    }

    [MenuItem("GameObject/Doctrina/Group", false, 49)]
    public static void Group(MenuCommand menuComand)
    {		
        if (Selection.transforms.Length > 1)
        {

            if (menuComand.context != Selection.objects [0])
            {
                return;
            }

            //Find avarage point
            Vector3 p = Vector3.zero;
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                p += Selection.transforms [i].position;
            }
            p = p / Selection.transforms.Length;

            List<Transform> objs = new List<Transform>(Selection.transforms);
            objs.Sort((x, y) => x.GetSiblingIndex().CompareTo(y.GetSiblingIndex()));

            GameObject top = new GameObject(objs [0].name.ToLower() + "s");
            top.transform.position = p;

            top.transform.parent = objs [0].parent;
            top.transform.SetSiblingIndex(objs [0].GetSiblingIndex());
            Undo.RegisterCreatedObjectUndo(top, "creating group");
           
            for (int i = 0; i < objs.Count; i++)
            {
                Undo.SetTransformParent(objs [i], top.transform, "move to group");
            }

            Selection.activeGameObject = top.gameObject;
        }
    }

        
     

    [MenuItem("GameObject/Doctrina/UnGroup", false, 49)]
    public static void UnGroup()
    {		
        if (Selection.activeGameObject != null)
        {

            if (Selection.activeGameObject.transform.childCount > 0)
            {

                int chCount = Selection.activeGameObject.transform.childCount;
                for (int i = 0; i < chCount; i++)
                {
                    Transform t = Selection.activeGameObject.transform.GetChild(0);
                    Undo.SetTransformParent(t, Selection.activeGameObject.transform.parent, "detach");
		
                    t.SetSiblingIndex(Selection.activeGameObject.transform.GetSiblingIndex() + i);
                }

                Undo.DestroyObjectImmediate(Selection.activeGameObject);
				
            }				
        }
    }

    [MenuItem("GameObject/Doctrina/ToDecor", false, 49)]
    public static void ToDecor()
    {       
        if (Selection.activeGameObject != null)
        {

            GameObject decor = GameObject.Find("decor");

            if (decor != null)
            {
                int chCount = Selection.transforms.Length;
                for (int i = 0; i < chCount; i++)
                {
                    Transform t = Selection.transforms [i];
                    Undo.SetTransformParent(t, decor.transform, "attach");
                }
            } else
            {
                Debug.Log("No decor object in scene");
            }


                

               

        }               
    }




}
