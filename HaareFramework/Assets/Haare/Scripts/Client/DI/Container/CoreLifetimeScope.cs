using VContainer;
using VContainer.Unity;
using Haare.Client.Routine.Service.SceneService;
using Haare.Client.UI;
using Haare.Util.LogHelper;
using UnityEngine;

namespace Haare.Client.Core.DI
{
    public class CoreLifetimeScope : LifetimeScope 
    {
        [SerializeField]
        private CoreUIManager _coreUIManagerPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneRoutine>(Lifetime.Singleton).As<SceneRoutine>().AsSelf();
            builder.RegisterComponentInNewPrefab(_coreUIManagerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad() // 씬 전환 시 파괴되지 않도록 설정
                .AsSelf();           // CoreUIManager 타입으로 등록
            
            builder.RegisterEntryPoint<GamePresenter>();
            LogHelper.Log(LogHelper.FRAMEWORK,"GameProcesserScope start");
            
        }
    }
}