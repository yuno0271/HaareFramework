using System;
using Cysharp.Threading.Tasks;
using Demo.Script;
using Demo.Script.UI;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Haare.Client.Routine.SceneRoutine;
using Haare.Client;
using Haare.Client.UI.Panel;
using Haare.Client.UI.UiManager;
using Unity.VisualScripting;

namespace Haare.Client.Presenter
{
    public class TitleUIPresenter : IPresenter
    {
        [Inject]
        private SceneUIManager _sceneUiManager;
        [Inject]
        private readonly IObjectResolver _resolver;
        [Inject] 
        public SceneRoutine sceneService;

        public void Dispose()
        {
            disposables.Dispose();
            Debug.Log(this.GetType()+" Disposed");

        }

        public CompositeDisposable disposables { get;}  = new CompositeDisposable();

        public void PostInitialize()
        {
            
            disposables.Add(
                _sceneUiManager.OnOpenedNewPannel.AsObservable()
                .Select(panel => panel.ConvertTo(typeof(LoadingFadePanel)) as LoadingFadePanel)
                .Where(fadepanel => fadepanel != null) 
                .Subscribe(panel =>
                {
                    panel.disposables.Add(panel.OnFinishedFade.AsObservable().Subscribe(_ =>
                    {
                        OnFinishedFadePanel(panel).Forget();
                    }));
                    
                    panel.FadeIn().Forget();
                }));

            
        }

        private async UniTask OnFinishedFadePanel(LoadingFadePanel panel)
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