using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

[CustomEditor(typeof(TestDependencyInjection), true)]
// [CanEditMultipleObjects]
public class EditorForTestInjector : Editor
{
    private TestDependencyInjection m_Holder;
    private List<TestBoundsProvider> m_findAllModels = new List<TestBoundsProvider>();

    private SerializedProperty m_propertyForListOfModelds = null;
    protected virtual void OnEnable()
    {
        m_Holder = (TestDependencyInjection)target;
        m_propertyForListOfModelds = FindTheSerializedProperty();
    }

    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("Find All Model from the Project"))
        {
            FindAllModels();
            ApplyToSerializedProperty();

            // To Save scriptable object we need to use set dirty and then save Assets
            serializedObject.ApplyModifiedProperties();
            PrefabUtility.RecordPrefabInstancePropertyModifications(m_Holder);
            EditorUtility.SetDirty(m_Holder);
            serializedObject.Update();
            AssetDatabase.SaveAssets();
            return;
        }
        DoOtherStuff();
        DrawPropertiesExcluding(serializedObject, new string[] { "m_Script" });
        serializedObject.ApplyModifiedProperties(); // needs to be here so that when you manfully change something, it will be displayed and saved
    }

    protected virtual void DoOtherStuff()
    {

    }

    private void FindAllModels()
    {
        // allLengthOfEnum = System.Enum.GetNames(typeof(KAU.Audio.SoundId)).Length;
        m_findAllModels.Clear();
        var filter = AssetDatabase.FindAssets("t:" + typeof(GameObject).Name);
        TestBoundsProvider testBOunds = null;
        foreach (var item in filter)
        {
            string pathTo = AssetDatabase.GUIDToAssetPath(item);
            var ModelPreProcessor = AssetDatabase.LoadAssetAtPath(pathTo, typeof(GameObject)) as GameObject;
            testBOunds = ModelPreProcessor.GetComponent<TestBoundsProvider>();
            if (testBOunds != null)
            {
                m_findAllModels.Add(testBOunds);
            }
        }
    }

    private void ApplyToSerializedProperty()
    {
        // allSoundFromTarget
        // m_propertyForListOfModelds.arraySize = m_findAllModels.Count;
        // var actualEntries = new List<TestDependencyInjection.ItemAndItsTestObject>();
        // TestDependencyInjection.ItemAndItsTestObject entry = null;
        // foreach (var item in m_findAllModels)
        // {
        //     entry = new TestDependencyInjection.ItemAndItsTestObject();
        //     entry.model = item.DebugContextData;
        //     entry.testBounds = item;
        //     actualEntries.Add(entry);
        // }

        // for (int i = 0; i < m_propertyForListOfModelds.arraySize; i++)
        // {
        //     m_propertyForListOfModelds.SetObjectValueAt(i, actualEntries[i]);
        //     SerializedProperty newSerprop = m_propertyForListOfModelds.GetArrayElementAtIndex(i);
        //     var propertyModel = newSerprop.FindPropertyRelative(nameof(TestDependencyInjection.ItemAndItsTestObject.model));
        //     propertyModel.objectReferenceValue = actualEntries[i].model;

        //     var propertyBounds = newSerprop.FindPropertyRelative(nameof(TestDependencyInjection.ItemAndItsTestObject.testBounds));
        //     propertyBounds.objectReferenceValue = actualEntries[i].testBounds;
        //     // newSerprop.objectReferenceValue = m_findAllModels[i].gameObject; // need to cast into Unity.Object
        // }
    }

    private SerializedProperty FindTheSerializedProperty()
    {
        var typeToLook = typeof(TestDependencyInjection.ItemAndItsTestObject[]);
        foreach (System.Reflection.FieldInfo fi in
            GetAllFields(m_Holder.GetType(),
                                        BindingFlags.Public
                                        | BindingFlags.NonPublic
                                        | BindingFlags.Instance
                                        | BindingFlags.DeclaredOnly
        ))
        {
            if (fi.FieldType == typeToLook)
            {
                m_propertyForListOfModelds = serializedObject.FindProperty(fi.Name);
            }
        }

        if (m_propertyForListOfModelds == null)
        {
            UnityEngine.Debug.LogError(string.Format("<Color=Red> Field not found! </Color>"));
        }
        return m_propertyForListOfModelds;
    }

    private static IEnumerable<FieldInfo> GetAllFields(Type t, System.Reflection.BindingFlags bindingFlags)
    {
        if (t == null)
            return Enumerable.Empty<FieldInfo>();

        BindingFlags flags = bindingFlags;
        // BindingFlags.Public | BindingFlags.NonPublic |
        // BindingFlags.Static | BindingFlags.Instance |
        // BindingFlags.DeclaredOnly;
        return t.GetFields(flags).Concat(GetAllFields(t.BaseType, bindingFlags));
    }

}
