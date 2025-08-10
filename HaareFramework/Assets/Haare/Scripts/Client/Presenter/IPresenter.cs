using System;
using R3;
using VContainer.Unity;

namespace Haare.Client.Presenter
{
    public interface IPresenter : IPostInitializable, IDisposable
    {
        public CompositeDisposable disposables { get; }
    }
}