using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Demo.UI;
using Haare.Client.Routine.Service.SceneService;
using Haare.Client.UI;
using Haare.Util.Prefab;
using UnityEngine;

namespace Demo.LoadScene
{
    public class DemoLoadUIManager : SceneUIManager
    {
        private int loadingPanelID;
        
        public override async UniTask Initialize()
        {
            await base.Initialize();
            
            loadingPanelID = await OpenPanel<LoadingPanel>(false, true);
           
        }
        
    }
}