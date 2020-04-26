using Scripts.CommandPattern;
using Scripts.Utils.Async;
using System.Collections;
using System.Collections.Generic;
using Tests.Library.Scripts.Extensions;
using UnityEngine;

public class TestScript : MonoBehaviour, IReceiver
{
    private const int MAX_NUMBER_GIZMOS = 100;
    private static readonly List<ICommand> CommandBuffer = new List<ICommand>();
    public static void AddToCommandBuffer(ICommand commandToAdd)
    {
        if (CommandBuffer.Count == MAX_NUMBER_GIZMOS)
        {
            // CommandBuffer.RemoveFirst();
            for (int i = 0; i < MAX_NUMBER_GIZMOS - 1; i++)
            {
                CommandBuffer[i] = CommandBuffer[i + 1];
            }
            CommandBuffer.Remove(CommandBuffer[CommandBuffer.Count - 1]);
        }
        CommandBuffer.Add(commandToAdd);
    }
    public static void RemoveFromBuffer(ICommand toRemove)
    {
        CommandBuffer.Remove(toRemove);
    }

    static TestScript()
    {
        // GenericSpawnService.OnGenerationTry += DrawOverLapCollidersBox;
    }

    private static void DrawOverLapCollidersBox(System.Object[] arrayOfObjects)
    {
        var from = (Vector3)arrayOfObjects[0];
        var size = (Vector3)arrayOfObjects[1];
        var rotation = (Quaternion)arrayOfObjects[2];
        var pass = (bool)arrayOfObjects[3];

        TestScript.AddToCommandBuffer(new TestScript.DrawOverlapColliderBox(from, size, rotation, pass));
    }

    public struct DrawCastBox : ICommand
    {
        private readonly Vector3 center;
        private readonly Vector3 size;
        private readonly Quaternion rotation;
        private readonly Vector3 axisDistance;
        private readonly bool isGood;

        public DrawCastBox(Vector3 center, Vector3 size, Quaternion rotation, Vector3 distance, bool isGOod)
        {
            this.center = center;
            this.size = size;
            this.rotation = rotation;
            this.axisDistance = distance;
            this.isGood = isGOod;
        }

        void ICommand.Execute(IReceiver receiver)
        {
            Color prev = Gizmos.color;
            if (isGood)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.matrix = Matrix4x4.TRS(center + axisDistance * 0.5f, rotation, Vector3.one);
            var biggerSize = new Vector3(size.x, axisDistance.magnitude, size.z);
            Gizmos.DrawWireCube(Vector3.zero, biggerSize);
            Gizmos.color = prev;
            Gizmos.matrix = Matrix4x4.identity;
        }
    }
    public struct DrawOverlapColliderBox : ICommand
    {
        private readonly Vector3 center;
        private readonly Vector3 size;
        private readonly Quaternion rotation;
        private readonly bool isGood;
        public DrawOverlapColliderBox(Vector3 center, Vector3 size, Quaternion rotation, bool isGOod)
        {
            this.center = center;
            this.size = size;
            this.rotation = rotation;
            this.isGood = isGOod;
        }

        void ICommand.Execute(IReceiver receiver)
        {
#if UNITY_EDITOR

            Color prev = Gizmos.color;
            if (isGood)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.color = prev;
            Gizmos.matrix = Matrix4x4.identity;
#endif
        }
    }

    //     public class DrawFullExtendedBox : ICommand
    //     {
    //         private readonly BoundsExtensions.ExtendedBounds copyOfBounds;
    //         private float timerToLive;
    //         public DrawFullExtendedBox(BoundsExtensions.ExtendedBounds original)
    //         {
    //             copyOfBounds = original; //.Copy();
    //             timerToLive = 3;
    //         }
    //         void ICommand.Execute(IReceiver receiver)
    //         {
    // #if UNITY_EDITOR
    //             var asTestScript = receiver as TestScript;
    //             timerToLive -= 0.5f;
    //             asTestScript.DrawCustomPlanes(copyOfBounds);
    //             foreach (var lineSegment in copyOfBounds.LineSegments)
    //             {
    //                 asTestScript.DrawLineSegment(lineSegment.Item1, lineSegment.Item2);
    //             }
    //             copyOfBounds.BoundsStandart.DrawGizmoWireFrameBox(Color.black);
    //             if (timerToLive <= 0)
    //             {
    //                 copyOfBounds.Dispose();
    //             }
    // #endif
    //         }
    //     }

#pragma warning disable
    [SerializeField] private bool dontDestroyOnLoad = true;
    [SerializeField] private bool TestInner = false;
    [SerializeField] private bool TestAsLevelGenerator = false;
    [SerializeField] public Color color = Color.green;
#pragma warning restore

    private void Awake()
    {
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (TestInner)
        {
            TestInner = false;
            if (Application.isPlaying)
            {
                ThreadTools.StartCoroutine(DoSimpleTest());
            }
        }

        if (TestAsLevelGenerator)
        {
            TestAsLevelGenerator = false;
            if (Application.isPlaying)
            {
                ThreadTools.StartCoroutine(DoPLayTestLevelGenerator());
            }
        }
    }

    private void CheckIntesects()
    {
    }


    private IEnumerator DoPLayTestLevelGenerator()
    {
        yield return null;
        // reinitialize service
    }

    private IEnumerator DoSimpleTest()
    {
        yield return null;

        CheckIntesects();
    }

    private void OnDrawGizmos()
    {
        foreach (var item in CommandBuffer)
        {
            item.Execute(this);
        }
    }

    // private void DrawCustomPlanes(BoundsExtensions.ExtendedBounds extendedBounds)
    // {
    //     Color temCOlor;
    //     for (int i = 0; i < extendedBounds.Planes.Length; i++)
    //     {
    //         switch (i % 3)
    //         {
    //             case 0:
    //                 temCOlor = Color.green; // top bottom
    //                 break;
    //             case 1:
    //                 temCOlor = Color.red; // right left
    //                 break;
    //             case 2:
    //                 temCOlor = Color.blue; // front back
    //                 break;
    //             default:
    //                 temCOlor = Color.black;
    //                 break;
    //         }
    //         // if(i == 5) // back
    //         // { 
    //         //     continue;
    //         // }
    //         DrawPLaneAndNormal(extendedBounds.Planes[i].Item1, extendedBounds.Planes[i].Item2, temCOlor);
    //     }
    // }

    // Vector3 point1 = extendedBounds.Planes[BoundsExtensions.ExtendedBounds.PLanesLeftRight[0]].Item1;
    // Vector3 point2 = extendedBounds.Planes[BoundsExtensions.ExtendedBounds.PLanesLeftRight[1]].Item1;
    // float radius = extendedBounds.ActualSize.x; // only on X - top
    public static void DrawWireCapsule(Vector3 _pos1, Vector3 _pos2, float _radius)
    {
        var pointOffset = ((_pos2 - _pos1).magnitude - (_radius * 2)) / 2;

        //draw sideways
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_pos1, _radius);
        Gizmos.DrawWireSphere(_pos2, _radius);
        var mesh = PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cylinder);
        Gizmos.DrawWireMesh(mesh, -1, Vector3.Lerp(_pos1, _pos2, 0.5f));
    }
    private void DrawPLaneAndNormal(Vector3 center, Plane plane, Color color)
    {
        var normal = plane.normal;
        var direction = center + normal * 0.1f;
        Gizmos.color = color;
        Gizmos.DrawLine(center, direction);
    }

    private void DrawLineSegment(Vector3 pointA, Vector3 pointB)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA, pointB);
    }

#endif


    public void IReceiveCommand(ICommand command)
    {
        //
    }
}