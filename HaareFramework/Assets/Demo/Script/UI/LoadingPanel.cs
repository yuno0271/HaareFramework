using Haare.Client.Routine;
using Haare.Client.UI.HaareButton;
using Haare.Client.UI.HaareImage;
using Haare.Client.UI.HaareSlider;
using Haare.Client.UI.HaareText;
using Haare.Client.UI.Panel;
using Haare.Client.UI.UiManager;
using R3;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

namespace Demo.Script.UI
{
    public class LoadingPanel : MonoRoutine,ICustomPanel
    {
        [SerializeField] public CustomSlider LoadingSlider;
        public SceneUIManager uiManager { get; set; }
        public GameObject panel { get; set; }

        public void BindEvent()
        {
            
        }
        
        public void OpenPanel()
        {
            this.gameObject.SetActive(true);
            panel = this.gameObject;
        }

        public void ClosePanel()
        {
        }
    }
}