using Demo.Script;
using VContainer;
using VContainer.Unity;
using Haare.Client.Presenter;
using Haare.Client.Routine.SceneRoutine;
using Haare.Client.Singleton;
using UnityEngine;

namespace Haare.Client.Container
{
    public class CoreLifetimeScope : LifetimeScope 
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneRoutine>(Lifetime.Singleton).As<SceneRoutine>().AsSelf();
            //builder.RegisterComponentInHierarchy<DemoBootMono>();
            builder.RegisterEntryPoint<GamePresenter>();
            Debug.Log("GameProcesserScope start");
            
        }
    }
}