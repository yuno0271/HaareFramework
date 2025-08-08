using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Haare.Client.Routine.SceneRoutine;
using Haare.Client;

namespace Haare.Client.Presenter
{
    public class GamePresenter : IPostInitializable, IDisposable
    {
        [Inject]
        private ISceneRoutine _SceneRoutine;
        
        public void Dispose()
        {
        }

        public void PostInitialize()
        {
            _SceneRoutine.CurrentPhase.AsObservable()
                .Skip(1)
                .Subscribe(_ =>
            {
                Debug.Log("로드 끝났죠?"+_);
                if (_ != SceneLoadPhase.EndLoad)
                    return;
                Processer.Instance.CheckDeleteProcessesForScene().Forget();
                Debug.Log("청소 끝났죠?");
            });
            Debug.Log("GamePresenter PostInitialize");
        }
    }
}