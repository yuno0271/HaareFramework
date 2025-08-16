using UnityEngine;

namespace Haare.Client.UI
{
    public interface ICustomPanel
    {
        public SceneUIManager uiManager { get; set; }
        public GameObject panel { get; set; }
        public void OpenPanel() {}

        public void ClosePanel() {}

        public void ReloadPanel() {}
        
        public void BindEvent() {}
    }
}