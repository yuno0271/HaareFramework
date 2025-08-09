using System.Collections.Generic;
using System;
using Demo.Script.HaareDebug;

namespace Haare.Util.Prefab
{
    public class PrefabPath
    {
        //Panel
        private static string DEBUG_PANEL = "Prefabs/HaareDebugPannel";
        public static Dictionary<Type, PrefabParam> PrefabDict = 
            new Dictionary<Type, PrefabParam>
            {
                { typeof(DebugPanel), new PrefabParam(DEBUG_PANEL) },
            };
        
    }
}