using Cysharp.Threading.Tasks;
using Demo.UI;
using Haare.Client.UI;


namespace Demo.LobbyScene
{
    public class DemoLobbyUIManager : SceneUIManager
    {
        
        public override async UniTask Initialize()
        {
            await base.Initialize();
        }
        
        private void BindIPanel(ICustomPanel panel)
        {
            panel.uiManager = this;
            panel.BindEvent();
        }
    }
}