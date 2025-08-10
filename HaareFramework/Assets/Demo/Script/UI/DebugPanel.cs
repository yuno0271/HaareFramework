using Haare.Client.Routine;
using Haare.Client.UI.HaareButton;
using Haare.Client.UI.HaareImage;
using Haare.Client.UI.HaareText;
using Haare.Client.UI.Panel;
using Haare.Client.UI.UiManager;
using R3;
using UnityEngine;
using VContainer;

namespace Demo.Script.UI
{
    public class DebugPanel : MonoRoutine,ICustomPanel
    {
        [SerializeField] public CustomButton CloseButton;
        [SerializeField] public CustomImage background;
        [SerializeField] public CustomText StatusLog;
        
       
        private ICustomPanel _customPanelImplementation;

        public void BindEvent()
        {
            CloseButton.Onclicked.AsObservable()
                .Subscribe(_ =>
            {
                uiManager.ClosePanel<DebugPanel>();
            });
        }

        public SceneUIManager uiManager { get; set; }
        public GameObject panel { get; set; }

        public void OpenPanel()
        {
            this.gameObject.SetActive(true);
            panel = this.gameObject;
        }

        public void ClosePanel()
        {
            Debug.Log("ClosePanel");
            Destroy(this.gameObject);
        }
    }
}