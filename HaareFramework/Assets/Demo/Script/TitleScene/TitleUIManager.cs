using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Demo.Script.UI;
using Haare.Client.UI.Panel;
using Haare.Client.UI.UiManager;
using Haare.Util.Prefab;
using R3;
using UnityEngine;

namespace Demo.Script
{
    public class TitleUIManager : SceneUIManager
    {
        private int debugPanelID;
        private int titlePanelID;
        public override async UniTask Initialize()
        {
            await base.Initialize();
            
            var debugPanel = OpenPanel<DebugPanel>(false,true);
            await debugPanel;
            debugPanelID = debugPanel.Result;
            BindIPanel(RentPanel<DebugPanel>(debugPanelID));
            
            var titlePanel = OpenPanel<TitlePanel>(false,true);
            await titlePanel;
            titlePanelID = titlePanel.Result;
            var _titlepanel = RentPanel<TitlePanel>(titlePanelID);
            BindIPanel(_titlepanel);
            _titlepanel.StartButton.Onclicked.AsObservable().Subscribe(_=>
            {
                OpenPanel<LoadingFadePanel>(false, true);
            });
            
            

        }
        private void BindIPanel(ICustomPanel panel)
        {
            panel.uiManager = this;
            panel.BindEvent();
        }
    }
}