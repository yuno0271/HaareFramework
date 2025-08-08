using Demo.Script;
using Haare.Client.Container;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Demo.Script
{
    public class DemoBootScope : LifetimeScope 
    {
        
        //protected override LifetimeScope Parent => FindObjectOfType<CoreLifetimeScope>();
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DemoBootMono>();
        }
    }
}