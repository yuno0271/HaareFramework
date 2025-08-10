using System.Collections.Generic;
using System;
using Demo.Script.UI;

namespace Haare.Util.Prefab
{
    public class PrefabPath
    {
        //Panel
        private static string DEBUG_PANEL = "Prefabs/HaareDebugPannel";
        private static string DEMO_TITLE_PANEL = "Prefabs/Demo_TitlePanel";
        private static string DEMO_LOADING_PANEL = "Prefabs/Demo_LoadingPanel";
        private static string DEMO_LOADINGFADE_PANEL = "Prefabs/Demo_LoadingFadePanel";
        public static Dictionary<Type, PrefabParam> PrefabDict = 
        new Dictionary<Type, PrefabParam>
        {
            { typeof(DebugPanel), new PrefabParam(DEBUG_PANEL) },
            { typeof(TitlePanel), new PrefabParam(DEMO_TITLE_PANEL) },
            { typeof(LoadingPanel), new PrefabParam(DEMO_LOADING_PANEL) },
            { typeof(LoadingFadePanel), new PrefabParam(DEMO_LOADINGFADE_PANEL) },
        };
        
    }
}