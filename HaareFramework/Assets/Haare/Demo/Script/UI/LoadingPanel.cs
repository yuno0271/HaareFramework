using Haare.Client.Routine;
using Haare.Client.UI;


using UnityEngine;

namespace Demo.UI
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