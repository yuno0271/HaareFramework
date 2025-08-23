using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Haare.Client.Routine
{
    public interface IRoutine
    {
        CancellationTokenSource _cts { get; }

        //Processer Register?
        bool isRegistered	{ get; }
        //SceneOnly
        bool isInSceneOnly { get; }
        //Init?
        bool isInitialized	{ get; }
        
        Func<UniTask> Oninitialize  { get; }
        Func<UniTask> Onfinalize	{ get; }
        
        UniTask Initialize();
        UniTask Finalize();
    }
}