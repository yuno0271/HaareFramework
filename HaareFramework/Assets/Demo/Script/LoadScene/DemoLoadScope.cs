using Demo.Script;
using Haare.Client.Container;
using Haare.Client.Presenter;
using Haare.Client.UI.UiManager;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Demo.Script
{
    public class DemoLoadScope : LifetimeScope 
    {
        
        //protected override LifetimeScope Parent => FindObjectOfType<CoreLifetimeScope>();
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LoadUIManager>().As<SceneUIManager>().AsSelf();;
            builder.RegisterComponentInHierarchy<DemoLoadMono>();
            builder.RegisterEntryPoint<LoadUIPresenter>();
        }
    }
}