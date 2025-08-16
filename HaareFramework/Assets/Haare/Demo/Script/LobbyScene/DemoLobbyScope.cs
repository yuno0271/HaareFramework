using Demo.LoadScene;
using VContainer;
using VContainer.Unity;

using Haare.Client.UI;


namespace Demo.LobbyScene
{
    public class DemoLobbyScope : LifetimeScope 
    {
        
        //protected override LifetimeScope Parent => FindObjectOfType<CoreLifetimeScope>();
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<LobbyUIManager>().As<SceneUIManager>().AsSelf();;
            builder.RegisterEntryPoint<LobbyUIPresenter>();
        }
    }
}