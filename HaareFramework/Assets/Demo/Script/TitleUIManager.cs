using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Demo.Script.HaareDebug;
using Haare.Client.UI.UiManager;
using Haare.Util.Prefab;
using UnityEngine;

namespace Demo.Script
{
    public class TitleUIManager : SceneUIManager
    {
        public override async UniTask Initialize()
        {
            await base.Initialize();
            OpenPanel<DebugPanel>();
            OpenPanel<DebugPanel>();
            OpenPanel<DebugPanel>();
            OpenPanel<DebugPanel>();
        }
    }
}