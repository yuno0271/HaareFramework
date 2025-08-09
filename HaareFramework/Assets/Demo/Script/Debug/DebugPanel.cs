using Haare.Client.Routine;
using Haare.Client.UI.BaseButton;
using Haare.Client.UI.BaseImage;
using Haare.Client.UI.BaseText;
using Haare.Client.UI.Panel;
using Haare.Client.UI.UiManager;
using R3;
using UnityEngine;
using VContainer;

namespace Demo.Script.HaareDebug
{
    public class DebugPanel : MonoRoutine,ICustomPanel
    {
        [SerializeField] public CustomButton CloseButton;
        [SerializeField] public CustomImage background;
        [SerializeField] public CustomText StatusLog;
        
        [Inject] private SceneUIManager uiManager;

        public void BindEvent()
        {
            CloseButton.Onclicked.AsObservable()
                .Subscribe(_ =>
            {
                ClosePanel();
            });
        }
        
        public void OpenPanel()
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePanel()
        {
            Debug.Log("ClosePanel");
            Destroy(this.gameObject);
        }
    }
}