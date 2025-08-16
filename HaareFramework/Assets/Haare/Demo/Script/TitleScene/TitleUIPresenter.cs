using Cysharp.Threading.Tasks;

using Demo.UI;

using R3;
using UnityEngine;
using VContainer;

using Haare.Client.Core.DI;
using Haare.Client.Routine.Service.SceneService;
using Haare.Client.UI;
using Haare.Util.LogHelper;
using Unity.VisualScripting;

namespace Demo.TitleScene
{
    public class TitleUIPresenter : IPresenter
    {
        [Inject]
        private CoreUIManager _coreUIManager;
        [Inject]
        private SceneUIManager _sceneUiManager;
        [Inject]
        private readonly IObjectResolver _resolver;
        [Inject] 
        public SceneRoutine sceneService;

        public void Dispose()
        {
            disposables.Dispose();
            LogHelper.Log(LogHelper.DEMO,$"{this.GetType()}Disposed");

        }

        public CompositeDisposable disposables { get;}  = new CompositeDisposable();

        public void PostInitialize()
        {
            
            disposables.Add(
                _sceneUiManager.OnOpenedNewPannel.AsObservable()
                .Select(panel => panel.ConvertTo(typeof(TitlePanel)) as TitlePanel)
                .Where(fadepanel => fadepanel != null) 
                .Subscribe(panel =>
                {
                    panel.StartButton.Onclicked.AsObservable().Subscribe(_ =>
                    {
                        StartGameSequence().Forget();
                    });
                }));

            
        }

        private async UniTask StartGameSequence()
        {
            var loadingPanelID = await _coreUIManager.OpenPanel<LoadingFadePanel>(false, true);
            var loadingPanel = _coreUIManager.RentPanel<LoadingFadePanel>(loadingPanelID);
    
            await loadingPanel.FadeIn();

            await OnFinishedFadePanel(); 
        }
        
        private async UniTask OnFinishedFadePanel()
        {
            await sceneService.LoadSceneWithLoad(SceneName.LobbyScene);
        }
        
        private void BindIPanel(ICustomPanel panel)
        {
            panel.uiManager = _sceneUiManager;
            panel.BindEvent();
        }
    }
}