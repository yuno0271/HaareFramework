using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Demo.Script.UI;
using Haare.Client.UI.UiManager;
using Haare.Util.Prefab;
using UnityEngine;

namespace Demo.Script
{
    public class LoadUIManager : SceneUIManager
    {
        private int loadingPanelID;
        private int loadingFadePanelID;
        
        public override async UniTask Initialize()
        {
            await base.Initialize();
            var loadingPanel = OpenPanel<LoadingPanel>(false,true);
            await loadingPanel;
            loadingPanelID = loadingPanel.Result;
            
            var loadingFadePanel = OpenPanel<LoadingFadePanel>(false,true);
            await loadingFadePanel;
            loadingFadePanelID = loadingFadePanel.Result;

            
            await RentPanel<LoadingFadePanel>(loadingFadePanelID).FadeOut();

        }
    }
}