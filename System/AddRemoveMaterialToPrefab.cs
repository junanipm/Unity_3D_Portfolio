using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
public class AddRemoveMaterialToPrefab : EditorWindow
{
    private Material mMaterialToAdd;
    private Material mMaterialToRemove;

    [MenuItem("Tools/Bonnate/Outline System/Material to Prefab")]
    public static void ShowWindow()
    {
        GetWindow<AddRemoveMaterialToPrefab>("Material to Prefab");
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.richText = true;

        GUILayout.Label("<color=white><b>선택한 오브젝트</b>에 <b>머티리얼</b>을 추가하거나 제거합니다.\n\n</color>", style);

        GUILayout.BeginHorizontal();
        mMaterialToAdd = (Material)EditorGUILayout.ObjectField("추가할 머티리얼", mMaterialToAdd, typeof(Material), false);
        if (GUILayout.Button("추가"))
        {
            AddMaterial();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        mMaterialToRemove = (Material)EditorGUILayout.ObjectField("제거할 머티리얼", mMaterialToRemove, typeof(Material), false);
        if (GUILayout.Button("제거"))
        {
            RemoveMaterial();
        }
        GUILayout.EndHorizontal();
    }

    private void AddMaterial()
    {

        foreach (GameObject prefab in Selection.gameObjects)
        {

            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {

                Material[] materials = renderer.sharedMaterials;
                bool materialExists = false;


                foreach (Material material in materials)
                    if (material == mMaterialToAdd)
                    {
                        materialExists = true;
                        break;
                    }


                if (!materialExists)
                {
                    ArrayUtility.Add(ref materials, mMaterialToAdd);
                    renderer.sharedMaterials = materials;
                }
            }
        }
    }

    private void RemoveMaterial()
    {

        foreach (GameObject prefab in Selection.gameObjects)
        {

            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {

                Material[] materials = renderer.sharedMaterials;


                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == mMaterialToRemove)
                    {
                        ArrayUtility.RemoveAt(ref materials, i);
                        renderer.sharedMaterials = materials;
                        break;
                    }
                }
            }
        }
    }
}
*/