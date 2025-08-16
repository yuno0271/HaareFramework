
using Cysharp.Threading.Tasks;
using Demo.UI;
using Haare.Client.Core.DI;

using R3;
using UnityEngine;
using VContainer;

using Haare.Client.Routine.Service.SceneService;
using Haare.Client.UI;
using Haare.Util.LogHelper;
using Unity.VisualScripting;

namespace Demo.LoadScene
{
    public class LoadUIPresenter : IPresenter 
    {
        [Inject]
        private CoreUIManager _coreUIManager;
        [Inject]
        private SceneUIManager _sceneUiManager;
        [Inject]
        private readonly IObjectResolver _resolver;
        [Inject] 
        public SceneRoutine sceneService;
        [Inject]
        private DemoLoadMono _loadMono;
        public void Dispose()
        {
            disposables.Dispose();
            LogHelper.Log(LogHelper.DEMO,$"{this.GetType()} Disposed");

        }

        public CompositeDisposable disposables { get;}  = new CompositeDisposable();

        public void PostInitialize()
        {
            disposables.Add(
                _sceneUiManager.OnOpenedNewPannel.AsObservable()
                .Select(panel => panel.ConvertTo(typeof(LoadingPanel)) as LoadingPanel)
                .Where(loadingpanel => loadingpanel != null) 
                .Subscribe(panel =>
                {
                    sceneService.LoadProgress.AsObservable().Subscribe(value =>
                    {
                        panel.LoadingSlider.SetValue(value);
                    });
                    LoadStartSequence().Forget();
                    sceneService.LoadScene();
                }));
        }
        
        private void BindIPanel(ICustomPanel panel)
        {
            panel.uiManager = _sceneUiManager;
            panel.BindEvent();
        }

        private async UniTask LoadStartSequence()
        {
            _coreUIManager.ClosePanel<LoadingFadePanel>();
            var fadepanelID = await _coreUIManager.OpenPanel<LoadingFadePanel>();
            var panel = _coreUIManager.RentPanel<LoadingFadePanel>(fadepanelID);
            await panel.FadeOut();
        }
    }
}