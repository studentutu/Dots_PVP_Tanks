using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestDependencies", menuName = "RoomEscape/Test/Dependencies", order = 1)]
public class TestDependencyInjection : ScriptableObject
{
    private static TestDependencyInjection instance = null;
    public static TestDependencyInjection Instance
    {
        get
        {
            if (instance == null) Init();
            return instance;
        }
    }

    private static void Init()
    {
        if (instance == null)
        {
            var allLoadedResourcesObjects = UnityEngine.Resources.LoadAll<TestDependencyInjection>("Tests");
            // When Instantiated - it clones everything from it!
            instance = UnityEngine.ScriptableObject.Instantiate<TestDependencyInjection>(allLoadedResourcesObjects[0]);
            // This is what Unity does on the start of the application for each Scriptable Object.
            instance.hideFlags = UnityEngine.HideFlags.NotEditable | UnityEngine.HideFlags.HideAndDontSave;
            for (int i = 0; i < allLoadedResourcesObjects.Length; i++)
            {
                UnityEngine.Resources.UnloadAsset(allLoadedResourcesObjects[i]);
            }
            allLoadedResourcesObjects = null;
        }
    }

    [System.Serializable]
    public class ItemAndItsTestObject
    {
        // public ItemModel model;
        public TestBoundsProvider testBounds;

        // Ad more in here
    }
    private Dictionary<string, ItemAndItsTestObject> dictionary = null;
    public Dictionary<string, ItemAndItsTestObject> Dictionary
    {
        get
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<string, ItemAndItsTestObject>(allItemAndTestProviders.Length);
            }
            dictionary.Clear();
            foreach (var item in allItemAndTestProviders)
            {
                // if (item != null && item.model != null && item.testBounds != null &&
                //     !dictionary.ContainsKey(item.model.PrefabView.gameObject.name)
                //     // !dictionary.ContainsKey(item.model.VisualPrefab.gameObject.name + " "

                //     )
                // {
                //     dictionary.Add(item.model.name, item);
                //     dictionary.Add(item.model.GetInstanceID().ToString(), item);
                //     if (!dictionary.ContainsKey(item.model.PrefabView.gameObject.name))
                //     {
                //         dictionary.Add(item.model.PrefabView.gameObject.name, item);
                //     }
                //     dictionary.Add(item.model.PrefabView.gameObject.name + " (Clone)", item);
                //     dictionary.Add(item.model.PrefabView.gameObject.name + "(Clone)", item);
                // }
            }
            return dictionary;
        }
    }
    [SerializeField] private ItemAndItsTestObject[] allItemAndTestProviders = new ItemAndItsTestObject[0];

}
