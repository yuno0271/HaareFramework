using Haare.Client.UI.UiManager;

namespace Haare.Client.UI.Panel
{
    public interface ICustomPanel
    {        
        public void OpenPanel() {}

        public void ClosePanel() {}

        public void ReloadPanel() {}
        
        public void BindEvent() {}
    }
}