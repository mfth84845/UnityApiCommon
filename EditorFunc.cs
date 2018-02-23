using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AttributeTargets = System.AttributeTargets;

#if UNITY_EDITOR

using UnityEditor;
using ReorderableList = UnityEditorInternal.ReorderableList;

[CustomEditor(typeof(EditorFuncClass))]
public class EditorFuncClassEditor : Editor
{

    ReorderableList l;

    private void OnEnable()
    {
        if (l != null)
            return;

        var targetList = serializedObject.FindProperty("rList");
        l = new ReorderableList(serializedObject, targetList, true, true, true, true);

        l.elementHeightCallback = (index) =>
          {
              int r = targetList.GetArrayElementAtIndex(index).intValue;
              return EditorGUIUtility.singleLineHeight * (r > 0 ? r : 1);
          };

        l.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
         {
             EditorGUI.LabelField(rect, "", GUI.skin.box);

             Rect cur = rect;
             cur.height = EditorGUIUtility.singleLineHeight;

             EditorGUI.PropertyField(cur, targetList.GetArrayElementAtIndex(index),
                 new GUIContent(string.Format("customInfos,high is {0}*singleLine", targetList.GetArrayElementAtIndex(index).intValue)));
         };
        l.onAddCallback = (ReorderableList list) =>
          {
              targetList.InsertArrayElementAtIndex(targetList.arraySize);
          };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        l.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }




}

[UnityEditor.CustomPropertyDrawer(typeof(ExampleProp))]
public class PropCustomDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.LabelField(position, "DrawBy[PropCustomDrawer]");
    }
}

[UnityEditor.CustomPropertyDrawer(typeof(ExampleAttribute))]
public class ExampleAttributeDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = this.attribute as ExampleAttribute;
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.LabelField(position, attr.info);
        EditorGUI.EndDisabledGroup();
    }
}

public class ExampleWindow : UnityEditor.EditorWindow
{
    [MenuItem("Example/window1")]
    public static void Launch()
    {
        EditorWindow.GetWindow<ExampleWindow>();
    }

    int c = 0;
    Vector2 scr;
    private void OnGUI()
    {
        c = EditorGUILayout.IntField(c);

        scr = EditorGUILayout.BeginScrollView(scr);
        for (int i = 0; i < c; i++)
        {
            EditorGUILayout.LabelField(string.Format("draw by example window {0}", i));
        }
        EditorGUILayout.EndScrollView();
    }
}

public class ExampleWizard : ScriptableWizard
{
    public string needName = "input info";
    public Mesh mesh;

    [MenuItem("Example/ExampleWizard1")]
    static void Launch()
    {
        ScriptableWizard.DisplayWizard<ExampleWizard>("example Wizard", "createButtonHere");
    }

    private void OnWizardCreate()
    {
        var v = mesh.vertices;
        for (int i = 0; i < v.Length; i++)
        {
            v[i] += Vector3.right;
        }
        mesh.vertices = v;
        mesh.RecalculateBounds();
    }
}

public class ExampleCommonFuncs
{
    [MenuItem("Example/Dialog")]
    static void Dialog()
    {
        EditorUtility.DisplayDialog("", "ExampleDiaLog", "Fin");
    }
}

#endif
[System.Serializable]
public struct ExampleProp
{


}

[System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class ExampleAttribute : UnityEngine.PropertyAttribute
{
    public string info;
    public ExampleAttribute(string input)
    {
        info = input;
    }
}


public class EditorFuncClass : MonoBehaviour
{
    public ExampleProp propCustom;
    [Example("TryUse[ExampleAttribute1]")]
    public int exampleValue = 2;
    [Example("TryUse[ExampleAttribute2]")]
    public int exampleValue2 = 2;

    public int[] rList;
}
