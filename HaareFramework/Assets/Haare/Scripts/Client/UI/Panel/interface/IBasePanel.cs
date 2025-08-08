using Haare.Client.UI.UiManager;

namespace Haare.Client.UI.Panel
{
    public interface IBasePanel
    {        
        void SetSceneUiManager(SceneUiManager uiManager);
        public void OpenPanel();
        public void ClosePanel();
        public void ReloadPanel();
    }
}