using Demo.Script;
using Haare.Client.Container;
using Haare.Client.Presenter;
using Haare.Client.UI.UiManager;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Demo.Script
{
    public class DemoTitleScope : LifetimeScope 
    {
        
        //protected override LifetimeScope Parent => FindObjectOfType<CoreLifetimeScope>();
        protected override void Configure(IContainerBuilder builder)
        {
            //builder.Register<TitleUIManager>(Lifetime.Scoped).As<SceneUIManager>().AsSelf();
            builder.RegisterComponentInHierarchy<TitleUIManager>().As<SceneUIManager>().AsSelf();;
            builder.RegisterComponentInHierarchy<DemoTitleMono>();
            builder.RegisterEntryPoint<DebugUIPresenter>();
        }
    }
}