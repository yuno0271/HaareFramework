using Cysharp.Threading.Tasks;
using Haare.Client.Routine;
using Haare.Client.UI;
using R3;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using Unit = R3.Unit;

namespace Demo.UI
{
    public class LoadingFadePanel : MonoRoutine,ICustomPanel
    {
        [SerializeField] public CustomImage FadeImage;
        public SceneUIManager uiManager { get; set; }
        public GameObject panel { get; set; }
        
        public Subject<Unit> OnFinishedFade { get; }= new Subject<Unit>();

        public override async UniTask Initialize()
        {
            await base.Initialize();
            FadeImage.ChangeAlpha(0);
            await UniTask.CompletedTask;
        }

        public async UniTask TestFunc()
        {
            await FadeIn();
            await FadeOut();
        }

        public async UniTask FadeIn()
        {
            await FadeImage.Fade(0f, 1f, 0.6f);
            OnFinishedFade.OnNext(Unit.Default);
        }
        public async UniTask FadeOut()
        {
            await FadeImage.Fade(1f, 0f, 0.6f);
            OnFinishedFade.OnNext(Unit.Default);
        }
        
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