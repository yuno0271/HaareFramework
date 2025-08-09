using System;
using Cysharp.Threading.Tasks;
using Demo.Script.HaareDebug;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Haare.Client.Routine.SceneRoutine;
using Haare.Client;
using Haare.Client.UI.UiManager;

namespace Haare.Client.Presenter
{
    public class DebugUIPresenter : IPostInitializable, IDisposable
    {
        [Inject]
        private SceneUIManager _sceneUiManager;
        
        public void Dispose()
        {
        }

        public void PostInitialize()
        {
            _sceneUiManager.OnOpenedNewPannel.AsObservable()
            .Subscribe(_ =>
            {
                _.BindEvent();
                BindMV();
            });
        }

        private void BindMV()
        {
            
        }
    }
}