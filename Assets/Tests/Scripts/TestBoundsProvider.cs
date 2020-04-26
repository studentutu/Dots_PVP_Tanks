using UnityEngine;

public static class ExtendedVersions
{
    // public static TestBoundsProvider SetTestContext(this BoundsProvider boundsProvider)
    // {
    //     var view = boundsProvider.transform.FindAncestorOfType<ItemView>();
    //     var bounds = boundsProvider.GetComponent<TestBoundsProvider>();
    //     if (bounds == null)
    //     {
    //         bounds = view.gameObject.AddComponent<TestBoundsProvider>();
    //     }
    //     var trim = view.gameObject.name;
    //     var index = trim.IndexOf(" (Clone)");
    //     if (index == -1)
    //     {
    //         index = trim.Length;
    //     }
    //     trim = trim.Substring(0, index);
    //     index = trim.IndexOf("(Clone)");
    //     if (index == -1)
    //     {
    //         index = trim.Length;
    //     }
    //     trim = trim.Substring(0, index);
    //     trim = trim.TrimEnd(' ');
    //     // Debug.LogWarning(" trimmed : " + trim);
    //     var from = TestDependencyInjection.Instance.Dictionary[trim];
    //     var json = JsonUtility.ToJson(from.testBounds);
    //     JsonUtility.FromJsonOverwrite(json, bounds);
    //     return bounds;
    // }
}

[DisallowMultipleComponent]
public class TestBoundsProvider : MonoBehaviour
{
    //     [System.NonSerialized] private BoundsProvider actualProvider = null;
    //     public BoundsProvider ActualProvider
    //     {
    //         get
    //         {
    //             if (actualProvider == null)
    //             {
    //                 actualProvider = GetComponentInChildren<BoundsProvider>();
    //             }
    //             return actualProvider;
    //         }
    //     }
    //     [SerializeField] public ItemModel DebugContextData;
    //     [SerializeField] private bool BoundsFromGO = false;
    //     [SerializeField] private bool BoundsNoGO = false;
    //     [SerializeField] private bool BoundsContextNoGO = false;
    //     [SerializeField, HideInInspector] private bool display = false;
    //     [SerializeField] private Vector3 actualSize;
    // #if UNITY_EDITOR
    //     [System.NonSerialized] private TestScript.DrawFullExtendedBox command = null;
    //     [System.NonSerialized] private BoundsExtensions.ExtendedBounds Bounds = null;
    // #endif
    //     protected virtual void OnValidate()
    //     {
    // #if UNITY_EDITOR
    //         int number = BoundsFromGO ? 1 : 0;
    //         number += BoundsNoGO ? 1 : 0;
    //         number += BoundsContextNoGO ? 1 : 0;

    //         if (Bounds != null && (number > 1 || number == 0))
    //         {
    //             TestScript.RemoveFromBuffer(command);
    //             Bounds.Dispose();
    //             Bounds = null;
    //             display = false;
    //         }
    // #endif
    //     }

    // #if UNITY_EDITOR
    //     protected virtual void OnDrawGizmosSelected()
    //     {
    //         if (Bounds == null)
    //         {
    //             if (BoundsFromGO)
    //             {
    //                 Bounds = ActualProvider.ExtendedBounds.Copy();
    //             }
    //             if (BoundsNoGO)
    //             {
    //                 Bounds = new BoundsExtensions.ExtendedBounds(ActualProvider.ExtendedBounds.ActualSize,
    //                                                              transform.position,
    //                                                              transform.rotation,
    //                                                              transform.lossyScale,
    //                                                              ActualProvider);
    //             }
    //             if (BoundsContextNoGO)
    //             {


    //                 var DebugContextDataBounds = DebugContextData.PrefabView.GetComponent<BoundsProvider>();

    //                 Bounds = new BoundsExtensions.ExtendedBounds(DebugContextDataBounds.ExtendedBounds.ActualSize,
    //                                                                          transform.position,
    //                                                                          transform.rotation,
    //                                                                          DebugContextData.PrefabView.transform.lossyScale,
    //                                                                          DebugContextDataBounds);
    //             }
    //             if (Bounds != null)
    //             {
    //                 command = new TestScript.DrawFullExtendedBox(Bounds);
    //                 TestScript.AddToCommandBuffer(command);
    //             }
    //         }
    //         display = Bounds != null;
    //         if (display)
    //         {
    //             Bounds.Position = transform.position;
    //             Bounds.Rotation = transform.rotation;
    //             Bounds.WorldScale = transform.lossyScale;
    //             actualSize = Bounds.ActualSize;
    //         }
    //     }
    // #endif

}
