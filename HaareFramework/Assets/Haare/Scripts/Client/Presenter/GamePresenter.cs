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
                Debug.Log("Ended Loading"+_);
                if (_ != SceneLoadPhase.EndLoad)
                    return;
                Processer.Instance.CheckDeleteProcessesForScene().Forget();
                Debug.Log("Ended Clean Scene");
            });
            Debug.Log("GamePresenter PostInitialize");
            
            
        }
    }
}