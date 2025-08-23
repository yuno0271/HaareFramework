using System;
using Cysharp.Threading.Tasks;
using Demo.UI;
using Haare.Client.UI;


namespace Demo.TitleScene
{
    public class DemoTitleUIManager : SceneUIManager
    {
        private int debugPanelID;
        private int titlePanelID;
        public override async UniTask Initialize()
        {
            await base.Initialize();
            
            // var debugPanel = OpenPanel<DebugPanel>(false,true);
            // await debugPanel;
            // debugPanelID = debugPanel.Result;
            // BindIPanel(RentPanel<DebugPanel>(debugPanelID));
            
            titlePanelID = await OpenPanel<TitlePanel>(false,true);
            var _titlepanel = RentPanel<TitlePanel>(titlePanelID);
            BindIPanel(_titlepanel);
            

        }

        private void Reset()
        {
            
        }

        private void BindIPanel(ICustomPanel panel)
        {
            panel.uiManager = this;
            panel.BindEvent();
        }
    }
}