
using System.Collections.Generic;

public static class ExtendedVersionOfArguments
{

    // private static Dictionary<System.Type, System.Func<BaseResolver, BaseSpawnArgs, bool, bool>> allTypesResolver = null;
    // private static Dictionary<System.Type, System.Func<BaseResolver, BaseSpawnArgs, bool, bool>> AllTypesResolver
    // {
    //     get
    //     {
    //         if (allTypesResolver == null)
    //         {
    //             allTypesResolver = new Dictionary<System.Type, System.Func<BaseResolver, BaseSpawnArgs, bool, bool>>
    //             {
    //                 {typeof(ContainerResolver), TestInitContainerSpawnArgs},
    //                 {typeof(DecipherResolver), TestInitDecipherSpawnArgs},
    //                 {typeof(TextResolver), TestInitTextSpawnArgs},
    //                 {typeof(InputResolver), TestInitInputSpawnArgs},
    //                 {typeof(KeyResolver), TestInitKeySpawnArgs},
    //                 {typeof(TriggerResolver), TestInitTriggerSpawnArgs},
    //                 {typeof(CubeResolver), TestInitCubeSpawnArgs},
    //             };
    //         }
    //         return allTypesResolver;
    //     }
    // }

    // /// 
    // /// Resolvers
    // /// 
    // public static bool TestInitWithSpawnArgs(this BaseResolver actualResolver, BaseSpawnArgs arguments, bool forceInitialize = false)
    // {
    //     var typeToLook = actualResolver.GetType();
    //     if (!AllTypesResolver.ContainsKey(typeToLook))
    //     {
    //         UnityEngine.Debug.LogError(" No type in dictionary " + typeToLook.Name);
    //         return false;
    //     }
    //     return AllTypesResolver[actualResolver.GetType()].Invoke(actualResolver, arguments, forceInitialize);
    // }

    // private static bool TestInitContainerSpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as ContainerResolver;
    //     var asRealArgument = arguments as ContainerSpawnArgs;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }

    // private static bool TestInitDecipherSpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as DecipherResolver;
    //     var asRealArgument = arguments as DecipherSpawnArgs;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }

    // private static bool TestInitTextSpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as TextResolver;
    //     var asRealArgument = arguments as TextSpawnArgs;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }

    // private static bool TestInitInputSpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as InputResolver;
    //     var asRealArgument = arguments as InputSpawnArgs;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }

    // private static bool TestInitKeySpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as KeyResolver;
    //     var asRealArgument = arguments as KeySpawnArgs;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }

    // private static bool TestInitTriggerSpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as TriggerResolver;
    //     var asRealArgument = arguments as TriggerSpawnArgs;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }

    // private static bool TestInitCubeSpawnArgs(BaseResolver contrainerArg, BaseSpawnArgs arguments, bool force)
    // {
    //     var realArg = contrainerArg as CubeResolver;
    //     var asRealArgument = arguments as CubeSpawnArguments;
    //     if (asRealArgument != null || force)
    //     {
    //         realArg.SetupSpawnArgs(asRealArgument);
    //         return true;
    //     }
    //     return false;
    // }
}